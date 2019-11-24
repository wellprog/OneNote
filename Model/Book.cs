using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class Book : Base
    {
        [HistoryField]
        public string   Name { get; set; }
        [HistoryField]
        public string   Cover { get; set; }
        [HistoryField]
        public string   Autor { get; set; }
        [HistoryField]
        public string   Description { get; set; }
    }
}
