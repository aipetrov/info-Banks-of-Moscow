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
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Info_Banks_of_Moscow
{
    /// <summary>
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void CreateAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if ((PasswordBox.Password == PasswordAgainBox.Password) && (LoginTextBox.Text != null) && (NameTextBox.Text != null) && (PasswordBox.Password != null))
            {
                using (SqlConnection con = new SqlConnection(@"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = c:\users\андрей\documents\visual studio 2017\Projects\Info Banks of Moscow\Info Banks of Moscow\Database1.mdf; Integrated Security = True"))
                {
                    SqlCommand cmd_insert = new SqlCommand("INSERT INTO [Table] (Login, Password, Name) VALUES (@value1 , @value2, @value3)", con);
                    cmd_insert.Parameters.AddWithValue("@value1", LoginTextBox.Text);
                    cmd_insert.Parameters.AddWithValue("@value2", PasswordBox.Password);
                    cmd_insert.Parameters.AddWithValue("@value3", NameTextBox.Text);
                    cmd_insert.Connection.Open();
                    cmd_insert.ExecuteNonQuery();
                    MessageBox.Show("Регистрация прошла успешно.", "Готово", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    this.Hide();
                    MainWindow MainWindow = new MainWindow();
                    MainWindow.Show();
                }
            }
            else
            {
                MessageBox.Show("Данные введены некорректно.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }           
        }
    }
}
