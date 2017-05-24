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
            InitializeComponent();
            //string[] ReadBanks = File.ReadAllLines("Banks.txt");

            //List<Bank> Banks = new List<Bank>();

            BinaryFormatter formatter = new BinaryFormatter();

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                List<Bank> Banks = (List<Bank>)formatter.Deserialize(fs);
                ShowBanksDataGrid.ItemsSource = Banks;
                
            }


            //for (int i = 0; i < ReadBanks.Length; i+=5)
            //{                
            //    Bank NewBank = new Bank(ReadBanks[i], ReadBanks[i+1], ReadBanks[i+2], int.Parse(ReadBanks[i+3]), ReadBanks[i+4]);
            //    Banks.Add(NewBank);
            //}
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
            DeleteBankPage DeleteBankPage = new DeleteBankPage();
            NavigationService.Navigate(DeleteBankPage);            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage ProfilePage = new ProfilePage();
            NavigationService.Navigate(ProfilePage);
        }
    }
}
