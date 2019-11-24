using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OneNote.Model;
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

            HistoryRecord record = new HistoryRecord();
            record.Autor = value.Autor;
            record.Table = nameof(Book);
            record.RecordID = value.ID;

            _connection.Add(record);

            HistoryDetail detail = new HistoryDetail();
            detail.HistoryRecord = record.ID;
            detail.Field = nameof(value.Name);
            detail.NewValue = value.Name;
            _connection.Add(detail);

            detail = new HistoryDetail();
            detail.HistoryRecord = record.ID;
            detail.Field = nameof(value.Cover);
            detail.NewValue = value.Cover;
            _connection.Add(detail);

            detail = new HistoryDetail();
            detail.HistoryRecord = record.ID;
            detail.Field = nameof(value.Autor);
            detail.NewValue = value.Autor;
            _connection.Add(detail);


            detail = new HistoryDetail();
            detail.HistoryRecord = record.ID;
            detail.Field = nameof(value.Description);
            detail.NewValue = value.Description;
            _connection.Add(detail);

            _connection.SaveChanges();
        }

        public void AddPage(Book value)
        {
            throw new NotImplementedException();
        }

        public void AddSection(Book value)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void UpdateBookByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            throw new NotImplementedException();
        }

        public void UpdatePage(Book value)
        {
            throw new NotImplementedException();
        }

        public void UpdatePageByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            throw new NotImplementedException();
        }

        public void UpdateSection(Book value)
        {
            throw new NotImplementedException();
        }

        public void UpdateSectionByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details)
        {
            throw new NotImplementedException();
        }
    }
}
