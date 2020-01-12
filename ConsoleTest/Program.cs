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


using System.Net.Http;

namespace ConsoleTest
{
    class Program
    {
        public void Test()
        {




        }

        static void Main(string[] args)
        {
            //Создание базы данных
            Connection c = new Connection();
            c.Database.EnsureCreated();

            //Book b = new Book();
            //b.Name = "test";
            //b.Cover = "test";
            //b.Autor = "Aleksandr aka (MadeInMama)";
            //b.Description = "Test";

            //Book b1 = new Book();
            //b1.Name = "test";
            //b1.Cover = "test";
            //b1.Autor = "Aleksandr aka (MadeInMama)";
            //b1.Description = "";


            Database d = new Database(c, true);

            //d.AddBook(b);
            //d.AddBook(b1);


            //   var arr = c.HistoryRecords.ToArray();

            List<Book> book  = d.GetBooks("Aleksandr aka (MadeInMama)").ToList();

            var history = d.GetBookHistory("7e9205cd-ed8a-46c7-b9e4-f2a3e9d8f661");

            d.UpdateBookByHistory(history.Records, history.Details);


            var value =  d.GetLastBookHistory();

            //MultipartFormDataContent content = new MultipartFormDataContent();
            //content.Add(new StringContent("123"), "token");

            //HttpClient client = new HttpClient();
            //client.PostAsync("http://test.ru/GetBooks", content);
        }
    }
}
