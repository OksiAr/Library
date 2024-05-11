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
            try
            {
                //найти в таблице USER пользователя с данным логином и паролем
                App.AuthUser = App.db.Users.FirstOrDefault(x => x.Login == LoginTb.Text && x.Password == PasswordPb.Password);
                //если пользователь найден
                if (App.AuthUser != null)
                {
                    //переменная авторизован? становится истинным, на окне появится кнопка Выход
                    App.isAuth = true;
                    //если у найденного пользователя роль=1 открыть меню администрартора
                    if (App.AuthUser.RoleId == 1)
                    {
                        Navigation.NextPage(new PageComponent("Меню администратора", new AdminMenuPage()));
                    }
                    else //иначе меню читптеля
                    {
                        Navigation.NextPage(new PageComponent("Меню читателя", new ReaderMenuPage()));
                    }
                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует!!!");
                }
            }
            catch 
            {
                MessageBox.Show("Возникла ошибка!");
            }   
        }
        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Регистрация читателя", new RegistrationPage(new User())));
        }

        private void OpenBookList_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Книги", new BookListPage()));
        }
    }
}
