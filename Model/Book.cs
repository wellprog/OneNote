using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class Book : Base
    {
        public string   Name { get; set; }
        public string   Cover { get; set; }
        public string   Autor { get; set; }
        public string   Description { get; set; }
    }
}
