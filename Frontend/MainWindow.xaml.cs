using Frontend.Windows.Admin;
using ShoeStore.Helpers;
using System.Net;
using System.Windows;

namespace ShoeStore.Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoadingGrid.Visibility = Visibility.Visible;
            var login = LoginBox.Text;
            if (string.IsNullOrWhiteSpace(login))
            {
                MessageBox.Show("Не введен логин");
                return;
            }

            var password = PasswordBox.Password;
            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Не введен пароль");
                return;
            }

            var status = await ShoeHttpClient.Login(login, password);
            switch (status)
            {
                case HttpStatusCode.OK:
                    //MessageBox.Show($"Вы успешно авторизовались");
                    switch (ShoeHttpClient.Role)
                    {
                        case "Admin":
                            var adminPanel = new AdminPanel(this);
                            adminPanel.Show();
                            Hide();
                            break;
                        default:
                            MessageBox.Show($"Была получена несуществующая роль {ShoeHttpClient.Role}, обратитсь к техническому специалисту");
                            break;
                    }
                    break;
                case HttpStatusCode.BadRequest:
                    MessageBox.Show("Не были указаны логин и пароль");
                    break;
                case HttpStatusCode.Unauthorized:
                    MessageBox.Show("Введены неверные данные для входа");
                    break;
                default:
                    MessageBox.Show($"Не удалось войти в систему из-за ошибки (статус код {status}), обратитсь к техническому специалисту");
                    break;
            }
            LoadingGrid.Visibility = Visibility.Hidden;
        }
    }
}