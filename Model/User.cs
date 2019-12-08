using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    
    public class User : Base
    {
        [HistoryField]
        [Validate(Required = true)]
        public string   UserName { get; set; }
        [HistoryField]
        [Validate(Required = true)]
        public string   EMail { get; set; }
        [HistoryField]
        [Validate(Required = true)]
        public string   Password { get; set; }
        [HistoryField]
        [Validate(Required = true)]
        public string   Phone { get; set; }
        [HistoryField]
        [Validate(MinValue = 12)]
        public int      Age { get; set; }
        [HistoryField]
        public string   Avatar { get; set; }
        [HistoryField]
        public bool     Gender { get; set; }
        [HistoryField]
        public string   Status { get; set; }
    }
}
