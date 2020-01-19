using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OneNote.Server.Model
{
    public class ResponceModel
    {
        public int    ErrorID { get; set; }
        public string ErrorDescription { get; set; }
        public string AutorID { get; set; }
        public object Data { get; set; }
    }
}
