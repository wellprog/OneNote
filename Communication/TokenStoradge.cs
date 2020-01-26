using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Communication
{
    public class TokenStorage
    {
        public Dictionary<string, User> Tokens = new Dictionary<string, User>();
    }
}
