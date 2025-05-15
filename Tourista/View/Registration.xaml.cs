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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private TouristaContext _context = new TouristaContext();

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Проверка заполнения всех полей
            if (string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                string.IsNullOrWhiteSpace(PasswordBox.Password) ||
                string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                string.IsNullOrWhiteSpace(PhoneTextBox.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_context.Users.Any(u => u.UserEmail == EmailTextBox.Text))
            {
                MessageBox.Show("Пользователь с таким email уже зарегистрирован.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Создание нового пользователя
            var newUser = new User
            {
                UserEmail = EmailTextBox.Text,
                UserPassword = PasswordBox.Password,
                UserFirstName = FirstNameTextBox.Text,
                UserLastName = LastNameTextBox.Text,
                UserPhoneNumber = PhoneTextBox.Text,
                Role = "customer"
            };

            try
            {
                _context.Users.Add(newUser);
                _context.SaveChanges();

                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                EmailTextBox.Text = "";
                PasswordBox.Password = "";
                FirstNameTextBox.Text = "";
                LastNameTextBox.Text = "";
                PhoneTextBox.Text = "";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoginLink_Click(object sender, RoutedEventArgs e)
        {
            // Код для перехода на окно входа
            // Например:
            // var loginWindow = new LoginWindow();
            // loginWindow.Show();
            // this.Close();
        }
    }
}
