using System;
using System.Collections.Generic;
using System.Text;
using OneNote.Communication.Model;
using OneNote.Model;

namespace OneNote.Communication
{
    class Communication : ICommunication
    {
        /// <summary>
        /// Проверить логин пароль (на непустые)
        /// сделать запрос
        /// Если ок, добавить в локальную БД
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Authorize(string login, string password)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Отправить запрос
        /// Залить все в базу
        /// 
        /// или если в базе уже есть тогда запросить историю
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public BookModel GetBooks(string token)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить историю и записать в базу
        /// </summary>
        /// <param name="token"></param>
        /// <param name="table"></param>
        /// <param name="lastID"></param>
        /// <returns></returns>
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
