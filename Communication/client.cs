using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OneNote.Communication.Model;
using OneNote.Model;
using OneNote.SQLite;

namespace OneNote.Communication
{
    public class Client : ICommunication
    {
        Uri baseUrl;
        HttpClient httpClient;
        private readonly Connection connection;

        Client(string uri, Connection _connection)
        {
            baseUrl = new Uri(uri);
            httpClient = new HttpClient();
            connection = _connection;
        }

        public string Authorize(string login, string password)
        {
            foreach(User i in connection.Users)
            {
                if(i.UserName == login && i.Password == password)
                {
                    return JsonConvert.SerializeObject(i);
                }
            }

            return null;
        }

        private async Task<BookModel> asyncGetBooks(string Token)
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            return JsonConvert.DeserializeObject<BookModel>(Token);
        }
        public BookModel GetBooks(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("someone"), "token");

            httpClient.PostAsync(new Uri(baseUrl + "/GetBooks"), content);

            return asyncGetBooks(Token).Result;
        }

        private async Task<HistoryModel> asyncGetHistory(string Token)
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            return JsonConvert.DeserializeObject<HistoryModel>(Token);
        }
        public HistoryModel GetHistory(string Token, string Table, int LastID)
        {
            MultipartContent content = new MultipartContent();
            content.Add();
            httpClient.PostAsync(new Uri(baseUrl + "/GetHistory"), content);

            return asyncGetHistory(Token).Result;
        }

        private async Task<PageModel> asyncGetPages(string Token)
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            return JsonConvert.DeserializeObject<PageModel>(Token);
        }
        public PageModel GetPages(string Token)
        {
            MultipartContent content = new MultipartContent();
            content.Add();
            httpClient.PostAsync(new Uri(baseUrl + "/GetPages"), content);

            return asyncGetPages(Token).Result;
        }

        private async Task<SectionModel> asyncGetSelections(string Token)
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            return JsonConvert.DeserializeObject<SectionModel>(Token);
        }
        public SectionModel GetSections(string Token)
        {
            MultipartContent content = new MultipartContent();
            content.Add();
            httpClient.PostAsync(new Uri(baseUrl + "/GetSections"), content);

            return asyncGetSelections(Token).Result;
        }

        private async Task<User> asyncGetUserDetails(string Token)
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            return JsonConvert.DeserializeObject<User>(Token);
        }
        public User GetUserDetails(string Token)
        {
            MultipartContent content = new MultipartContent();
            content.Add();
            httpClient.PostAsync(new Uri(baseUrl + "/GetUserDetails"), content);

            return asyncGetUserDetails(Token).Result;
        }

        public string Register(User user)
        {
            foreach(User i in connection.Users)
            {
                if(i.UserName == user.UserName)
                {
                    return null;
                }
            }

            connection.Users.Add(user);
            connection.SaveChanges();
            return JsonConvert.SerializeObject(user);
        }

        public string SetHistory(string Token, HistoryModel history)
        {
            //connection.HistoryDetails.Add(history.Details);
        }
    }
}