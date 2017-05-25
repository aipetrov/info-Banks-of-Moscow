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
    /// Логика взаимодействия для FindBankPage.xaml
    /// </summary>
    public partial class FindBankPage : Page
    {
        public FindBankPage()
        {
            InitializeComponent();
            NameTextBox.Focus();
        }

        private void FindBankButton_Click(object sender, RoutedEventArgs e)
        {
            try
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

                BinaryFormatter formatter = new BinaryFormatter();

                List<Bank> banks = new List<Bank>();

                using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
                {
                    banks = (List<Bank>)formatter.Deserialize(fs);
                }

                List<Bank> foundBanks = new List<Bank>();

                for (int i = 0; i < banks.Count; i++)
                {

                    if (((banks[i].Name == NameTextBox.Text) || (NameTextBox.Text == "")) &&
                    ((banks[i].Address == AddressTextBox.Text) || (AddressTextBox.Text == "")) &&
                    ((banks[i].Metro == MetroTextBox.Text) || (MetroTextBox.Text == "")) &&
                    ((banks[i].Rate == RateComboBox.Text) || (RateComboBox.Text == "") || (RateComboBox.Text == "-")))
                    {
                        foundBanks.Add(banks[i]);
                    }
                }

                ShowBanksDataGrid.ItemsSource = foundBanks;

                if (foundBanks.Count == 0)
                {
                    MessageBox.Show("Банков, соответствующих выбранным критериям, не существует.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch { MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
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

        private void NameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}
