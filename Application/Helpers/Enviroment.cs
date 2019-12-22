using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNote.Application.Helpers
{
    class Enviroment : IEnviroment
    {
        public string UserToken { get; set; }
        public User CurrentUser { get; set; }
    }
}
