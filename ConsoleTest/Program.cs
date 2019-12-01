using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OneNote.Model;
using OneNote.SQLite;
using OneNote.SQLite.Helper;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //Создание базы данных
            Connection c = new Connection();
            c.Database.EnsureCreated();

            Book b = new Book();
            b.Name = "test";
            b.Cover = "test";
            b.Autor = "Aleksandr aka (MadeInMama)";
            b.Description = "Test";

            Book b1 = new Book();
            b1.Name = "test";
            b1.Cover = "test";
            b1.Autor = "Aleksandr aka (MadeInMama)";
            b1.Description = "";


            Database d = new Database(c);

            d.AddBook(b);
            d.AddBook(b1);


            var arr = c.HistoryRecords.ToArray();


            d.GetBookHistory("0e5c2067-3a28-4b66-8c96-4d974f471b7d");
        }
    }
}
