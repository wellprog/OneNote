﻿using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    class Section : Base
    {
        public string   Name { get; set; }
        public string   Description { get; set; }
        public int      Book { get; set; }
    }
}
