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
    /// Логика взаимодействия для EditBankPage.xaml
    /// </summary>
    public partial class EditBankPage : Page
    {
        List<Bank> ableToEditBanks = new List<Bank>();
        public EditBankPage()
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
                        ableToEditBanks.Add(banks[i]);
                    }
                }

                ShowBanksDataGrid.ItemsSource = ableToEditBanks;

                for (int i = 0; i < ableToEditBanks.Count; i++)
                {
                    string str = i + "~" + ableToEditBanks[i].Name + "~" + ableToEditBanks[i].Address + "~" + ableToEditBanks[i].Metro + "~" + ableToEditBanks[i].Telephone;
                    AbleToEditBanksTextBox.Items.Add(str);
                }
            }
            catch { MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void EditBankButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string str = AbleToEditBanksTextBox.Text;
                string[] bank = str.Split(new char[] { '~' }, StringSplitOptions.RemoveEmptyEntries);


                FileStream fs = new FileStream("editbank.txt", FileMode.Create);
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    for (int i = 0; i < bank.Length; i++)
                    {
                        sw.WriteLine(bank[i]);
                    }
                    sw.WriteLine(ableToEditBanks[int.Parse(bank[0])].Rate);
                    sw.WriteLine(ableToEditBanks[int.Parse(bank[0])].GetJustOpinion(ableToEditBanks[int.Parse(bank[0])]));
                }
            }
            catch { MessageBox.Show("Выберете банк для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
           
            EditBankNewPage EditBankNewPage = new EditBankNewPage();
            NavigationService.Navigate(EditBankNewPage);
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
