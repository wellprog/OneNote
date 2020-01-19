using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Communication.Model.ResponseModel
{
    class ResponseToken
    {
        public int ErrorID { get; set; }
        public string ErrorDescription { get; set; }
        public string AutorID { get; set; }
        public string Token { get; set; }
    }
}
