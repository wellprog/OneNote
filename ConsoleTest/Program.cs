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
            //Connection c = new Connection();
            //c.Database.EnsureCreated();

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


            var d = History.GetHistoryFromModel(b1, b, "123");
        }
    }
}
