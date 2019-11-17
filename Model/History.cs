using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    class History : Base
    {
        public string Table { get; set; }
        public string Field { get; set; }
        public string PrevValue { get; set; }
        public string NewValue { get; set; }
        public int Autor { get; set; }
    }
}
