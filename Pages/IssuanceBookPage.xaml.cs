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
    /// Логика взаимодействия для IssuanceBookPage.xaml
    /// </summary>
    public partial class IssuanceBookPage : Page
    {
        public IssuanceBookPage()
        {
            InitializeComponent();
            BookCb.ItemsSource = App.db.Books.ToList();
            BookCb.DisplayMemberPath = "Name";
           
            ReaderCb.ItemsSource = App.db.Readers.ToList();
            ReaderCb.DisplayMemberPath = "FullName";
           
            DateReturnDp.SelectedDate = DateTime.Now.AddDays(30);
            DateIssueDp.SelectedDate = DateTime.Now;
        }

        private void IssueBtn_Click(object sender, RoutedEventArgs e)
        {
            Bookissuance bookissuance = new Bookissuance()
            {
                BookId = (BookCb.SelectedItem as Book).Id,
                ReaderNumberLibraryCard = (ReaderCb.SelectedItem as Reader).NumberLibraryCard,
                DateOfIssue = DateTime.Now,
                DateOfReturn = DateTime.Now.AddDays(30)
            };
            App.db.Bookissuances.Add(bookissuance);
            App.db.SaveChanges();
            Navigation.BackPage();
        }
    }
}
