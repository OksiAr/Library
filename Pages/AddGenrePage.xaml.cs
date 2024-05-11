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
    /// Логика взаимодействия для AddGenrePage.xaml
    /// </summary>
    public partial class AddGenrePage : Page
    {
        public AddGenrePage()
        {
            InitializeComponent();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            //добавление нового жанра
            App.db.Genres.Add(new Models.Genre() { Name = NameTb.Text });
            App.db.SaveChanges();
            Navigation.BackPage();
        }

        private void NameTb_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //в текстбокс можно вводить только буквы
            if (char.IsDigit(char.Parse(e.Text)))
            {
                e.Handled = true;
            }

        }
    }
}
