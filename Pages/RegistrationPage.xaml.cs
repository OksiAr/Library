using Library.Components;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinimum6Chars = new Regex(@".{6,}");
            var PhoneRegex = new Regex(@"^(\+7|8|7)\d{10}$");

            if (string.IsNullOrWhiteSpace(LastNameTb.Text)
                || string.IsNullOrWhiteSpace(FirsNameTb.Text)
                || string.IsNullOrWhiteSpace(AddressTb.Text)
                || string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                ValidTb.Text = "Заполните все поля!";
                return;
            }

            if (!PhoneRegex.IsMatch(PhoneTb.Text))
            {
                ValidTb.Text = "Неверный номер!";
                return;
            }

            if (App.db.Users.Any(x => x.Login == LoginTb.Text))
            {
                ValidTb.Text = "Логин занят";
                return;
            }

            if (!hasNumber.IsMatch(PasswordTb.Password))
            {
                ValidTb.Text = "Пароль дложен содержать цифру!";
                return;
            }
            if (!hasUpperChar.IsMatch(PasswordTb.Password))
            {
                ValidTb.Text = "Пароль дложен содержать заглавную букву!";
                return;
            }
            if (!hasMinimum6Chars.IsMatch(PasswordTb.Password))
            {
                ValidTb.Text = "Пароль дложен состоять минимум из 6 символов!";
                return;
            }
            if (PasswordTb.Password == PasswordTwoTb.Password)
            {
                ValidTb.Text = "Пароли не совпадают!";
                return;
            }

            ValidTb.Text = "";
            var reader = App.db.Readers.Add(new Reader()
            {
                Lastname = LastNameTb.Text,
                Firstname = FirsNameTb.Text,
                Patronymic = PatronymicTb.Text,
                Address = AddressTb.Text,
                Phone = PhoneTb.Text
            });
            App.db.SaveChanges();
            App.db.Users.Add(new User()
            {
                Login = LoginTb.Text,
                Password = PasswordTb.Password,
                ReaderNumberCard = reader.Entity.NumberLibraryCard,
                RoleId = 2
            });
            App.db.SaveChanges();
            MessageBox.Show("Регистрация прошла успешно");
            Navigation.BackPage();
        }
    }
}

