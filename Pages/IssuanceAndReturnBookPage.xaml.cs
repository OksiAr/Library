using Google.Protobuf.Reflection;
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
        //метод обновления списка
        private void Refresh()
        {
            try
            {
                IEnumerable<Bookissuance> bookissuances;
                //если зашел не админ кнопку добавления скрыть
                if (App.AuthUser.RoleId != 1)
                {
                    AddBtn.Visibility = Visibility.Collapsed;
                }
                //если возел админ выводить все записи
                if (App.AuthUser.RoleId == 1)
                {
                    bookissuances = App.db.Bookissuances
                   .Include(p => p.Reader)
                   .Include(p => p.Book)
                   .ToList();
                }//если вощел читатель выводить только его взятые книги
                else
                {
                    bookissuances = App.db.Bookissuances
                   .Include(p => p.Reader)
                   .Include(p => p.Book)
                   .Where(x => x.ReaderNumberLibraryCard == App.AuthUser.ReaderNumberCard)
                   .ToList();
                }
                //если что-то введено в поле поиск, из списка отбирать или по ФИО, или по номеру читательского, или по наименованию книги
                if (SearchTb.Text.Length > 0)
                {
                    bookissuances = bookissuances.Where(x =>
                    x.Reader.FullName.ToLower().Contains(SearchTb.Text.ToLower())
                    || x.Reader.NumberLibraryCard.ToString().Contains(SearchTb.Text)
                    || x.Book.Name.ToLower().Contains(SearchTb.Text.ToLower()));
                }

                IssuanceAndReturnBookList.ItemsSource = bookissuances;

            }
            catch
            {
                MessageBox.Show("Возникла ошибка");
            }

        }
        private void ExtendBtn_Click(object sender, RoutedEventArgs e)
        {
            var select = (sender as Button).DataContext as Bookissuance;
            Navigation.NextPage(new PageComponent("Продление книги", new ExtendBookPage(select)));
        }

        private void ReturnBookBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //при возврате запись из таблицы возвраткинги перемещается в возврат кинги архив
                //отслеживаем на какой записи была нажата кнопка возврат
                var select = (sender as Button).DataContext as Bookissuance;
                //сохдаем новый обьект типа bookissuancearchive (архив)
                Bookissuancearchive bookissuancearchive = new Bookissuancearchive()
                {
                    BookId = select.BookId,
                    BookName = select.Book.Name,
                    DateOfIssue = select.DateOfIssue,
                    DateOfReturn = DateTime.Now,
                    ReaderNumberLibraryCard = select.ReaderNumberLibraryCard,
                    FullNameReader = select.Reader.FullName
                };
                //удаление записи из таблицы Bookissuances
                App.db.Bookissuances.Remove(select);
                //добавление записи в архивную таблицу
                App.db.Bookissuancearchives.Add(bookissuancearchive);
                //после возврата количесво копий данной книги +1
                var returnBook = App.db.Books.FirstOrDefault(x => x.Id == select.BookId);
                returnBook.CountCopies += 1;
                //сохранение изменений в базе
                App.db.SaveChanges();
                Refresh();
            }
            catch
            {
                MessageBox.Show("Возникла ошибка");
            }

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Выдача книги", new IssuanceBookPage()));
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
