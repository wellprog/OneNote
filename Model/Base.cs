using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class Base
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public DateTime UpdateTime { get; set; } = DateTime.Now;
        public DateTime DeleteTime { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
    }
}
