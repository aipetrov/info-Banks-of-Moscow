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
    /// Логика взаимодействия для AddBank.xaml
    /// </summary>
    public partial class AddBank : Window
    {
        public AddBank()
        {
            InitializeComponent();
            NameTextBox.Focus();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            Bank NewBank = new Bank(NameTextBox.Text, AddressTextBox.Text, MetroTextBox.Text,  TelephoneTextBox.Text, RateComboBox.Text, OpinionTextBox.Text);

            //FileStream fs = new FileStream("Banks.txt", FileMode.Append);
            //using (StreamWriter sw = new StreamWriter(fs))
            //{
            //    sw.WriteLine(NewBank.Name);
            //    sw.WriteLine(NewBank.Address);
            //    sw.WriteLine(NewBank.Metro);
            //    sw.WriteLine(NewBank.Rate);
            //    sw.WriteLine(NewBank.OpinionFormer(NewBank));
            //}

            BinaryFormatter formatter = new BinaryFormatter();

            List<Bank> Banks = new List<Bank>();


            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                Banks = (List<Bank>)formatter.Deserialize(fs);
            }

            Banks.Add(NewBank);

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Banks);                
            }

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

        private void ShowBanksButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            ShowBanks ShowBanksWindow = new ShowBanks();
            ShowBanksWindow.Show();
        }
    }
}
