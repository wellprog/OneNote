using System;
using System.Collections.Generic;
using System.Text;
using OneNote.Communication.Model;
using OneNote.Model;

namespace OneNote.Communication
{
    class Communication : ICommunication
    {
        public string Authorize(string login, string password)
        {
            throw new NotImplementedException();
        }

        public BookModel GetBooks(string token)
        {
            throw new NotImplementedException();
        }

        public HistoryModel GetHistory(string token, string table, int lastID)
        {
            throw new NotImplementedException();
        }

        public PageModel GetPages(string token)
        {
            throw new NotImplementedException();
        }

        public SectionModel GetSections(string token)
        {
            throw new NotImplementedException();
        }

        public User GetUserDetails(string token)
        {
            throw new NotImplementedException();
        }

        public string Register(User user)
        {
            throw new NotImplementedException();
        }

        public string SetBookHistory(string token, HistoryModel history)
        {
            throw new NotImplementedException();
        }

        public string SetHistory(string token, HistoryModel history)
        {
            throw new NotImplementedException();
        }

        public string SetPageHistory(string token, HistoryModel history)
        {
            throw new NotImplementedException();
        }

        public string SetSectionHistory(string token, HistoryModel history)
        {
            throw new NotImplementedException();
        }
    }
}
