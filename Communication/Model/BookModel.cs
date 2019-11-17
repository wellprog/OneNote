using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Communication.Model
{
    class BookModel
    {
        public List<Book> Books { get; set; }
        public string LastHistory { get; set; }
    }
}
