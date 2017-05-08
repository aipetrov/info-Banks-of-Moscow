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
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = c:\users\андрей\documents\visual studio 2017\Projects\Info Banks of Moscow\Info Banks of Moscow\Database1.mdf; Integrated Security = True");
            SqlDataAdapter sda = new SqlDataAdapter("Select Count (*) From [Table] where Login='"+ LoginTextBox.Text + "'and Password='" + PasswordBox.Password+"'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows[0][0].ToString() == "1") 
            {
                User AuthorisedUser = new User(LoginTextBox.Text);
                this.Hide();

                Profile ProfileWindow = new Profile();
                ProfileWindow.Show();
               
            }
            else
            {
                MessageBox.Show("Пользователя с таким логином и паролем не существует", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();

            RegisterWindow RegisterWindow = new RegisterWindow();
            RegisterWindow.Show();
        }
    }
}
