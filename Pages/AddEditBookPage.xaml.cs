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
            GenreCb.ItemsSource = App.db.Genres.ToList();
            GenreCb.DisplayMemberPath = "Name";
            AuthorCb.ItemsSource = App.db.Authors.ToList();
            AuthorCb.DisplayMemberPath = "FullName";
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            book.GenreId = (GenreCb.SelectedItem as Genre).Id;
            book.AuthorId = (AuthorCb.SelectedItem as Author).Id;
            if (book.Id == 0)
                App.db.Books.Add(book);

            App.db.SaveChanges();
            MessageBox.Show("Операция выполнена успешно!");
            Navigation.NextPage(new PageComponent("Книги", new BookListPage()));
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
    }
}
