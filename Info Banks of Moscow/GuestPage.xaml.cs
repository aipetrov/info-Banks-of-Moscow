using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Info_Banks_of_Moscow
{
    /// <summary>
    /// Логика взаимодействия для GuestPage.xaml
    /// </summary>
    public partial class GuestPage : Page
    {
        public GuestPage()
        {
            InitializeComponent();

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                List<Bank> Banks = (List<Bank>)formatter.Deserialize(fs);
                ShowBanksDataGrid.ItemsSource = Banks;


            }
        }

        private void FindBankButton_Click(object sender, RoutedEventArgs e)
        {
            FindBankGuestPage FindBankGuestPage = new FindBankGuestPage();
            NavigationService.Navigate(FindBankGuestPage);
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage LoginPage = new LoginPage();
            NavigationService.Navigate(Pages.LoginPage);
        }
    }
}
