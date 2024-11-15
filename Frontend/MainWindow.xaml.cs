using ShoeStore.Helpers;
using System.Windows;

namespace ShoeStore.Frontend
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        internal ShoeHttpClient client = new ShoeHttpClient();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await client.Login("admin", "admin");
        }
    }
}