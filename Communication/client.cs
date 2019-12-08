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

        public Client(string uri)
        {
            baseUrl = new Uri(uri);
            httpClient = new HttpClient();
        }
        public Client(Uri uri)
        {
            baseUrl = uri;
            httpClient = new HttpClient();
        }
        
        public BookModel GetBooks(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);
            
            HttpResponseMessage returnedData = httpClient.PostAsync(new Uri(baseUrl + "/GetBooks"), content).Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<BookModel>(returnedDataString);
        }
        
        public HistoryModel GetHistory(string Token, string Table, int LastID)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);
            content.Add(new StringContent("Table"), Table);
            content.Add(new StringContent("LastID"), LastID.ToString());

            HttpResponseMessage returnedData = httpClient.PostAsync(new Uri(baseUrl + "/GetHistory"), content).Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<HistoryModel>(returnedDataString);
        }
        
        public PageModel GetPages(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            HttpResponseMessage returnedData = httpClient.PostAsync(new Uri(baseUrl + "/GetPages"), content).Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<PageModel>(returnedDataString);
        }
        
        public SectionModel GetSections(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            HttpResponseMessage returnedData = httpClient.PostAsync(new Uri(baseUrl + "/GetSections"), content).Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<SectionModel>(returnedDataString);
        }
        
        public User GetUserDetails(string Token)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);

            HttpResponseMessage returnedData = httpClient.PostAsync(new Uri(baseUrl + "/GetUserDetails"), content).Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<User>(returnedDataString);
        }
        
        public string Authorize(string login, string password)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("login"), login);
            content.Add(new StringContent("password"), password);

            HttpResponseMessage returnedData = httpClient.PostAsync(new Uri(baseUrl + "/Authorize"), content).Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return returnedDataString;
        }
        
        public string Register(User user)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("user"), JsonConvert.SerializeObject(user));

            HttpResponseMessage returnedData = httpClient.PostAsync(new Uri(baseUrl + "/Register"), content).Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return returnedDataString;
        }
        
        public string SetHistory(string Token, HistoryModel history)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("Token"), Token);
            content.Add(new StringContent("history"), JsonConvert.SerializeObject(history));

            HttpResponseMessage returnedData = httpClient.PostAsync(new Uri(baseUrl + "/SetHistory"), content).Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return returnedDataString;
        }
    }
}