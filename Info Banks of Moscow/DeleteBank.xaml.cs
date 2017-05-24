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
    /// Логика взаимодействия для DeleteBank.xaml
    /// </summary>
    /// 

        


    public partial class DeleteBank : Window
    {
        List<Bank> AbleToDeleteBanks = new List<Bank>();

        public DeleteBank()
        {
            InitializeComponent();

            BinaryFormatter formatter = new BinaryFormatter();

            List<Bank> Banks = new List<Bank>();

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                Banks = (List<Bank>)formatter.Deserialize(fs);                
            }
            
            FileStream fsn = new FileStream("name.txt", FileMode.Open);
            List<User> Users = new List<User>();

            using (StreamReader sr = new StreamReader(fsn))
            {
               
                string str = sr.ReadLine();
                string[] loginname = str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                User AuthorisedUser = new User(loginname[1], loginname[0]);
                Users.Add(AuthorisedUser);
            }

            

            for (int i = 0; i < Banks.Count; i++)
            {
                if (Banks[i].GetUserName(Banks[i])==Users[0].Login)
                {
                    AbleToDeleteBanks.Add(Banks[i]);
                }
            }

            //if (AbleToDeleteBanks.Count == 0)
            //{   MessageBox.Show("Вы не можете удалить ни одного банка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //    DeleteBankButton.IsEnabled = false;
            //    ShowBanksDataGrid.IsEnabled = false;
            //    AbleToDeleteBanksTextBox.IsEnabled = false;
            //}

            ShowBanksDataGrid.ItemsSource = AbleToDeleteBanks;

            for (int i = 0; i < AbleToDeleteBanks.Count; i++)
            {
                string str = i + "-" + AbleToDeleteBanks[i].Name + "-" + AbleToDeleteBanks[i].Address + "-" + AbleToDeleteBanks[i].Metro + "-" + AbleToDeleteBanks[i].Telephone;
                 AbleToDeleteBanksTextBox.Items.Add(str);
            }            

        }

        private void DeleteBankButton_Click(object sender, RoutedEventArgs e)
        {
            string str = AbleToDeleteBanksTextBox.Text;
            string[] bank = str.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            int numberOfDeletedBank = int.Parse(bank[0]);

            Bank DeletedBank = AbleToDeleteBanks[numberOfDeletedBank];

            BinaryFormatter formatter = new BinaryFormatter();

            List<Bank> Banks = new List<Bank>();

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                Banks = (List<Bank>)formatter.Deserialize(fs);
            }

            for (int i = 0; i < Banks.Count; i++)
            {
                if ((Banks[i].Name==DeletedBank.Name)&& 
                    (Banks[i].Address == DeletedBank.Address) && 
                    (Banks[i].Metro == DeletedBank.Metro) && 
                    (Banks[i].Telephone == DeletedBank.Telephone) && 
                    (Banks[i].Rate == DeletedBank.Rate) && 
                    (Banks[i].Opinion == DeletedBank.Opinion))
                {
                    Banks.Remove(Banks[i]);
                }
            }

            using (FileStream fs = new FileStream("Banks.dat", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, Banks);
            }

            this.Hide();

            ShowBanks ShowBanksWindow = new ShowBanks();
            ShowBanksWindow.Show();
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
    }
}
