using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using OneNote.Model;
using OneNote.SQLite.Helper;
using OneNote.SQLite.Model;

namespace OneNote.SQLite
{
    public class Database : IDatabase
    {
        private readonly Connection _connection;
        Database(Connection connection)
        {
            _connection = connection;
        }

        public void AddBook(Book value)
        {
            _connection.Add(value);
            var history = History.GetHistoryFromModel(null, value, "Autor");
            _connection.Add(history.Details);
            _connection.Add(history.Records);
            _connection.SaveChanges();
        }

        public void AddPage(Page value)
        {
            _connection.Add(value);
            var history = History.GetHistoryFromModel(null, value, "Autor");
            _connection.Add(history.Details);
            _connection.Add(history.Records);
            _connection.SaveChanges();
        }

        public void AddSection(Section value)
        {
            _connection.Add(value);
            var history = History.GetHistoryFromModel(null, value, "Autor");
            _connection.Add(history.Details);
            _connection.Add(history.Records);
            _connection.SaveChanges();
        }

        public HistoryModel GetBookHistory(string LastID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Book> GetBooks(string Autor)
        {
            throw new NotImplementedException();
        }

        public HistoryModel GetPageHistory(string LastID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Page> GetPages(string Section)
        {
            throw new NotImplementedException();
        }

        public HistoryModel GetSectionHistory(string LastID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Section> GetSections(string Book)
        {
            throw new NotImplementedException();
        }

        public void UpdateBook(Book value)
        {
            _connection.Add(value);
            var item = _connection.Books.Where(f => f.ID == value.ID).FirstOrDefault();
            if(item == null)
            {
                AddBook(value);
                return;
            }
            var history = History.GetHistoryFromModel(item, value, "Autor");
            _connection.Add(history.Details);
            _connection.Add(history.Records);
            _connection.SaveChanges();
        }

        public void UpdateBookByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            throw new NotImplementedException();
        }

        public void UpdatePage(Page value)
        {
            _connection.Add(value);
            var item = _connection.Pages.Where(f => f.ID == value.ID).FirstOrDefault();
            if (item == null)
            {
                AddPage(value);
                return;
            }
            var history = History.GetHistoryFromModel(item, value, "Autor");
            _connection.Add(history.Details);
            _connection.Add(history.Records);
            _connection.SaveChanges();
        }

        public void UpdatePageByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            throw new NotImplementedException();
        }

        public void UpdateSection(Section value)
        {
            _connection.Add(value);
            var item = _connection.Sections.Where(f => f.ID == value.ID).FirstOrDefault();
            if (item == null)
            {
                AddSection(value);
                return;
            }
            var history = History.GetHistoryFromModel(item, value, "Autor");
            _connection.Add(history.Details);
            _connection.Add(history.Records);
            _connection.SaveChanges();
        }

        public void UpdateSectionByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            throw new NotImplementedException();
        }
    }
}
