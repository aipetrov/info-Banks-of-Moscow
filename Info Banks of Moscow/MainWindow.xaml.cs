using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Info_Banks_of_Moscow
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {        
        public MainWindow()
        {
            InitializeComponent();            
            frameMain.Navigate(new LoginPage());
            //LoginTextBox.Focus();
        }

        //private void LoginButton_Click(object sender, RoutedEventArgs e)
        //{
        //    SqlConnection sqlcon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = c:\users\андрей\documents\visual studio 2017\Projects\Info Banks of Moscow\Info Banks of Moscow\Database1.mdf; Integrated Security = True");
        //    SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) From [Table] where Login='"+ LoginTextBox.Text + "'and Password='" + PasswordBox.Password+"'", sqlcon);

        //    DataTable Table = new DataTable();
        //    sda.Fill(Table);

        //    if (Table.Rows[0][0].ToString() == "1")
        //    {
        //        SqlCommand sqlcommand = new SqlCommand("Select Name FROM [Table] where Login='" + LoginTextBox.Text + "'and Password='" + PasswordBox.Password + "'",  sqlcon);
        //        sqlcon.Open();

        //        SqlDataReader sqlreader = sqlcommand.ExecuteReader();

        //        List<User> Users = new List<User>();

        //        while (sqlreader.Read())
        //        {
        //            User AuthorisedUser = new User(sqlreader.GetString(0), LoginTextBox.Text);
        //            Users.Add(AuthorisedUser);
                               
        //        }

        //        FileStream fs = new FileStream("name.txt", FileMode.Create);
        //        using (StreamWriter sw = new StreamWriter(fs))
        //        {
        //            sw.Write(LoginTextBox.Text + " " + Users[0].Name);
        //        }
                                
        //        this.Hide();

                
                
        //        Profile ProfileWindow = new Profile();        
        //        ProfileWindow.Show();

               
        //    }
        //    else
        //    {
        //        MessageBox.Show("Пользователя с таким логином и паролем не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }

            
        //}

        //private void RegisterButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Hide();

        //    RegisterWindow RegisterWindow = new RegisterWindow();
        //    RegisterWindow.Show();
        //}

        //private void GuestButton_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Hide();

        //    Guest GuestWindow = new Guest();
        //    GuestWindow.Show();
        //}
    }
}
