using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Info_Banks_of_Moscow
{
    /// <summary>
    /// Логика взаимодействия для ShowBanks.xaml
    /// </summary>
    public partial class ShowBanks : Window
    {
        public ShowBanks()
        {   InitializeComponent();


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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            Profile ProfileWindow = new Profile();
            ProfileWindow.Show();

        }
                
        private void AddBankButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            AddBank AddBankWindow = new AddBank();
            AddBankWindow.Show();
        }

        private void FindBankButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            FindBank FindBankWindow = new FindBank();
            FindBankWindow.Show();
        }

        private void DeleteBankButton_Click(object sender, RoutedEventArgs e)
        {
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

            List<Bank> AbleToDeleteBanks = new List<Bank>();

            for (int i = 0; i < Banks.Count; i++)
            {
                if (Banks[i].GetUserName(Banks[i]) == Users[0].Login)
                {
                    AbleToDeleteBanks.Add(Banks[i]);
                }
            }

            if (AbleToDeleteBanks.Count == 0)
            {
                MessageBox.Show("Вы не можете удалить ни одного банка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                this.Hide();

                DeleteBank DeleteBankWindow = new DeleteBank();
                DeleteBankWindow.Show();
            }
        }
    }
}
