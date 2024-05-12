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
    /// Логика взаимодействия для BookArchivePage.xaml
    /// </summary>
    public partial class BookArchivePage : Page
    {
        public BookArchivePage()
        {
            InitializeComponent();
            try
            {
                var genres = App.db.Genres.ToList();
                genres.Insert(0, new Genre() { Id = 0, Name = "Все книги" });
                GenrehCb.ItemsSource = genres;
                GenrehCb.DisplayMemberPath = "Name";
                Refresh(); 
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }
        //метод организующий выборку из списка( фильткацию и поиск)
        public void Refresh()
        {
            try
            {
                IEnumerable<Bookarchive> books = App.db.Bookarchives.ToList();
                //если что-то введено в поле поиск выполни поиск по наименованию или автору
                if (SearchTb.Text.Length > 0)
                {
                    books = books.Where(x =>
                    x.BookName.ToLower().Contains(SearchTb.Text.ToLower())
                    || x.AuthorFullName.ToLower().Contains(SearchTb.Text.ToLower()));
                }
                //если выбран какой-то жанр отбери книги с этим жанром
                if (GenrehCb.SelectedIndex > 0)
                {
                    var selGenre = GenrehCb.SelectedItem as Genre;
                    books = books.Where(x => x.GenreName == selGenre.Name);
                }
                //заполнение списка фильтрованными данными
                BookList.ItemsSource = books;
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }


        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void GenrehCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();

        }

        private void ExportExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            HelperExcel.ExportExcel(App.db.Bookarchives.ToArray());
            MessageBox.Show("Excel файл \"BookArchive\" на рабочем столе");
        }
    }
}
