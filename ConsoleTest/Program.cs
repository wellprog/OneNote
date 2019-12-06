using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            var history = d.GetBookHistory("3cd02520-d7aa-484b-8691-5bccdbca512a");

            d.UpdateBookByHistory(history.Records, history.Details);
        }
    }
}
