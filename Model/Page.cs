using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class Page : Base
    {
        [HistoryField]
        public string   Name { get; set; }
        [HistoryField]
        public string   Description { get; set; }
        [HistoryField]
        public string   Text { get; set; }
        [HistoryField]
        public string   Section { get; set; }
    }
}
