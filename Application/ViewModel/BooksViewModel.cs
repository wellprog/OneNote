using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using OneNote.Model;
using OneNote.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNote.Application.ViewModel
{
    public class BooksViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void BookSelected(string bookId);
        public event BookSelected OnBookSelected;


        private ObservableCollection<Book> _books = new ObservableCollection<Book>();
        public ObservableCollection<Book> Books
        {
            get
            {
                return _books;
            }
            set
            {
                _books = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Books)));
            }
        }

        private int _bookIndex = -1;
        public int BookIndex
        {
            get
            {
                return _bookIndex;
            }
            set
            {
                _bookIndex = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(BookIndex)));
                if (_bookIndex != -1 && Books.Count > _bookIndex)
                {
                    OnBookSelected?.Invoke(Books[_bookIndex].ID);
                }
                
            }
        }


        public ClickCommand AddCommand { get; }

        private ICommunication _communication;
        private IEnviroment _enviroment;
        private IDatabase _database;

        private string token = "";
        private User currentUser;


        public BooksViewModel()
        {
            AddCommand = new ClickCommand(OnAddCommand);
            AddCommand.SetCanExecuted(true);

            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();
            _database = ClassLoader.Instance.GetElement<IDatabase>();

            token = _enviroment.UserToken;
            currentUser = _communication.GetUserDetails(token);

            _database.GetBooks(currentUser.ID).ToList().ForEach(f => Books.Add(f));
        }

        protected void OnAddCommand(object param)
        {

            AddBox box = new AddBox();
            box.ShowDialog();

            string result = box.GetResult;

            if(!string.IsNullOrWhiteSpace(result))
            {
                Book book = _database.GetBooks(currentUser.ID).Where(f => f.Name == result).FirstOrDefault();
                if (book == null)
                {
                    book = new Book() { Name = result, Autor = currentUser.ID };
                    _database.AddBook(book);
                    Books.Add(book);
                }
            }
        }

        //TODO
        // 1 - Список книг
        // 2 - Комманда добавления
        // 3 - Функция добавления с использованием ICommunication

    }
}
