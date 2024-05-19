﻿using Library.Components;
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
    //    / <summary>
    //    / Логика взаимодействия для ReaderListPage.xaml
    //    / </summary>
    public partial class ReaderListPage : Page
    {
        public ReaderListPage()
        {
            InitializeComponent();
            Refresh();

        }

        // метод для обновления списка после выполнения каких либо действий с ним
        public void Refresh()
        {
            IEnumerable<Reader> readers = App.db.Readers.ToList();
            
            //если что-то ввели в поле для поиска, выполняется выборка по ФИО или номеру читательского
            if (SearchTb.Text.Length > 0)
            {
                readers = readers.Where(x =>
                x.FullName.ToLower().Contains(SearchTb.Text.ToLower()) || x.NumberLibraryCard.ToString().Contains(SearchTb.Text));
            }
            //заполнение списка
            ReaderList.ItemsSource = readers;
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new PageComponent("Регистрация читателя", new RegistrationPage(new User())));
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            //если что-то выбрано из списка открыть окно редактирования
            var selReader = ReaderList.SelectedItem as Reader;
            if (selReader != null)
            {
                //на окно редактирования передать выбранного пользователя
                Navigation.NextPage(new PageComponent("Редактирование читателя", new RegistrationPage(App.db.Users.FirstOrDefault(x=> x.ReaderNumberCard == selReader.NumberLibraryCard))));
            }
            else
            {
                MessageBox.Show("Выберите чичтателя из списка!");
            }

        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //если что-то выбрано из списка открыть окно с кнопками да, нет , для удаления
                var selReader = ReaderList.SelectedItem as Reader;
                if (selReader != null)
                {
                    MessageBoxResult result = MessageBox.Show($"Вы действительно хотите удалить читателя \"{selReader.FullName}\"?", "Удаление читателя", MessageBoxButton.YesNo);
                    if (App.db.Bookissuances.Any(x => x.ReaderNumberLibraryCard == selReader.NumberLibraryCard))
                    {
                        MessageBox.Show("Невозможно удалить читателя!");
                        return;
                    }
                    App.db.Readers.Remove(selReader);
                    App.db.SaveChanges();
                }
                else
                {
                    MessageBox.Show("Выберите чичтателя из списка!");
                }

            }
            catch
            {
                MessageBox.Show("Возникла ошибка!");
            }
        }
    }
}
