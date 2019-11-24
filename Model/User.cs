using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class User : Base
    {
        [HistoryField]
        public string   UserName { get; set; }
        [HistoryField]
        public string   EMail { get; set; }
        [HistoryField]
        public string   Password { get; set; }
        [HistoryField]
        public string   Phone { get; set; }
        [HistoryField]
        public int      Age { get; set; }
        [HistoryField]
        public string   Avatar { get; set; }
        [HistoryField]
        public bool     Gender { get; set; }
        [HistoryField]
        public string   Status { get; set; }
    }
}
