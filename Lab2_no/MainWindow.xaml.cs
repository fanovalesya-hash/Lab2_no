using Lab2_no.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2_no
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Loggin_Button_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text.Trim();
            string password = PasswordBox.Password;

            ErrorTextBlock.Text = string.Empty;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                ErrorTextBlock.Text = "Заполните все поля!";
                return;
            }

            using (var context = new Book2307g2NasenkinaContext())
            {
                var user = context.Users
                                  .Include(u => u.Role)
                                  .FirstOrDefault(u => u.Login == login && u.Password == password);

                if (user != null)
                {
                    string roleName = user.Role.RoleName;
                    Window targetWindow;

                    if (roleName == "?????????????")
                        targetWindow = new AdminWindow(user);
                    else if (roleName == "????????")
                        targetWindow = new ManagerWindow(user);
                    else
                        targetWindow = new ClientWindow(user);

                    targetWindow.Show();
                    this.Hide();
                }
                else
                {
                    ErrorTextBlock.Text = "Неверный логин или пароль!";
                }
            }
        }
    }
}