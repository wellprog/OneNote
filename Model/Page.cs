using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    class Page : Base
    {
        public string   Name { get; set; }
        public string   Description { get; set; }
        public string   Text { get; set; }
        public int      Section { get; set; }
    }
}
