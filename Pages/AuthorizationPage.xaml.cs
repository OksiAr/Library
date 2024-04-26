using Library.Components;
using Library.Models;
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

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void EntryBtn_Click(object sender, RoutedEventArgs e)
        {

            App.AuthUser = App.db.Users.FirstOrDefault(x => x.Login == LoginTb.Text && x.Password == PasswordPb.Password);

            if (App.AuthUser != null)
            {
                App.isAuth = true;
                if (App.AuthUser.RoleId == 1)
                {
                    Navigation.NextPage(new PageComponent("Меню администратора", new AdminMenuPage()));
                }
                else
                {
                    Navigation.NextPage(new PageComponent("Меню читателя", new ReaderMenuPage()));
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует!!!");
            }


        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Регистрация читателя", new RegistrationPage()));
        }
    }
}
