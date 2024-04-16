using Library.Components;
using Library.DataBases;
using Library.Pages;
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

namespace Library
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Navigation.mainWindow = this;
           Navigation.NextPage(new PageComponent("Авторизация",new AuthorizationPage()));
          
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.BackPage();
        }

        private void ForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoForward)
                MainFrame.GoForward();

        }

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            App.isAdmin = false;
            Navigation.NextPage(new PageComponent("Авторизация", new AuthorizationPage()));
        }
    }
}
