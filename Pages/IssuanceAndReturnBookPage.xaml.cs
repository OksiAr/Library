﻿using Library.Components;
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
           IssuanceAndReturnBookList.ItemsSource = App.db.Bookissuances
               .Include(p => p.Reader)
               .Include(p => p.Book)
               .ToList();
           
        }
      
    }
}
