using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class Book : Base
    {
        [HistoryField]
        [Validate(Required = true)]
        public string   Name { get; set; }
        [HistoryField]
        [Validate(Required = true)]
        public string   Cover { get; set; }
        [HistoryField]
        [Validate(Required = true)]
        public string   Autor { get; set; }
        [HistoryField]
        public string   Description { get; set; }
    }
}
