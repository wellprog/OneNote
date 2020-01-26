using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using OneNote.Model;
using OneNote.Communication.Model;
using OneNote.Communication.Helpers;
using OneNote.Communication.Model.ResponseModel;
using OneNote.SQLite.Model;

namespace OneNote.Communication
{
    public class Communication : ICommunication
    {
        private Client _client;
        private Enviroment _env;

        public Communication(Client client, Enviroment env)
        {
            _client = client;
            _env = env;
        }

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
            if (String.IsNullOrWhiteSpace(login) && String.IsNullOrWhiteSpace(password))
            {
                return "Please fill both fields!";
            }

            var response = _client.Authorize(login, password);
            ResponseToken responseToken = JsonConvert.DeserializeObject<ResponseToken>(response);
            if (responseToken.ErrorID != 0)
                return responseToken.ErrorDescription;


            _client.Token = responseToken.Token;
            //BookModel bookModel = _client.GetBooks(_client.Token);

            //записать сначала локальные на сервер, потом с сервера вытащить всё(в том числе только что записанные локальные)
            //отправляем HistoryModel.
            //var lastID = _client.GetLastId("Books");
            var lastID = _env.LastRecordId;
            var localHistoryModel = _client.GetLocalBookHystory(lastID);


            return responseToken.Token;
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
        public HistoryModel GetHistory(string token, string table, string lastID)
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
