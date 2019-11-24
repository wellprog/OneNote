using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OneNote.Model;
using OneNote.SQLite.Helper;
using OneNote.SQLite.Model;
using System.Linq;

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
            InsertModel(value);
        }

        public void AddPage(Page value)
        {
            InsertModel(value);
        }

        public void AddSection(Section value)
        {
            InsertModel(value);
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
            UpdateModel(value);
        }

        public void UpdateBookByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            throw new NotImplementedException();
        }

        public void UpdatePage(Page value)
        {
            UpdateModel(value);
        }

        public void UpdatePageByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            throw new NotImplementedException();
        }

        public void UpdateSection(Section value)
        {
            UpdateModel(value);
        }

        public void UpdateSectionByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            throw new NotImplementedException();
        }

        private void InsertHistory(HistoryModel model)
        {
            _connection.Add(model.Records);
            _connection.Add(model.Details);

            _connection.SaveChanges();
        }

        private void InsertModel<T>(T model) where T : Base
        {
            _connection.Add(model);
            _connection.SaveChanges();

            InsertHistory(History.GetHistoryFromModel(null, model, model.ID));
        }

        private void UpdateModel<T>(T model) where T : Base
        {
            var element = _connection.Find(model.GetType(), new object[] { model.ID });
            if (element == null)
            {
                InsertModel(model);
                return;
            }

            _connection.Entry(element).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            _connection.Update(model);

            InsertHistory(History.GetHistoryFromModel(element as T, model, model.ID));
        }
    }
}
