using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            LoginTextBox.Focus();
        }

        private string CalculateHash(string password)
        {
            MD5 md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.ASCII.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = c:\users\андрей\documents\visual studio 2017\Projects\Info Banks of Moscow\Info Banks of Moscow\Database1.mdf; Integrated Security = True");
                SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) From [Table] where Login='" + LoginTextBox.Text + "'and Password='" + PasswordBox.Password + "'", sqlCon);


                SqlCommand sqlCommandPassword = new SqlCommand("Select Password From [Table] where Login='" + LoginTextBox.Text + "'", sqlCon);
                sqlCon.Open();

                SqlDataReader sqlReaderPassword = sqlCommandPassword.ExecuteReader();

                string[] hash = new string[1];

                while (sqlReaderPassword.Read())
                {
                    string _hash = sqlReaderPassword.GetString(0);
                    hash[0] = _hash;
                }

                sqlReaderPassword.Close();
                sqlCon.Close();


                DataTable Table = new DataTable();
                sda.Fill(Table);

                if ((Table.Rows[0][0].ToString() == "1") && (CalculateHash(PasswordBox.Password) == CalculateHash(hash[0])))
                {
                    SqlCommand sqlCommand = new SqlCommand("Select Name From [Table] where Login='" + LoginTextBox.Text + "'and Password='" + PasswordBox.Password + "'", sqlCon);
                    sqlCon.Open();

                    SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                    List<User> Users = new List<User>();

                    while (sqlReader.Read())
                    {
                        User authorisedUser = new User(sqlReader.GetString(0), LoginTextBox.Text);
                        Users.Add(authorisedUser);

                    }

                    FileStream fs = new FileStream("name.txt", FileMode.Create);
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(LoginTextBox.Text + " " + Users[0].Name);
                    }

                    ProfilePage ProfilePage = new ProfilePage();
                    NavigationService.Navigate(ProfilePage);
                }
                else
                {
                    MessageBox.Show("Пользователя с таким логином и паролем не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            catch { MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void GuestButton_Click(object sender, RoutedEventArgs e)
        {
            GuestPage GuestPage = new GuestPage();
            NavigationService.Navigate(GuestPage);
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegisterPage RegisterPage = new RegisterPage();
            NavigationService.Navigate(RegisterPage);
        }
    }
}
