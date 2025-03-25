using Library.Dto.Employee;
using Library.Helpers;
using ShoeStore.Dto.Sale;
using ShoeStore.Helpers.ServerContexts;
using System.Collections;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace ShoeStore.Frontend.Windows.Admin
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditor.xaml
    /// </summary>
    public partial class SalesEditor : Window
    {
        // Родительское окно
        private Window _parent;

        // Флаг того, что строчку закончили редактировать
        private bool _isRowEditEnding;

        // Флаг того, что у новой строчки заполнены все данные
        private bool _doesNewSaleHaveAllData = false;

        // Последняя созданная скидка
        private SaleInfoDto? lastNewSale = null;

        /// <summary>
        /// Создание нового окна "Редактор продаж"
        /// </summary>
        /// <param name="parent">Родительское окно</param>
        public SalesEditor(Window parent)
        {
            // Инициализиуем элементы на форме
            InitializeComponent();

            // Задаем родительское окно
            _parent = parent;

            // Отображаем панель загрузки
            LoadingGrid.Visibility = Visibility.Visible;

            // К событию загрузки формы прикрепляем функцию отображения продаж
            Loaded += ShowSales;
        }

        /// <summary>
        /// Отображает продажи из базы данных в таблице
        /// </summary>
        /// <param name="sender">Загруженный элемент на форме</param>
        /// <param name="e">Аргументы события</param>
        private async void ShowSales(object sender, RoutedEventArgs e)
        {
            // Получаем все продажи со стороны сервера
            var sales = await SaleContext.GetSales();

            // Загружаем в таблицу данные, устанавливаем параметры
            dataGrid.ItemsSource = sales;
            dataGrid.CanUserResizeColumns = false;
            dataGrid.CanUserReorderColumns = false;

            // Скрываем панель загрукзи
            LoadingGrid.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Функция обработки нажатия на кнопку выхода на предыдущую форму
        /// </summary>
        /// <param name="sender">Элемент, на который нажали</param>
        /// <param name="e">Аргументы события нажатия</param>
        private void BackToPanelButton_Click(object sender, RoutedEventArgs e)
        {
            _parent.Show();
            Close();
        }

        /// <summary>
        /// Функция обработки завершения редактирования таблицы
        /// </summary>
        /// <param name="sender">Элемент, на который нажали</param>
        /// <param name="e">Аргументы события завершения редактирования</param>
        private async void DataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (_isRowEditEnding)
            {
                return;
            }

            try
            {
                _isRowEditEnding = true;

                var row = e.Row;
                var oldDto = ((SaleInfoDto)row.Item).Copy();

                if (e.EditAction == DataGridEditAction.Commit)
                {
                    dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                }
                
                var dto = row.Item as SaleInfoDto;
                if (dto == null)
                {
                    return;
                }

                bool isItemTitleEmpty = string.IsNullOrEmpty(dto.ItemTitle);
                if (isItemTitleEmpty)
                {
                    MessageBox.Show($"Для новой продажи обязательно необходмо задать название товара", "Ошибка!");
                    return;
                }

                bool isEmployeeLoginEmpty = string.IsNullOrEmpty(dto.ItemTitle);
                if (isEmployeeLoginEmpty)
                {
                    MessageBox.Show($"Для новой продажи обязательно необходмо задать имя сотрудника", "Ошибка!");
                    return;
                }

                var code = HttpStatusCode.NoContent;
                if (dto == lastNewSale)
                {
                    code = await SaleContext.CreateSale(dto);
                }
                else if (dto.ItemTitle != oldDto.ItemTitle || dto.EmployeeLogin != oldDto.EmployeeLogin)
                {
                    code = await SaleContext.UpdateSale(dto);
                }

                // TODO: реализовать правильные коды ошибок
                switch (code)
                {
                    case HttpStatusCode.OK:
                        MessageBox.Show($"Пользователь успешно создан", "Удачно");
                        lastNewSale = null;
                        break;
                    case HttpStatusCode.NotFound:
                        MessageBox.Show($"Роль с таким названием отсутствует", "Ошибка!");
                        break;
                    case HttpStatusCode.Conflict:
                        MessageBox.Show($"Пользователь с таким логином уже существует");
                        break;
                    default:
                        MessageBox.Show($"При создании пользователя возникла ошибка со статусом {code}", "Ошибка!");
                        break;
                }
            }
            finally
            {
                _isRowEditEnding = false;
            }
        }

        /// <summary>
        /// Функция обработки нажатия на кнопку "Добавить продажу"
        /// </summary>
        /// <param name="sender">Элемент, на который нажали</param>
        /// <param name="e">Аргументы события</param>
        private void AddSaleButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastNewSale == null)
            {
                var list = (IList)dataGrid.ItemsSource;
                list.Add(
                    new SaleInfoDto()
                );
                dataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show($"Перед добавлением нового пользователя впишите все данные предыдущего");
            }
        }

        /// <summary>
        /// Функция обработки нажатия на кнопку "Вернуть товар"
        /// </summary>
        /// <param name="sender">Элемент, на который нажали</param>
        /// <param name="e">Аргументы события</param>
        private void ReturnItem_Click(object sender, RoutedEventArgs e)
        {
            var password = PasswordGenerator.Generate();
            var button = sender as Button;
            var dto = button.DataContext as EmployeeInfoDto;
            if (dto != null)
            {
                dataGrid.CommitEdit();
                dto.Password = password;
                dataGrid.Items.Refresh();
            }
        }
    }
}
