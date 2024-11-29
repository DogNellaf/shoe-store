using ShoeStore.Frontend;
using ShoeStore.Helpers;
using System.Windows;

namespace Frontend.Windows.Admin
{
    /// <summary>
    /// Логика взаимодействия для AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        private Window _parent;
        public AdminPanel(Window parent)
        {
            InitializeComponent();
            _parent = parent;
            TitleLabel.Content = $"Добро пожаловать, {ShoeHttpClient.UserLogin}!";
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            _parent.Show();
            Close();
        }

        private void employeeEditorButton_Click(object sender, RoutedEventArgs e)
        {
            var editor = new EmployeeEditor(this);
            editor.Show();
            Hide();
        }
    }
}
