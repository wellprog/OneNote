using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OneNote.Model;

namespace OneNote.Server
{
    public class TokenStorage
    {
        public Dictionary<string, User> Tokens = new Dictionary<string, User>();
    }
}
