using Library.Components;
using Library.Models
    ;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Library.Pages
{
    /// <summary>
    /// Логика взаимодействия для BookListPage.xaml
    /// </summary>
    public partial class BookListPage : Page
    {
        public BookListPage()
        {
            InitializeComponent();
            var genres = App.db.Genres.ToList();
            genres.Insert(0, new Genre() { Id = 0, Name = "Все книги" });
            GenrehCb.ItemsSource = genres;
            GenrehCb.DisplayMemberPath = "Name";
            Refresh();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Добавление книги", new AddEditBookPage(new Book())));
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            var selBook = BookList.SelectedItem as Book;
            if(selBook != null)
            {
                Navigation.NextPage(new PageComponent("Изменение книги", new AddEditBookPage(selBook)));
            }
            else
            {
                MessageBox.Show("Выберете книгу для изменения!");
            }
           
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
        public void Refresh()
        {
            IEnumerable<Book> books = App.db.Books
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .ToList();
          
            if (SearchTb.Text.Length > 0)
            {
                books = books.Where(x =>
                x.Name.ToLower().Contains(SearchTb.Text.ToLower())
                || x.Author.FullName.ToLower().Contains(SearchTb.Text.ToLower()));
            }
          
            if (GenrehCb.SelectedIndex > 0)
            {
                var selGenre = GenrehCb.SelectedItem as Genre;
                books = books.Where(x => x.Genre.Name == selGenre.Name);
            }
            BookList.ItemsSource = books;

        }
        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();

        }

        private void GenrehCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
    }
}
