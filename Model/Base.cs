using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    class Base
    {
        public int ID { get; set; }
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public DateTime DeleteTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } 
    }
}
