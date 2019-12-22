using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OneNote.Communication.Model;
using OneNote.Model;
using OneNote.Server;
using OneNote.SQLite;

namespace Server.Controllers
{
    public class HomeController : Controller
    {
        private IDatabase _db;
        private Connection _con;
        private string _salt = "somesalt";
        private TokenStorage _tokenStorage;

        public HomeController(TokenStorage tokenStorage)
        {
            Dictionary<string, string> test;

            _tokenStorage = tokenStorage;
            Connection c = new Connection();
            if (c.Database.EnsureCreated())
            {
                _con = c;
                _db = new Database(c);
            }
            else
            {
                throw new Exception("A white little polar animal has come!");
            }
        }

        public ActionResult Authorize(string login, string password)
        {
            var user = _db.GetUserByLoginPassword(login, password);
            if (user == null) return Json("No such user!");
            string token = login + _salt + password;
            if (!_tokenStorage.Tokens.ContainsKey(token))
            {
                _tokenStorage.Tokens.Add(token, user);
            }

            return Json(token);
        }

        public ActionResult Register([FromForm]User user)
        {
            if (_db.IsUserExists(user.UserName)) return Json("User with same name already exitsts!");
            _db.AddUser(user);
            string token = user.UserName + _salt + user.Password;
            _tokenStorage.Tokens.Add(token, user);
            return Json(token);
        }

        public ActionResult/*User*/ GetUserDetails(string token)
        {
            if (_tokenStorage.Tokens.ContainsKey(token))
            {
                return Json(_tokenStorage.Tokens[token]);
            }

            return Json("No token");
        }

        public ActionResult/*BookModel*/ GetBooks(string token)
        {
            User user = GetUserByToken(token);
            if (token == null) return Json("No token");
            var books = GetBooks(user);
            var lastHistory = _db.GetLastBookHistory();
            BookModel result = new BookModel { Books = books.ToList(), LastHistory = lastHistory };
            return Json(result);
        }

        public ActionResult/*SectionModel*/ GetSections(string token)
        {
            User user = GetUserByToken(token);
            if (token == null) return Json("No token");
            var books = GetBooks(user);
            var sections = GetSections(books);
            var lastHistory = _db.GetLastSectionHistory();
            SectionModel result = new SectionModel { Sections = sections.ToList(), LastHistory = lastHistory };
            return Json(result);
        }

        public ActionResult/*PageModel*/ GetPages(string token)
        {
            User user = GetUserByToken(token);
            if (token == null) return Json("No token");

            var books = GetBooks(user);
            var sections = GetSections(books);
            var pages = GetPages(sections);
            var lastHistory = _db.GetLastPageHistory();
            PageModel result = new PageModel { Pages = pages.ToList(), LastHistory = lastHistory };
            return Json(result);
        }

        public ActionResult/*HistoryModel*/ GetHistory(string token, string table, int lastID)
        {
            User user = GetUserByToken(token);
            if (token == null) return Json("No token");
            switch (table)
            {
                case "Books":
                    return Json(_db.GetBookHistory(Convert.ToString(lastID)));
                case "Sections":
                    return Json(_db.GetSectionHistory(Convert.ToString(lastID)));
                case "Pages":
                    return Json(_db.GetPageHistory(Convert.ToString(lastID)));
                default:
                    return Json("No such table");
            }
        }

        public ActionResult/*string*/ SetBookHistory(String token, HistoryModel history)
        {
            _db.UpdateBookByHistory(history.Records, history.Details);
            return Json(_db.GetLastBookHistory());
        }

        public ActionResult/*string*/ SetSectionHistory(String token, HistoryModel history)
        {
            _db.UpdateSectionByHistory(history.Records, history.Details);
            return Json(_db.GetLastSectionHistory());
        }

        public ActionResult/*string*/ SetPageHistory(String token, HistoryModel history)
        {
            _db.UpdatePageByHistory(history.Records, history.Details);
            return Json(_db.GetLastPageHistory());
        }

        private IEnumerable<Book> GetBooks(User user)
        {
            return _db.GetBooks(user.ID);
        }

        private IEnumerable<Section> GetSections(IEnumerable<Book> books)
        {
            List<Section> finalSections = new List<Section>();
            foreach (var book in books)
            {
                var sections = _db.GetSections(book.Name);
                finalSections.AddRange(sections);
            }

            return finalSections;
        }

        private IEnumerable<Page> GetPages(IEnumerable<Section> sections)
        {
            List<Page> finalPages = new List<Page>();
            foreach (var section in sections)
            {
                var pages = _db.GetPages(section.Name);
                finalPages.AddRange(pages);
            }

            return finalPages;
        }

        private User GetUserByToken(string token)
        {
            if (!_tokenStorage.Tokens.ContainsKey(token)) return null;
            return _tokenStorage.Tokens[token];
        }
    }
}