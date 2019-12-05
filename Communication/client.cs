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

        private async Task<BookModel> asyncGetBooks()
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            string getted = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BookModel>(getted);
            //либо return JsonConvert.DeserializeObject<BookModel>(Token);
        }
        public BookModel GetBooks(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            httpClient.PostAsync(new Uri(baseUrl + "/GetBooks"), content);

            return asyncGetBooks().Result;
        }

        private async Task<HistoryModel> asyncGetHistory()
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            string getted = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<HistoryModel>(getted);
            //либо return JsonConvert.DeserializeObject<BookModel>(Token);
        }
        public HistoryModel GetHistory(string Token, string Table, int LastID)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);
            content.Add(new StringContent("Table"), Table);
            content.Add(new StringContent("LastID"), LastID.ToString());

            httpClient.PostAsync(new Uri(baseUrl + "/GetHistory"), content);

            return asyncGetHistory().Result;
        }

        private async Task<PageModel> asyncGetPages()
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            string getted = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<PageModel>(getted);
            //либо return JsonConvert.DeserializeObject<BookModel>(Token);
        }
        public PageModel GetPages(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            httpClient.PostAsync(new Uri(baseUrl + "/GetPages"), content);

            return asyncGetPages().Result;
        }

        private async Task<SectionModel> asyncGetSections()
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            string getted = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<SectionModel>(getted);
            //либо return JsonConvert.DeserializeObject<BookModel>(Token);
        }
        public SectionModel GetSections(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            httpClient.PostAsync(new Uri(baseUrl + "/GetSections"), content);

            return asyncGetSections().Result;
        }
        
        private async Task<User> asyncGetUserDetails()
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            string getted = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<User>(getted);
            //либо return JsonConvert.DeserializeObject<BookModel>(Token);
        }
        public User GetUserDetails(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            httpClient.PostAsync(new Uri(baseUrl + "/GetUserDetails"), content);

            return asyncGetUserDetails().Result;
        }

        /*private async Task<string> asyncAuthorize(string login, string password)
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            return JsonConvert.DeserializeObject<string>(login);
        }
         private async Task<string> asyncAuthorize()
         {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            string getted = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string>(getted);
         }*/
        public string Authorize(string login, string password)
        {
            /*
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("login"), login);
            content.Add(new StringContent("password"), password);

            httpClient.PostAsync(new Uri(baseUrl + "/Authorize"), content);

            return asyncAuthorize().Result;
            */
            foreach (User i in connection.Users)
            {
                if (i.UserName == login && i.Password == password)
                {
                    return JsonConvert.SerializeObject(i);
                }
            }

            return null;
        }

        /*private async Task<string> asyncRegister()
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            string getted = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string>(getted);
        }*/
        public string Register(User user)
        {
            /*MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("user"), JsonConvert.SerializeObject(user));
            httpClient.PostAsync(new Uri(baseUrl + "/Register"), content);

            return asyncRegister().Result;*/

            foreach (User i in connection.Users)
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

        private async Task<string> asyncGetSettedHistoryID()
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            string getted = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<string>(getted);
        }
        public string SetHistory(string Token, HistoryModel history)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);
            content.Add(new StringContent("history"), JsonConvert.SerializeObject(history));
            httpClient.PostAsync(new Uri(baseUrl + "/SetHistory"), content);

            return asyncGetSettedHistoryID().Result;
        }
    }
}