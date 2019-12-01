using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Communication.Model
{
    public class PageModel
    {
        public List<Page> Pages { get; set; }
        public string LastHistory { get; set; }
    }
}
