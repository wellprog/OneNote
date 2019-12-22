using System;
using System.Collections.Generic;
using System.Text;
using OneNote.Communication.Model;
using OneNote.Model;
using System.Linq;

namespace OneNote.Communication
{
    public class LocalCommunication : ICommunication
    {
        SQLite.Database testDB;
        User _currentUser = null;

        public LocalCommunication()
        {
            testDB = new SQLite.Database(new SQLite.Connection("test.db"));
        }

        public string Authorize(string login, string password)
        {
            var user = testDB.GetUserByLoginPassword(login, password);
            if (user == null)
            {
                return "";
            }

            _currentUser = user;

            return "123";
        }

        public BookModel GetBooks(string token)
        {
            if (!CheckAccess(token)) return null;

            return new BookModel
            {
                Books = testDB.GetBooks(_currentUser.ID).ToList(),
                LastHistory = testDB.GetLastBookHistory()
            };
        }

        public HistoryModel GetHistory(string token, string table, int lastID)
        {
            if (!CheckAccess(token)) return null;

            var data = testDB.GetHistory(table, lastID.ToString());
            return new HistoryModel()
            {
                Records = data.Records.ToList(),
                Details = data.Details.ToList()
            };
        }

        public PageModel GetPages(string token)
        {
            if (!CheckAccess(token)) return null;

            return new PageModel
            {
                Pages = testDB.GetPages(_currentUser.ID).ToList(),
                LastHistory = testDB.GetLastPageHistory()
            };
        }

        public SectionModel GetSections(string token)
        {
            if (!CheckAccess(token)) return null;

            return new SectionModel
            {
                Sections = testDB.GetSections(_currentUser.ID).ToList(),
                LastHistory = testDB.GetLastSectionHistory()
            };
        }

        public User GetUserDetails(string token)
        {
            if (!CheckAccess(token)) return null;

            return _currentUser;
        }

        public string Register(User user)
        {
            var data = testDB.AddUser(user);
            if (data)
                return "123";
            return "";
        }

        public string SetBookHistory(string token, HistoryModel history)
        {
            if (!CheckAccess(token)) return null;

            testDB.UpdateBookByHistory(history.Records, history.Details);

            return testDB.GetLastBookHistory();
        }

        public string SetPageHistory(string token, HistoryModel history)
        {
            if (!CheckAccess(token)) return null;

            testDB.UpdatePageByHistory(history.Records, history.Details);

            return testDB.GetLastPageHistory();
        }

        public string SetSectionHistory(string token, HistoryModel history)
        {
            if (!CheckAccess(token)) return null;

            testDB.UpdateSectionByHistory(history.Records, history.Details);

            return testDB.GetLastSectionHistory();
        }



        private bool CheckAccess(string token)
        {
            if (token != "123")
                return false;
            if (_currentUser == null)
                return false;

            return true;
        }
    }
}
