using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using OneNote.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneNote.Application.ViewModel
{
    public class GeneralWindowViewModel
    {
        public BooksViewModel BooksPanel { get; set; } = new BooksViewModel();
    }
}
