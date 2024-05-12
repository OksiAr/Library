using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Library.Components
{
    public static class HelperExcel
    {
        //Путь Excel файла
        private static string filePath = @"C:\Users\oksan\OneDrive\Рабочий стол\bookArchive.xlsx";

        public static async Task ExportExcel(Bookarchive[]  bookArchives)
        {
            //Установка лицензии для библиотеки - не коммерческая лицензия
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            //Создание нового Excel файла
            ExcelPackage excel = new ExcelPackage();

            //Создание странички в Excel файле
            var workSheet = excel.Workbook.Worksheets.Add("Sheet1");

            //Добавление колонок на страницу
            workSheet.Cells[1, 1].Value = "№";
            workSheet.Cells[1, 2].Value = "№ книги";
            workSheet.Cells[1, 3].Value = "Название книги";
            workSheet.Cells[1, 4].Value = "№ автора";
            workSheet.Cells[1, 5].Value = "ФИО автора";
            workSheet.Cells[1, 6].Value = "№ жанра";
            workSheet.Cells[1, 7].Value = "Название жанра";
            workSheet.Cells[1, 8].Value = "Издательство";
            workSheet.Cells[1, 9].Value = "Год";
            workSheet.Cells[1, 10].Value = "Количество копий";
            //Индекс строки, которая каждый раз увеличивается на 1
            var startIndex = 2;
            foreach (var bookArchive in bookArchives)
            {
                //Запись каждого объекта в виде каждой строки
                workSheet.Cells[startIndex, 1].Value = bookArchive.Id.ToString();
                workSheet.Cells[startIndex, 2].Value = bookArchive.BookId.ToString();
                workSheet.Cells[startIndex, 3].Value = bookArchive.BookName;
                workSheet.Cells[startIndex, 4].Value = bookArchive.AuthorId.ToString();
                workSheet.Cells[startIndex, 5].Value = bookArchive.AuthorFullName;
                workSheet.Cells[startIndex, 6].Value = bookArchive.GenreId.ToString();
                workSheet.Cells[startIndex, 7].Value = bookArchive.GenreName;
                workSheet.Cells[startIndex, 8].Value = bookArchive.PublihingHouse;
                workSheet.Cells[startIndex, 9].Value = bookArchive.Year.ToString();
                workSheet.Cells[startIndex, 10].Value = bookArchive.CountCopies.ToString();

                //Увеличение индекса строки, чтобы следующий объект записался на следующую строку.
                startIndex++;
            }

            //Вызов AutoFit чтобы колонки стали по ширине текста внутри колонки
            workSheet.Column(1).AutoFit();
            workSheet.Column(2).AutoFit();
            workSheet.Column(3).AutoFit();
            workSheet.Column(4).AutoFit();
            workSheet.Column(5).AutoFit();
            workSheet.Column(6).AutoFit();
            workSheet.Column(7).AutoFit();
            workSheet.Column(8).AutoFit();
            workSheet.Column(9).AutoFit();
            workSheet.Column(10).AutoFit();

            //Если уже есть такой файл, удаляем существующий файл
            if (File.Exists(filePath))
                File.Delete(filePath);

            //Создаём файл
            FileStream objFileStrm = File.Create(filePath);
            objFileStrm.Close();

            //Записываем в Excel файл данные
            File.WriteAllBytes(filePath, excel.GetAsByteArray());

            //Очищаем ресурсы
            excel.Dispose();
        }
    }
}
 