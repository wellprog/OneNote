using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class Page : Base
    {
        public string   Name { get; set; }
        public string   Description { get; set; }
        public string   Text { get; set; }
        public string   Section { get; set; }
    }
}
