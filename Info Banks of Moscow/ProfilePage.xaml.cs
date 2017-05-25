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
    /// Логика взаимодействия для ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            try
            {
                InitializeComponent();
                List<User> users = new List<User>();
                FileStream fs = new FileStream("name.txt", FileMode.Open);
                using (StreamReader sr = new StreamReader(fs))
                {
                    string str = sr.ReadLine();
                    string[] loginName = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    User authorisedUser = new User(loginName[1], loginName[0]);
                    users.Add(authorisedUser);
                }

                Greeting.Content = "Здравствуйте, " + users[0].Name + "! Вы можете:";
            }
            catch { MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

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
            List<Bank> ableToDeleteBanks = new List<Bank>();

            BinaryFormatter formatter = new BinaryFormatter();

            List<Bank> banks = new List<Bank>();

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                banks = (List<Bank>)formatter.Deserialize(fs);
            }

            FileStream fsn = new FileStream("name.txt", FileMode.Open);
            List<User> users = new List<User>();

            using (StreamReader sr = new StreamReader(fsn))
            {

                string str = sr.ReadLine();
                string[] loginName = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                User authorisedUser = new User(loginName[1], loginName[0]);
                users.Add(authorisedUser);
            }



            for (int i = 0; i < banks.Count; i++)
            {
                if (banks[i].GetUserName(banks[i]) == users[0].Login)
                {
                    ableToDeleteBanks.Add(banks[i]);
                }
            }
            if (ableToDeleteBanks.Count == 0)
            {
                MessageBox.Show("Вы не можете удалить ни одного банка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                DeleteBankPage DeleteBankPage = new DeleteBankPage();
                NavigationService.Navigate(DeleteBankPage);
            }
        }

        private void EditBankButton_Click(object sender, RoutedEventArgs e)
        {
            List<Bank> ableToEditBanks = new List<Bank>();

            BinaryFormatter formatter = new BinaryFormatter();

            List<Bank> banks = new List<Bank>();

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                banks = (List<Bank>)formatter.Deserialize(fs);
            }

            FileStream fsn = new FileStream("name.txt", FileMode.Open);
            List<User> users = new List<User>();

            using (StreamReader sr = new StreamReader(fsn))
            {

                string str = sr.ReadLine();
                string[] loginName = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                User authorisedUser = new User(loginName[1], loginName[0]);
                users.Add(authorisedUser);
            }



            for (int i = 0; i < banks.Count; i++)
            {
                if (banks[i].GetUserName(banks[i]) == users[0].Login)
                {
                    ableToEditBanks.Add(banks[i]);
                }
            }

            if (ableToEditBanks.Count == 0)
            {
                MessageBox.Show("Вы не можете изменить ни одного банка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                EditBankPage EditBankPage = new EditBankPage();
                NavigationService.Navigate(EditBankPage);
            }
        }
    }
}
