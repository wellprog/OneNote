using System;
using System.Collections.Generic;
using System.Linq;
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

            return GetHistory(nameof(Book), LastID);
        }

        public IEnumerable<Book> GetBooks(string Autor)
        {
            return _connection.Books.Where(f => f.Autor == Autor).ToArray();
        }

        public HistoryModel GetPageHistory(string LastID)
        {
            return GetHistory(nameof(Page), LastID);
        }

        public IEnumerable<Page> GetPages(string Section)
        {
            return _connection.Pages.Where(f => f.Section == Section).ToArray();
        }

        public HistoryModel GetSectionHistory(string LastID)
        {
            return GetHistory(nameof(Section), LastID);
        }

        public IEnumerable<Section> GetSections(string Book)
        {
            return _connection.Sections.Where(f => f.Book == Book).ToArray();
        }

        public void UpdateBook(Book value)
        {
            UpdateModel(value);
        }

        public void UpdateBookByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            UpdateValueByHistory(records, details);
        }

        public void UpdatePage(Page value)
        {
            UpdateModel(value);
        }

        public void UpdatePageByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            UpdateValueByHistory(records, details);
        }

        public void UpdateSection(Section value)
        {
            UpdateModel(value);
        }

        public void UpdateSectionByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            UpdateValueByHistory(records, details);
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
        private HistoryModel GetHistory(string TableName, string LastID)
        {
            HistoryModel historyModel = new HistoryModel();

            historyModel.Records = _connection.HistoryRecords.Where(f => f.Table == TableName && f.RecordID == LastID);
            List<HistoryDetail> details = new List<HistoryDetail>();
            foreach (HistoryRecord rec in historyModel.Records)
            {
                details.AddRange(_connection.HistoryDetails.Where(f => f.HistoryRecord == rec.ID).ToList());
            }
            historyModel.Details = details;
            return historyModel;
        }

        private void UpdateValueByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            foreach (HistoryRecord record in records)
            {
                Type typeElement = Type.GetType(record.Table);

                var element = _connection.Find(typeElement, record.RecordID);
                //   Book book = _connection.Books.Where(f => f.ID == record.RecordID).FirstOrDefault();
                if (element == null) continue;

                IEnumerable<HistoryDetail> _details = details.Where(f => f.HistoryRecord == record.ID);
                foreach (HistoryDetail item in _details)
                {
                    PropertyInfo property = typeElement.GetType().GetProperty(item.Field);
                    property.SetValue(null, item.NewValue);
                }
                _connection.Entry(element).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _connection.SaveChanges();

        }
    }
    }
