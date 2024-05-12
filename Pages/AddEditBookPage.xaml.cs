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
    /// Логика взаимодействия для AddEditBookPage.xaml
    /// </summary>
    public partial class AddEditBookPage : Page
    {
        Book book;
        public AddEditBookPage(Book _book)
        {
            InitializeComponent();
            book = _book;
            this.DataContext = book;
            //заполнение комбобокса жанрами из базы
            GenreCb.ItemsSource = App.db.Genres.ToList();
            GenreCb.DisplayMemberPath = "Name";
            //заполнение комбобокса авторами из базы
            AuthorCb.ItemsSource = App.db.Authors.ToList();
            AuthorCb.DisplayMemberPath = "FullName";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                //у жанра и автора взять Id
                book.GenreId = (GenreCb.SelectedItem as Genre).Id;
                book.AuthorId = (AuthorCb.SelectedItem as Author).Id;
                //если id книги = 0 значит выполнить добавление новой записи, иначе обновление записи в базе
                if (book.Id == 0)
                    App.db.Books.Add(book);
                //сохранение изменеий в базе данных
                App.db.SaveChanges();
                MessageBox.Show("Операция выполнена успешно!");
                Navigation.BackPage();
            }
            catch
            {
                MessageBox.Show("Заполните поля!");
            }
         
        }

        private void AddAuthorBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Добавление автора", new AddAuthorPage()));
        }

        private void AddGenreBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Добавление жанра", new AddGenrePage()));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GenreCb.ItemsSource = App.db.Genres.ToList();
            GenreCb.DisplayMemberPath = "Name";
            AuthorCb.ItemsSource = App.db.Authors.ToList();
            AuthorCb.DisplayMemberPath = "FullName";
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //в текстбокс год и кол-во копий можно вводить только цифры
            if (!char.IsDigit(char.Parse(e.Text)))
            {
                e.Handled = true;
            }

        }
    }
}
