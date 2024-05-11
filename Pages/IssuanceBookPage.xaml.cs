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
            //заполнение комбобокса книгами из базы(вывод только наименования)
            BookCb.ItemsSource = App.db.Books.Where(x=> x.CountCopies > 0).ToList();
            BookCb.DisplayMemberPath = "Name";

            //заполнение комбобокса читателями из базы
            ReaderCb.ItemsSource = App.db.Readers.ToList();
            ReaderCb.DisplayMemberPath = "FullName";

            //заполнение комбобокса номерами читательских карт из базы
            NumberCb.ItemsSource = App.db.Readers.ToList();
            NumberCb.DisplayMemberPath = "NumberLibraryCard";

            //автоматическое заполнение даты выдачи текущей датой
            DateIssueDp.SelectedDate = DateTime.Now;
            //автоматическое заполнение даты возврата(текущая дата  + 30 дней)
            DateReturnDp.SelectedDate = DateTime.Now.AddDays(30);
          
        }

        private void IssueBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //проверка заполнены ли все поля
                if (BookCb.SelectedItem == null
                    || ReaderCb.SelectedItem == null)
                {
                    MessageBox.Show("Заполните все поля!");
                }
                else
                {
                    //создание новой записи о выданной книге
                    Bookissuance bookissuance = new Bookissuance()
                    {
                        BookId = (BookCb.SelectedItem as Book).Id,
                        ReaderNumberLibraryCard = (ReaderCb.SelectedItem as Reader).NumberLibraryCard,
                        DateOfIssue = DateTime.Now,
                        DateOfReturn = DateTime.Now.AddDays(30)
                    };
                    App.db.Bookissuances.Add(bookissuance);

                    //после выдачи -1 копия в наличии библиотеки
                    var book = App.db.Books.FirstOrDefault(x => x.Id == (BookCb.SelectedItem as Book).Id);
                    book.CountCopies -= 1;
                    //сохрание изменений в базу данных
                    App.db.SaveChanges();
                    //возврат на предыдущую страницу
                    Navigation.BackPage();
                }
            }
            catch 
            {
                MessageBox.Show("Возникла ошибка");
            }    
           
        }

        private void ReaderCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selReader = ReaderCb.SelectedItem as Reader;
            NumberCb.SelectedItem = selReader;
        }

        private void NumberCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selReader = NumberCb.SelectedItem as Reader;
            ReaderCb.SelectedItem = selReader;
        }
    }
}
