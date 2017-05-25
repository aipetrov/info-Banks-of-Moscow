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
    /// Логика взаимодействия для EditBankNewPage.xaml
    /// </summary>
    public partial class EditBankNewPage : Page
    {
        string[] editedBank;
        public EditBankNewPage()
        {       try
            {
                InitializeComponent();

                editedBank = File.ReadAllLines("editbank.txt");

                NameTextBox.Text = editedBank[1];
                AddressTextBox.Text = editedBank[2];
                MetroTextBox.Text = editedBank[3];
                TelephoneTextBox.Text = editedBank[4];
                RateComboBox.Text = editedBank[5];
                OpinionTextBox.Text = editedBank[6];
            }
            catch { MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }

        }


        List<Bank> ableToEditBanks = new List<Bank>();
        private void EditBankButton_Click(object sender, RoutedEventArgs e)
        {
            InitializeComponent();

            if (NameTextBox.Text != "")
            {
                string s = NameTextBox.Text;
                s = s.Substring(0, 1).ToUpper() + s.Remove(0, 1);
                NameTextBox.Text = s;
            }

            if (MetroTextBox.Text != "")
            {
                string s = MetroTextBox.Text;
                s = s.Substring(0, 1).ToUpper() + s.Remove(0, 1);
                MetroTextBox.Text = s;
            }

            if (AddressTextBox.Text != "")
            {
                string s = AddressTextBox.Text;
                s = s.Substring(0, 1).ToUpper() + s.Remove(0, 1);
                AddressTextBox.Text = s;
            }


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


            for (int i = 0; i < banks.Count; i++)
            {
                if ((banks[i].Name == ableToEditBanks[int.Parse(editedBank[0])].Name) &&
                   (banks[i].Address == ableToEditBanks[int.Parse(editedBank[0])].Address) &&
                   (banks[i].Metro == ableToEditBanks[int.Parse(editedBank[0])].Metro) &&
                   (banks[i].Telephone == ableToEditBanks[int.Parse(editedBank[0])].Telephone) &&
                   (banks[i].Rate == ableToEditBanks[int.Parse(editedBank[0])].Rate) &&
                   (banks[i].Opinion == ableToEditBanks[int.Parse(editedBank[0])].Opinion))
                {
                    banks.Remove(banks[i]);
                }

            }

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, banks);
            }

            Bank newBank = new Bank(NameTextBox.Text, AddressTextBox.Text, MetroTextBox.Text, TelephoneTextBox.Text, RateComboBox.Text, OpinionTextBox.Text);
                      

            List<Bank> banksEdited = new List<Bank>();


            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                banksEdited = (List<Bank>)formatter.Deserialize(fs);
            }

            banksEdited.Add(newBank);

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, banksEdited);
            }

            EditBankPage EditBankPage = new EditBankPage();
            NavigationService.Navigate(EditBankPage);
        }
    }
}
