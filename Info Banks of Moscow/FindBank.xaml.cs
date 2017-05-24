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
using System.Windows.Shapes;

namespace Info_Banks_of_Moscow
{
    /// <summary>
    /// Логика взаимодействия для FindBank.xaml
    /// </summary>
    public partial class FindBank : Window
    {
        public FindBank()
        {
            InitializeComponent();
            NameTextBox.Focus();
        }

        private void ShowBanksButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            ShowBanks ShowBanksWindow = new ShowBanks();
            ShowBanksWindow.Show();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            Profile ProfileWindow = new Profile();
            ProfileWindow.Show();
        }

        private void FindBankButton_Click(object sender, RoutedEventArgs e)
        {
            //if (NameTextBox.Text == null)
            //{
            //    NameTextBox.Text = "-";
            //}

            //if (AddressTextBox.Text == null)
            //{
            //    AddressTextBox.Text = "-";
            //}

            //if (MetroTextBox.Text == null)
            //{
            //    MetroTextBox.Text = "-";
            //}

            //if (RateComboBox.Text == null)
            //{
            //    RateComboBox.Text = "0";
            //}

            //string[] ReadBanks = File.ReadAllLines("Banks.txt");

            //List<Bank> Banks = new List<Bank>();

            //for (int i = 0; i < ReadBanks.Length; i += 5)
            //{
            //    Bank NewBank = new Bank(ReadBanks[i], ReadBanks[i + 1], ReadBanks[i + 2], int.Parse(ReadBanks[i + 3]), ReadBanks[i + 4]);
            //    Banks.Add(NewBank);
            //}

            BinaryFormatter formatter = new BinaryFormatter();

            List<Bank> Banks = new List<Bank>();

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                Banks = (List<Bank>)formatter.Deserialize(fs);                
            }

            List<Bank> FoundBanks = new List<Bank>();

            for (int i = 0; i < Banks.Count; i++)
            {
                
                    if (((Banks[i].Name == NameTextBox.Text)||(NameTextBox.Text=="")) && 
                    ((Banks[i].Address == AddressTextBox.Text)||(AddressTextBox.Text=="")) && 
                    ((Banks[i].Metro == MetroTextBox.Text)|| (MetroTextBox.Text=="")) && 
                    ((Banks[i].Rate == RateComboBox.Text)|| (RateComboBox.Text=="")|| (RateComboBox.Text=="-")))
                    {
                        FoundBanks.Add(Banks[i]);
                    }
                //}
                //else
                //{
                //    NameTextBox.Text = " ";
                //    AddressTextBox.Text = " ";
                //    MetroTextBox.Text = " ";
                //    RateComboBox.Text = " ";
                //    if ((Banks[i].Name == NameTextBox.Text) || (Banks[i].Address == AddressTextBox.Text) || (Banks[i].Metro == MetroTextBox.Text) || (Banks[i].Rate == int.Parse(RateComboBox.Text)))
                //    {
                //        FoundBanks.Add(Banks[i]);
                //    }
                //}
            }

            ShowBanksDataGrid.ItemsSource = FoundBanks;

            if (FoundBanks.Count==0)
            {
                MessageBox.Show("Банков, соответствующих выбранным критериям, не существует.", "Результат поиска", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
