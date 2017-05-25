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
    /// Логика взаимодействия для ShowBanksPage.xaml
    /// </summary>
    public partial class ShowBanksPage : Page
    {
        public ShowBanksPage()
        {
            try
            {
                InitializeComponent();

                BinaryFormatter formatter = new BinaryFormatter();

                using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
                {
                    List<Bank> banks = (List<Bank>)formatter.Deserialize(fs);
                    ShowBanksDataGrid.ItemsSource = banks;

                }
            }
            catch { MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void FindBankButton_Click(object sender, RoutedEventArgs e)
        {
            FindBankPage FindBankPage = new FindBankPage();
            NavigationService.Navigate(FindBankPage);
        }

        private void AddBankButton_Click(object sender, RoutedEventArgs e)
        {
            AddBankPage AddBankPage = new AddBankPage();
            NavigationService.Navigate(AddBankPage);

        }

        
        private void DeleteBankButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch { MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage ProfilePage = new ProfilePage();
            NavigationService.Navigate(ProfilePage);
        }

        private void EditBankButton_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch { MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
