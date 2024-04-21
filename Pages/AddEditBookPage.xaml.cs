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
            GenreList.ItemsSource = App.db.Genres.ToList();
            AuthorList.ItemsSource = App.db.Authors.ToList();   
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {

            if(book.Id == 0)
                App.db.Books.Add(book);
            
            App.db.SaveChanges();
            MessageBox.Show("Операция выполнена успешно!");
            Navigation.NextPage(new PageComponent("Книги", new BookListPage()));
        }
    }
}
