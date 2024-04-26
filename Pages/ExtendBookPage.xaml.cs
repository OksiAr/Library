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
    /// Логика взаимодействия для ExtendBookPage.xaml
    /// </summary>
    public partial class ExtendBookPage : Page
    {
        Bookissuance bookissuance;
        public ExtendBookPage(Bookissuance _bookissuance)
        {
            InitializeComponent();
            //bookissuance = _bookissuance;
            //this.DataContext = bookissuance;
            //NewDateRetirnDp.DisplayDateStart = bookissuance.DateOfReturn;
        }

        private void ExtendBtn_Click(object sender, RoutedEventArgs e)
        {
        //    bookissuance.DateOfReturn = NewDateRetirnDp.SelectedDate;
        //    App.db.SaveChanges();
        //    Navigation.BackPage();
        }
    }
}
