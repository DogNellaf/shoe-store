using Library.Dto.Employee;
using Library.Helpers;
using ShoeStore.Helpers;
using System.Collections;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace Frontend.Windows.Admin
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditor.xaml
    /// </summary>
    public partial class EmployeeEditor : Window
    {
        private Window _parent;
        private bool _isRowEditEnding;
        private bool _DoesNewUserHaveAllData = false;
        private EmployeeInfoDto lastNewUser = null;

        public EmployeeEditor(Window parent)
        {
            InitializeComponent();
            _parent = parent;
            LoadingGrid.Visibility = Visibility.Visible;
            Loaded += showEmployees;
        }

        private async void showEmployees(object sender, RoutedEventArgs e)
        {
            var employees = await BaseServerContext.GetEmployees();
            employees = employees.Where(x => x.Role != "Admin").ToList();

            dataGrid.ItemsSource = employees;
            dataGrid.CanUserResizeColumns = false;
            dataGrid.CanUserReorderColumns = false;

            LoadingGrid.Visibility = Visibility.Hidden;
        }

        private bool checkExistsLogin(string login, int rowIndex)
        {
            var loginColumn = dataGrid.Columns[0];
            for (int i = 0; i < dataGrid.Items.Count; i++)
            {
                var item = dataGrid.Items[i];
                var user = item as EmployeeInfoDto;

                if (user == null)
                {
                    continue;
                }

                if (user.Login == login && i != rowIndex)
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

        private async void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (_isRowEditEnding)
                return;

            try
            {
                _isRowEditEnding = true;

                var row = e.Row;

                var oldDto = (row.Item as EmployeeInfoDto).Copy();

                if (e.EditAction == DataGridEditAction.Commit)
                {
                    dataGrid.CommitEdit(DataGridEditingUnit.Row, true);
                }
                
                var dto = row.Item as EmployeeInfoDto;

                if (dto == null)
                    return;

                if (!string.IsNullOrEmpty(dto.Login) && !string.IsNullOrEmpty(dto.Role))
                {
                    if (!checkExistsLogin(dto.Login, row.GetIndex()))
                    {
                        if (dto == lastNewUser)
                        {
                            if (string.IsNullOrEmpty(dto.Password))
                            {
                                MessageBox.Show($"Для нового пользователя обязательно должен быть задан пароль", "Ошибка!");
                            }
                            else
                            {
                                var code = await BaseServerContext.CreateEmployee(dto);
                                switch (code)
                                {
                                    case HttpStatusCode.OK:
                                        MessageBox.Show($"Пользователь успешно создан", "Удачно");
                                        lastNewUser = null;
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
                        } 
                        else
                        {
                            if (dto.Login != oldDto.Login || !string.IsNullOrEmpty(dto.Password) || dto.Role != oldDto.Role)
                            {
                                var code = await BaseServerContext.UpdateEmployee(dto);
                                switch (code)
                                {
                                    case HttpStatusCode.OK:
                                        MessageBox.Show($"Пользователь успешно изменен", "Удачно");
                                        lastNewUser = null;
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
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Пользователь с таким именем уже существует", "Ошибка!");
                        dto.Login = "";
                    }
                }
            }
            finally
            {
                _isRowEditEnding = false;
            }
        }

        private void generatePassword_Click(object sender, RoutedEventArgs e)
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

        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastNewUser == null)
            {
                var list = (IList)dataGrid.ItemsSource;
                lastNewUser = new EmployeeInfoDto();
                list.Add(lastNewUser);
                dataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show($"Перед добавлением нового пользователя впишите все данные предыдущего");
            }
        }
    }
}
