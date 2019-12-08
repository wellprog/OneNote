using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
   public class ValidateAttribute: Attribute
    {
        public bool Required { get; set; } = false;
        public int MinValue { get; set; } = -1;
        public string Regex { get; set; } = "";
    }
}
