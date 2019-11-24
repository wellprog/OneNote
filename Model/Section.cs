using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class Section : Base
    {
        [HistoryField]
        public string   Name { get; set; }
        [HistoryField]
        public string   Description { get; set; }
        [HistoryField]
        public string   Book { get; set; }
    }
}
