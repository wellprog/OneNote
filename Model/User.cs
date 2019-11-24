using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class User : Base
    {
        public string   UserName { get; set; }
        public string   EMail { get; set; }
        public string   Password { get; set; }
        public string   Phone { get; set; }
        public int      Age { get; set; }
        public string   Avatar { get; set; }
        public bool     Gender { get; set; }
        public string   Status { get; set; }
    }
}
