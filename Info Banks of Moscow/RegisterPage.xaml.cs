using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace Info_Banks_of_Moscow
{
    /// <summary>
    /// Логика взаимодействия для RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
            LoginTextBox.Focus();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlConnection sqlCon = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = c:\users\андрей\documents\visual studio 2017\Projects\Info Banks of Moscow\Info Banks of Moscow\Database1.mdf; Integrated Security = True");
                SqlConnection sqlConnection = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = c:\users\андрей\documents\visual studio 2017\Projects\Info Banks of Moscow\Info Banks of Moscow\Database1.mdf; Integrated Security = True");
                SqlCommand sqlCommand = new SqlCommand("Select Login From [Table] where Login= '" + LoginTextBox.Text + "'", sqlCon);
                sqlCon.Open();
                SqlDataReader sqlReader = sqlCommand.ExecuteReader();

                if (sqlReader.HasRows)
                {
                    while (sqlReader.Read())
                    {
                        if (LoginTextBox.Text == sqlReader.GetString(0))
                        {
                            MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                }
                else
                {
                    if ((PasswordBox.Password == PasswordAgainBox.Password) && (LoginTextBox.Text != "") && (NameTextBox.Text != "") && (PasswordBox.Password != ""))
                    {
                        using (sqlConnection)
                        {
                            SqlCommand sqlCommandInsert = new SqlCommand("INSERT INTO [Table] (Login, Password, Name) VALUES (@valueoflogin , @valueofpassword, @valueofname)", sqlConnection);
                            sqlCommandInsert.Parameters.AddWithValue("@valueoflogin", LoginTextBox.Text);
                            sqlCommandInsert.Parameters.AddWithValue("@valueofpassword", PasswordBox.Password);
                            sqlCommandInsert.Parameters.AddWithValue("@valueofname", NameTextBox.Text);
                            sqlCommandInsert.Connection.Open();
                            sqlCommandInsert.ExecuteNonQuery();
                            MessageBox.Show("Регистрация прошла успешно.", "Готово", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                            LoginPage LoginPage = new LoginPage();
                            NavigationService.Navigate(LoginPage);
                        }
                    }
                    else
                    {
                        if (PasswordBox.Password != PasswordAgainBox.Password)
                        {
                            MessageBox.Show("Введённые пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            MessageBox.Show("Данные введены некорректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch { MessageBox.Show("Что-то пошло не так...", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error); }
        }

        private void LogInButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage LoginPage = new LoginPage();
            NavigationService.Navigate(LoginPage);
        }
    }
}
