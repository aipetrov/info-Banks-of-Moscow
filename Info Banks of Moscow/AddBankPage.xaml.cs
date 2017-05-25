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
    /// Логика взаимодействия для AddBankPage.xaml
    /// </summary>
    public partial class AddBankPage : Page
    {
        public AddBankPage()
        {
            InitializeComponent();
            NameTextBox.Focus();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if ((NameTextBox.Text != "") && (MetroTextBox.Text != "") && (AddressTextBox.Text != "") && (TelephoneTextBox.Text != "") && (RateComboBox.Text != "") && (OpinionTextBox.Text != ""))
                {
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

                    Bank newBank = new Bank(NameTextBox.Text, AddressTextBox.Text, MetroTextBox.Text, TelephoneTextBox.Text, RateComboBox.Text, OpinionTextBox.Text);

                    BinaryFormatter formatter = new BinaryFormatter();

                    List<Bank> banks = new List<Bank>();


                    using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
                    {
                        banks = (List<Bank>)formatter.Deserialize(fs);
                    }

                    banks.Add(newBank);

                    using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, banks);
                    }

                    ShowBanksPage ShowBanksPage = new ShowBanksPage();
                    NavigationService.Navigate(ShowBanksPage);
                }
                else
                {
                    MessageBox.Show("Данные введены некорректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            } catch { MessageBox.Show("Данные введены некорректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
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
