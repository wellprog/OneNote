using OneNote.Communication;
using OneNote.Model;
using System;
using System.Collections.Generic;

namespace ServerTests
{
    class Program
    {
        static List<Book> books = new List<Book>();
        static List<Section> sections = new List<Section>();
        static List<Page> pages = new List<Page>();

        static User user = new User();
        static string URL = "http://localhost:58591/Home";

        static void Main(string[] args)
        {
            //1 - register user and get token;

            //var token = RegisterUser();

            //if (string.IsNullOrWhiteSpace(token))
            //{
            //    Console.Write("Ups error register");
            //    return;
            //}

            //2 - login user

            var token = AuthorizeUser("User 1", "password");

            if (string.IsNullOrWhiteSpace(token))
            {
                Console.Write("Ups error register");
                return;
            }
            //3 Get current user


            Console.WriteLine("Hello World!");
        }


        static string RegisterUser()
        {
            user.Age = 123;
            user.Avatar = "Avatar";
            user.CreateTime = DateTime.Now;
            user.EMail = "test@test.com";
            user.Gender = true;
            user.IsDeleted = false;
            user.Password = "password";
            user.Phone = "911";
            user.Status = "status";
            user.UpdateTime = DateTime.Now;
            user.UserName = "User 1";
            var client = new Client(URL);
            var str = client.Register(user);
            return str;
        }

        static string AuthorizeUser(string login,string password)
        {
            var client = new Client(URL);
            var str = client.Authorize(login, password);
            return str;
        }
    }
}
