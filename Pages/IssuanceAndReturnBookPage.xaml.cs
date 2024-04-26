using Library.Components;
using Library.Models;
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
    /// Логика взаимодействия для IssuanceAndReturnBookPage.xaml
    /// </summary>
    public partial class IssuanceAndReturnBookPage : Page
    {
        public IssuanceAndReturnBookPage()
        {
            InitializeComponent();
            Refresh();

        }
        private void Refresh()
        {
            if(App.AuthUser.RoleId == 1)
            {
                IssuanceAndReturnBookList.ItemsSource = App.db.Bookissuances
               .Include(p => p.Reader)
               .Include(p => p.Book)
               .ToList();
            }
            else
            {
                IssuanceAndReturnBookList.ItemsSource = App.db.Bookissuances
               .Include(p => p.Reader)
               .Include(p => p.Book)
               .Where(x => x.ReaderNumberLibraryCard == App.AuthUser.ReaderNumberCard)
               .ToList();
            }
           
        }
        private void ExtendBtn_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Button).DataContext as Bookissuance;
            Navigation.NextPage(new PageComponent("Продление книги", new ExtendBookPage(select)));
        }

        private void ReturnBookBtn_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Button).DataContext as Bookissuance;
            App.db.Bookissuances.Remove(select);
            var returnBook = App.db.Books.FirstOrDefault(x => x.Id == select.BookId);
            returnBook.CountCopies += 1;
            App.db.SaveChanges();
            Refresh();

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Выдача книги", new IssuanceBookPage()));
        }
    }
}
