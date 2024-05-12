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
            try
            {
                var genres = App.db.Genres.ToList();
                genres.Insert(0, new Genre() { Id = 0, Name = "Все книги" });
                GenrehCb.ItemsSource = genres;
                GenrehCb.DisplayMemberPath = "Name";
                Refresh();
                //если авторизован не админ скрыть кнопки дабвить, изменить, удалить
                if (App.AuthUser.RoleId != 1)
                {
                    AddBtn.Visibility = Visibility.Collapsed;
                    EditBtn.Visibility = Visibility.Collapsed;
                    DeleteBtn.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Добавление книги", new AddEditBookPage(new Book())));
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            //если книга выбрана из списка, открыть окно редактирования
            var selBook = BookList.SelectedItem as Book;
            if (selBook != null)
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
            try
            {
                //если книга выбрана из списка
                var selBook = BookList.SelectedItem as Book;
                if (selBook != null)
                {
                    //откравать окно с кнопка да, нет
                    MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить одну копию книги \"{selBook.Name}\"?", "Удаление одной копии", MessageBoxButton.YesNo);
                    if (selBook.CountCopies == 0 && App.db.Bookissuances.Any(x => x.BookId == selBook.Id))
                    {
                        MessageBox.Show("Удаление невозможно, данные копии на руках у читателей");
                        return;
                    }
                    //если нажато да, выполнить перемещение одной копии в архив
                    if (result == MessageBoxResult.Yes)
                    {
                        //если в архиве есть уже такая книга, к количесву копий прибавляем 1
                        var bookA = App.db.Bookarchives.FirstOrDefault(x => x.BookName == selBook.Name);
                        if (bookA != null)
                        {
                            bookA.CountCopies += 1; //прибавляем 1
                        }
                        else
                        { // иначе создаем новую книгу для архива и добавляем ее в таблицу архив
                            Bookarchive bookarchive = new Bookarchive()
                            {
                                BookId = selBook.Id,
                                BookName = selBook.Name,
                                AuthorId = selBook.AuthorId,
                                AuthorFullName = selBook.Author.FullName,
                                GenreId = selBook.GenreId,
                                GenreName = selBook.Genre.Name,
                                PublihingHouse = selBook.PublihingHouse,
                                Year = selBook.Year,
                                CountCopies = 1
                            };

                            App.db.Bookarchives.Add(bookarchive);
                        }

                        // если количесво копий !=0 отнимаем -1 иначе полностью удаляем книгу из базы в архив
                        if (selBook.CountCopies != 0)
                            selBook.CountCopies -= 1;
                        else
                            App.db.Books.Remove(selBook);
                        App.db.SaveChanges();
                        Refresh();
                    }
                }
                else
                {
                    MessageBox.Show("Выберете книгу для удаления!");
                }
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
                IEnumerable<Book> books = App.db.Books
                               .Include(b => b.Author)
                               .Include(b => b.Genre)
                               .ToList();
                //если что-то введено в поле поиск выполни поиск по наименованию или автору
                if (SearchTb.Text.Length > 0)
                {
                    books = books.Where(x =>
                    x.Name.ToLower().Contains(SearchTb.Text.ToLower())
                    || x.Author.FullName.ToLower().Contains(SearchTb.Text.ToLower()));
                }
                //если выбран какой-то жанр отбери книги с этим жанром
                if (GenrehCb.SelectedIndex > 0)
                {
                    var selGenre = GenrehCb.SelectedItem as Genre;
                    books = books.Where(x => x.Genre.Name == selGenre.Name);
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


    }
}
