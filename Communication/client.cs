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
        
        private async Task<GettedType> asyncGet<GettedType>()
        {
            HttpResponseMessage response = await httpClient.GetAsync(baseUrl);
            string getted = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GettedType>(getted);
            //return JsonConvert.DeserializeObject<BookModel>(Token);
        }
        public BookModel GetBooks(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            httpClient.PostAsync(new Uri(baseUrl + "/GetBooks"), content);

            return asyncGet<BookModel>().Result;
        }
        
        public HistoryModel GetHistory(string Token, string Table, int LastID)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);
            content.Add(new StringContent("Table"), Table);
            content.Add(new StringContent("LastID"), LastID.ToString());

            httpClient.PostAsync(new Uri(baseUrl + "/GetHistory"), content);

            return asyncGet<HistoryModel>().Result;
        }
        
        public PageModel GetPages(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            httpClient.PostAsync(new Uri(baseUrl + "/GetPages"), content);

            return asyncGet<PageModel>().Result;
        }
        
        public SectionModel GetSections(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            httpClient.PostAsync(new Uri(baseUrl + "/GetSections"), content);

            return asyncGet<SectionModel>().Result;
        }
        
        public User GetUserDetails(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            httpClient.PostAsync(new Uri(baseUrl + "/GetUserDetails"), content);

            return asyncGet<User>().Result;
        }
        
        public string Authorize(string login, string password)
        {
            /*
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("login"), login);
            content.Add(new StringContent("password"), password);

            httpClient.PostAsync(new Uri(baseUrl + "/Authorize"), content);
            
            return asyncGet<string>().Result;
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
        
        public string Register(User user)
        {
            /*MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("user"), JsonConvert.SerializeObject(user));
            httpClient.PostAsync(new Uri(baseUrl + "/Register"), content);
            
            return asyncGet<User>().Result;*/

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
        
        public string SetHistory(string Token, HistoryModel history)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);
            content.Add(new StringContent("history"), JsonConvert.SerializeObject(history));
            httpClient.PostAsync(new Uri(baseUrl + "/SetHistory"), content);

            return asyncGet<string>().Result;
        }
    }
}