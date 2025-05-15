using Microsoft.EntityFrameworkCore;
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

namespace Tourista.View
{
    /// <summary>
    /// Логика взаимодействия для Autorization.xaml
    /// </summary>
    public partial class Autorization : Window
    {
        public Autorization()
        {
            InitializeComponent();
        }

        private TouristaContext _context = new TouristaContext();

        private void RegisterLink_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                MessageBox.Show("Пожалуйста, введите email и пароль", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = _context.Users.FirstOrDefault(u => u.UserEmail == EmailTextBox.Text);

            if (user == null || user.UserPassword != PasswordBox.Password)
            {
                MessageBox.Show("Неверный email или пароль", "Ошибка авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            OpenMainWindow(user);
        }

        private void OpenMainWindow(User user)
        {
            if (user.Role == "admin")
            {
                var adminPanel = new AdminPanel();
                adminPanel.Show();

                this.Close();
            }
            else
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();

                this.Close();
            }
        }

    }
}
