using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Info_Banks_of_Moscow
{
    /// <summary>
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            List<User> Users = new List<User>();
            FileStream fs = new FileStream("name.txt", FileMode.Open);
            using (StreamReader sr = new StreamReader(fs))
            {
                string str = sr.ReadLine();
                string[] loginname = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                User AuthorisedUser = new User(loginname[1], loginname[0]);
                Users.Add(AuthorisedUser);
            }

            Greeting.Content = "Здравствуйте, " + Users[0].Name + "!";


        }

        private void AddBankButton_Click(object sender, RoutedEventArgs e)
        {
            AddBankPage AddBankPage = new AddBankPage();
            NavigationService.Navigate(AddBankPage);
        }

        private void ShowBanksButton_Click(object sender, RoutedEventArgs e)
        {
            ShowBanksPage ShowBanksPage = new ShowBanksPage();
            NavigationService.Navigate(ShowBanksPage);
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage LoginPage = new LoginPage();
            NavigationService.Navigate(LoginPage);
        }

        private void FindBankButton_Click(object sender, RoutedEventArgs e)
        {
            FindBankPage FindBankPage = new FindBankPage();
            NavigationService.Navigate(FindBankPage);
        }

        private void DeleteBankButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteBankPage DeleteBankPage = new DeleteBankPage();
            NavigationService.Navigate(DeleteBankPage);
        }
    }
}
