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
    /// Логика взаимодействия для DeleteBankPage.xaml
    /// </summary>
    public partial class DeleteBankPage : Page
    {
        List<Bank> ableToDeleteBanks = new List<Bank>();
        public DeleteBankPage()
        {
            try
            {
                InitializeComponent();
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

                //if (AbleToDeleteBanks.Count == 0)
                //{   MessageBox.Show("Вы не можете удалить ни одного банка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                //    DeleteBankButton.IsEnabled = false;
                //    ShowBanksDataGrid.IsEnabled = false;
                //    AbleToDeleteBanksTextBox.IsEnabled = false;
                //}

                ShowBanksDataGrid.ItemsSource = ableToDeleteBanks;

                for (int i = 0; i < ableToDeleteBanks.Count; i++)
                {
                    string str = i + "~" + ableToDeleteBanks[i].Name + "~" + ableToDeleteBanks[i].Address + "~" + ableToDeleteBanks[i].Metro + "~" + ableToDeleteBanks[i].Telephone;
                    AbleToDeleteBanksTextBox.Items.Add(str);
                }
            }
            catch { MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void DeleteBankButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = AbleToDeleteBanksTextBox.Text;
                string[] bank = str.Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries);
                int numberOfDeletedBank = int.Parse(bank[0]);

                Bank deletedBank = ableToDeleteBanks[numberOfDeletedBank];

                BinaryFormatter formatter = new BinaryFormatter();

                List<Bank> banks = new List<Bank>();

                using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
                {
                    banks = (List<Bank>)formatter.Deserialize(fs);
                }

                for (int i = 0; i < banks.Count; i++)
                {
                    if ((banks[i].Name == deletedBank.Name) &&
                        (banks[i].Address == deletedBank.Address) &&
                        (banks[i].Metro == deletedBank.Metro) &&
                        (banks[i].Telephone == deletedBank.Telephone) &&
                        (banks[i].Rate == deletedBank.Rate) &&
                        (banks[i].Opinion == deletedBank.Opinion))
                    {
                        banks.Remove(banks[i]);
                    }
                }

                using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
                {
                    formatter.Serialize(fs, banks);
                }

                ShowBanksPage ShowBanksPage = new ShowBanksPage();
                NavigationService.Navigate(ShowBanksPage);

            }
            catch { MessageBox.Show("Выберете банк для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void ShowBanksButton_Click(object sender, RoutedEventArgs e)
        {
            ShowBanksPage ShowBanksPage = new ShowBanksPage();
            NavigationService.Navigate(ShowBanksPage);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage ProfilePage = new ProfilePage();
            NavigationService.Navigate(ProfilePage);
        }
    }
}
