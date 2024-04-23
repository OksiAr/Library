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
            var hasMinimum8Chars = new Regex(@".{8,}");
            if (hasNumber.IsMatch(PasswordTb.Password))
            {
                if (hasUpperChar.IsMatch(PasswordTb.Password))
                {
                    if (hasMinimum8Chars.IsMatch(PasswordTb.Password))
                    {
                        if (PasswordTb.Password == PasswordTwoTb.Password)
                        {
                            App.db.Users.Add(new User()
                            {
                                Login = LoginTb.Text,
                                Password = PasswordTb.Password,
                                RoleId = 2
                            });
                            App.db.SaveChanges();
                            MessageBox.Show("Регистрация прошла успешно");
                            Navigation.BackPage();
                        }
                        else
                        {
                            ValidPasswordTb.Text = "Пароли не совпадают!";
                        }
                    }
                    else
                    {
                        ValidPasswordTb.Text = "Пароль дложен состоять минимум из 8 символов!";
                    }
                }
                else
                {
                    ValidPasswordTb.Text = "Пароль дложен содержать заглавную букву!";
                }
            }
            else
            {
                ValidPasswordTb.Text = "Пароль дложен содержать число!";
            }

        }
    }
}
