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
    /// Логика взаимодействия для BookIssuanceArchivePage.xaml
    /// </summary>
    public partial class BookIssuanceArchivePage : Page
    {
        public BookIssuanceArchivePage()
        {
            InitializeComponent();
            Refresh();
        }
        //метод обновления списка
        private void Refresh()
        {
            try
            {
                IEnumerable<Bookissuancearchive> bookissuancesarchive;

                bookissuancesarchive = App.db.Bookissuancearchives.ToList();
               
                //если что-то введено в поле поиск, из списка отбирать или по ФИО, или по номеру читательского, или по наименованию книги
                if (SearchTb.Text.Length > 0)
                {
                    bookissuancesarchive = bookissuancesarchive.Where(x =>
                    x.FullNameReader.ToLower().Contains(SearchTb.Text.ToLower())
                    || x.ReaderNumberLibraryCard.ToString().Contains(SearchTb.Text)
                    || x.BookName.ToLower().Contains(SearchTb.Text.ToLower()));
                }

                ArchiveReturnBookList.ItemsSource = bookissuancesarchive;

            }
            catch
            {
                MessageBox.Show("Возникла ошибка");
            }

        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
