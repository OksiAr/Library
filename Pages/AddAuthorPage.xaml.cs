using Library.Components;
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
    /// Логика взаимодействия для AddAuthorPage.xaml
    /// </summary>
    public partial class AddAuthorPage : Page
    {
        public AddAuthorPage()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(FirsNameTb.Text) || string.IsNullOrWhiteSpace(LastNameTb.Text))
            {
                MessageBox.Show("Заполните поля!");
                return;
;           }
            //добавление нового автора в базу данных
            App.db.Authors.Add(new Models.Author() { Firstname = FirsNameTb.Text, Lastname = LastNameTb.Text, Patronymic = PatronymicTb.Text });
            //сохранение в базу данных
            App.db.SaveChanges();
            //переход на предыдущую страницу
            Navigation.BackPage();
        }

      

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //в текстбокс можно вводить только буквы
            if (char.IsDigit(char.Parse(e.Text)))
            {
                e.Handled = true;
            }

        }
    }
}
