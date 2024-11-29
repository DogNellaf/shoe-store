using Library.Dto.Employee;
using Library.Helpers;
using ShoeStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Frontend.Windows.Admin
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditor.xaml
    /// </summary>
    public partial class EmployeeEditor : Window
    {
        private Window _parent;
        private bool _isRowEditEnding;
        public EmployeeEditor(Window parent)
        {
            InitializeComponent();
            _parent = parent;
            LoadingGrid.Visibility = Visibility.Visible;
            Loaded += showEmployees;
        }

        private async void showEmployees(object sender, RoutedEventArgs e)
        {
            var employees = await ShoeHttpClient.GetEmployees();
            employees = employees.Where(x => x.Role != "Admin").ToList();

            dataGrid.ItemsSource = employees;
            dataGrid.CanUserResizeColumns = false;
            dataGrid.CanUserReorderColumns = false;

            //foreach (var item in dataGrid.Row)
            //{
                
            //}

            LoadingGrid.Visibility = Visibility.Hidden;
        }

        private bool checkExistsLogin(string login)
        {
            var loginColumn = dataGrid.Columns[0];
            foreach (var item in dataGrid.Items)
            {
                var user = item as EmployeeInfoDto;

                if (user == null)
                {
                    continue;
                }
                    
                if (user.Login == login)
                {
                    return true;
                }
            }
            return false;
        }

        private void backToPanelButton_Click(object sender, RoutedEventArgs e)
        {
            _parent.Show();
            Close();
        }

        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (_isRowEditEnding)
                return;

            try
            {
                _isRowEditEnding = true;

                if (e.EditAction == DataGridEditAction.Commit)
                {
                    dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                }

                var row = e.Row;
                var dto = row.Item as EmployeeInfoDto;

                if (dto == null)
                    return;

                if (!string.IsNullOrEmpty(dto.Login) && !string.IsNullOrEmpty(dto.Role))
                {
                    if (!checkExistsLogin(dto.Login))
                    {
                        MessageBox.Show($"Пользователь успешно создан");
                    }
                    else
                    {
                        MessageBox.Show($"Пользователь с таким именем уже существует");
                        dto.Login = "";
                    }
                }
            }
            finally
            {
                _isRowEditEnding = false;
            }
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.Column.Header.ToString() == "Пароль")
            {
                var password = (e.EditingElement as TextBox).Text;
                MessageBox.Show($"Пароль успешно изменен на {password}");
            }
        }

        private void generatePassword_Click(object sender, RoutedEventArgs e)
        {
            var password = PasswordGenerator.Generate();
            var button = sender as Button;
            var cell = button.Parent as DataGridCell;
            var row = cell.Parent as DataGridRow;

            var employeeInfo = button.DataContext as EmployeeInfoDto;
            if (employeeInfo == null) return;

            // Изменяем значение соседней ячейки
            employeeInfo.Role = "test"; // Устанавливаем значение в "Роль" или другую ячейку
            dataGrid.Items.Refresh();
        }
    }
}
