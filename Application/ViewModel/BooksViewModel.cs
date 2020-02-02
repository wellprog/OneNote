using OneNote.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNote.Application.ViewModel
{
    class BooksViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Book> _books = new List<Book>();
        public List<Book> Books
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

        public ClickCommand AddCommand { get; }

        public BooksViewModel()
        {
            AddCommand = new ClickCommand(OnAddCommand);
        }

        protected void OnAddCommand(object param)
        {

        }

        //TODO
        // 1 - Список книг
        // 2 - Комманда добавления
        // 3 - Функция добавления с использованием ICommunication

    }
}
