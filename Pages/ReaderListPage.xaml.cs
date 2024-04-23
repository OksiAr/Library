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
    /// Логика взаимодействия для ReaderListPage.xaml
    /// </summary>
    public partial class ReaderListPage : Page
    {
        public ReaderListPage()
        {
            InitializeComponent();
            Refresh();
        
        }
        public void Refresh()
        {
            IEnumerable<Reader> readers =App.db.Readers.ToList();
  
            if (SearchTb.Text.Length > 0)
            {
                readers = readers.Where(x =>
                x.FullName.ToLower().Contains(SearchTb.Text.ToLower()) || x.NumberLibraryCard.ToString().Contains(SearchTb.Text));  
            }
           
            ReaderList.ItemsSource = readers;
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
