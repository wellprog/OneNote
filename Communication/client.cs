using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OneNote.Communication.Model;
using OneNote.Model;
using OneNote.SQLite;
using OneNote.SQLite.Model;

namespace OneNote.Communication
{
    public class Client : ICommunication
    {
        Uri baseUrl;
        HttpClient httpClient;
        private IDatabase _db;
        private Connection _con;
        private string LastId { get; set; }  //указатель на последнюю запись скачанную с удаленного репо

        public string Token { get; set; }
        private string _salt = "somesalt";
        private TokenStorage _tokenStorage;

        public Client(string uri) : this(new Uri(uri))
        {
        }

        public Client(Uri uri)
        {
            baseUrl = uri;
            httpClient = new HttpClient();

            Connection c = new Connection();
           // if (c.Database.EnsureCreated())
            {
                _con = c;
                _db = new Database(c);
            }
          //  else
            {
              //  throw new Exception("A white little polar animal has come!");
            }
        }
        
        public BookModel GetBooks(string token, CancellationToken cToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("token"), token);

            var request = httpClient.PostAsync(new Uri(baseUrl + "/GetBooks"), content);
            request.Wait(cToken);


            HttpResponseMessage returnedData = request.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<BookModel>(returnedDataString);
        }
        
        public HistoryModel GetHistory(string token, string table, string lastID, CancellationToken cToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("token"), token);
            content.Add(new StringContent("table"), table);
            content.Add(new StringContent("lastID"), lastID);

            var request = httpClient.PostAsync(new Uri(baseUrl + "/GetHistory"), content);
            request.Wait(cToken);

            HttpResponseMessage returnedData = request.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<HistoryModel>(returnedDataString);
        }
        
        public PageModel GetPages(string token, CancellationToken cToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("token"), token);

            var request = httpClient.PostAsync(new Uri(baseUrl + "/GetPages"), content);
            request.Wait(cToken);

            HttpResponseMessage returnedData = request.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<PageModel>(returnedDataString);
        }
        
        public SectionModel GetSections(string token, CancellationToken cToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("token"), token);

            var request = httpClient.PostAsync(new Uri(baseUrl + "/GetSections"), content);
            request.Wait(cToken);

            HttpResponseMessage returnedData = request.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<SectionModel>(returnedDataString);
        }
        
        public User GetUserDetails(string token, CancellationToken cToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("token"), token);

            var request = httpClient.PostAsync(new Uri(baseUrl + "/GetUserDetails"), content);
            request.Wait(cToken);

            HttpResponseMessage returnedData = request.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<User>(returnedDataString);
        }
        
        public string Authorize(string login, string password, CancellationToken cToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent(login), "login");
            content.Add(new StringContent(password), "password");

            var request = httpClient.PostAsync(new Uri(baseUrl + "/Authorize"), content);
            request.Wait(cToken);

            HttpResponseMessage returnedData = request.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return returnedDataString;
        }
        
        public string Register(User user, CancellationToken cToken)
        {
            //MultipartFormDataContent content = new MultipartFormDataContent();
           
            //var json = JsonConvert.SerializeObject(user);
            
            //content.Add(new StringContent(json),
            //    "user");
            var encData = new Dictionary<string, string>();
            PropertyInfo[] pInfo = typeof(User).GetProperties();
            foreach(PropertyInfo item in pInfo)
            {
                encData.Add(item.Name, item.GetValue(user).ToString());
            }
            var formContent = new FormUrlEncodedContent(encData);
            var uri = new Uri(baseUrl + "/Register");
            var request = httpClient.PostAsync(uri, formContent);
            request.Wait();
            HttpResponseMessage returnedData = request.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return returnedDataString;

            //if (_db.IsUserExists(user.UserName)) return null;
            //_db.AddUser(user);
            //string token = user.UserName + _salt + user.Password;
            //_tokenStorage.Tokens.Add(token, user);
            //return token;
        }
        
        public string SetHistory(string token, HistoryModel history, CancellationToken cToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("token"), token);
            content.Add(new StringContent("history"), JsonConvert.SerializeObject(history));

            var request = httpClient.PostAsync(new Uri(baseUrl + "/SetHistory"), content);
            request.Wait(cToken);

            HttpResponseMessage returnedData = request.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return returnedDataString;
        }

        public string SetBookHistory(string token, HistoryModel history, CancellationToken cToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("token"), token);
            content.Add(new StringContent("history"), JsonConvert.SerializeObject(history));

            var request = httpClient.PostAsync(new Uri(baseUrl + "/SetBookHistory"), content);
            request.Wait(cToken);

            HttpResponseMessage returnedData = request.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return returnedDataString;
        }

        public string SetSectionHistory(string token, HistoryModel history, CancellationToken cToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("token"), token);
            content.Add(new StringContent("history"), JsonConvert.SerializeObject(history));

            var request = httpClient.PostAsync(new Uri(baseUrl + "/SetSectionHistory"), content);
            request.Wait(cToken);

            HttpResponseMessage returnedData = request.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return returnedDataString;
        }

        public string SetPageHistory(string token, HistoryModel history, CancellationToken cToken)
        {
            MultipartFormDataContent content = new MultipartFormDataContent();
            content.Add(new StringContent("token"), token);
            content.Add(new StringContent("history"), JsonConvert.SerializeObject(history));

            var result = httpClient.PostAsync(new Uri(baseUrl + "/SetPageHistory"), content);
            result.Wait(cToken);

            HttpResponseMessage returnedData = result.Result;
            string returnedDataString = returnedData.Content.ReadAsStringAsync().Result;

            return returnedDataString;
        }

        public void AddLocalBook(Book value)
        {
            _db.AddBook(value);
        }

        public void AddLocalSection(Section value)
        {
            _db.AddSection(value);
        }

        public void AddLocalPage(Page value)
        {
            _db.AddPage(value);
        }

        public IEnumerable<Book> GetLocalBooks(string autorID)
        {
            return _db.GetBooks(autorID);
        }

        public IEnumerable<Section> GetLocalSections(string book)
        {
            return _db.GetSections(book);
        }

        public IEnumerable<Page> GetLocalPages(string section)
        {
            return _db.GetPages(section);
        }
        
        public SQLite.Model.HistoryModel GetLocalBookHystory(string LastID)
        {
            return _db.GetBookHistory(LastID);
        }

        public string GetLastId(string tableName)
        {
            return _db.GetLastID(tableName);
        }
    }
}