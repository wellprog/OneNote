using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OneNote.Model;
using OneNote.SQLite.Helper;
using OneNote.SQLite.Model;

namespace OneNote.SQLite
{
    public class Database : IDatabase
    {
        private readonly Connection _connection;
        public Database(Connection connection)
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
            _connection.AddRange(model.Records);
            _connection.AddRange(model.Details);
            var max = model.Records.Max(f => f.CreateTime);
            var newRecordID = model.Records.Single().RecordID;
            var tableName = model.Records.First().Table;
            var pointer = _connection.HistoryPointers
                .Where(f => f.TableName == tableName)
                .FirstOrDefault();

            if (pointer == null)
            {
                pointer = new HistoryPointer() {
                    TableName = tableName,
                    RecordId = newRecordID
                };
                _connection.HistoryPointers.Add(pointer);
            }
            else
            {
                pointer.RecordId = newRecordID;
            }

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
        public HistoryModel GetHistory(string TableName, string LastID)
        {
            HistoryModel historyModel = new HistoryModel();
            var targetCreateTime = _connection.HistoryRecords
                .Where(f => f.Table == TableName && f.RecordID == LastID).Select(f => f.CreateTime)
                .FirstOrDefault();
            historyModel.Records = _connection.HistoryRecords
                .Where(f => f.Table == TableName && f.CreateTime > targetCreateTime);
            List <HistoryDetail> details = new List<HistoryDetail>();
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
                Type typeElement = Type.GetType($"OneNote.Model.{ record.Table }, Model");

                Base element = (Base)_connection.Find(typeElement, record.RecordID);
                if (element == null)
                {
                    element = (Base)Activator.CreateInstance(typeElement);
                    element.ID = record.RecordID;
                    _connection.Add(element);
                }

                IEnumerable<HistoryDetail> _details = _connection.HistoryDetails.Where(f => f.HistoryRecord == record.ID);
                foreach (HistoryDetail item in _details)
                {
                    PropertyInfo property = typeElement.GetProperty(item.Field);
                    property.SetValue(element, item.NewValue);
                }
            }
            _connection.SaveChanges();
        }

        public User GetUserByLoginPassword(string login, string password)
        {
            User currentUser = _connection.Users.Where(f => f.UserName == login && f.Password == password).FirstOrDefault();
            if (currentUser == null)
                return null;

            return currentUser;
        }

        public bool AddUser(User user)
        {
            User currentUser = _connection.Users.Where(f => f.UserName == user.UserName).FirstOrDefault();
            if (currentUser != null)
                return false;
            _connection.Add(user);
            _connection.SaveChanges();
            return true;
        }

        public bool DeleteUser(User user)
        {

            User currentUser = _connection.Users.Where(f => f.UserName == user.UserName).FirstOrDefault();
            if (currentUser == null)
                return false;
            currentUser.IsDeleted = true;
            _connection.SaveChanges();
            return true;
        }

        public bool UpdateUser(User user)
        {
            User currentUser = _connection.Users.Where(f => f.UserName == user.UserName).FirstOrDefault();
            if (currentUser == null)           return false;
            if (!Validate.ValidateModel(user)) return false;
            PropertyInfo[] propertys = currentUser.GetType().GetProperties();

           
            foreach(PropertyInfo prop in propertys)
            {
                if (prop.Name.ToLower() == "id")
                    continue;
                prop.SetValue(currentUser, prop.GetValue(user));
            }

            _connection.SaveChanges();
            return true;
        }

        public bool IsUserExists(string login)
        {
            User currentUser = _connection.Users.Where(f => f.UserName == login).FirstOrDefault();
            return currentUser == null ? false : true;
        }

        public string GetLastBookHistory()
        {
            return GetLastHistory("Book");
        }

        public string GetLastSectionHistory()
        {
            return GetLastHistory("Section");
        }

        public string GetLastPageHistory()
        {
            return GetLastHistory("Page");
        }

        public string GetLastHistory(string tableName)
        {
            var History = _connection.HistoryRecords.Where(f => f.Table == tableName).ToList();
            if (History.Count == 0) return null;
            return History.OrderBy(f => f.CreateTime).Last().ID;
        }

        public string GetLastID(string tableName)
        {
            var result = _connection.HistoryPointers
                .Where(f => f.TableName == tableName && f.IsClient)
                .SingleOrDefault();

            if (result == null) throw new ArgumentException("Wrong table name!");
            return result.RecordId;
        }
    }
}
