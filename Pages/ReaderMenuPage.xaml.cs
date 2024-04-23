using Library.Components;
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
    /// Логика взаимодействия для ReaderMenuPage.xaml
    /// </summary>
    public partial class ReaderMenuPage : Page
    {
        public ReaderMenuPage()
        {
            InitializeComponent();
        }

        private void BookListBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Книги", new BookListPage()));
        }

        private void BookIssuancetBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Взятые книги", new IssuanceAndReturnBookPage()));
        }
    }
}
