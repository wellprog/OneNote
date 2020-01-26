using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNote.Communication.Helpers
{
    public class Enviroment : IEnviroment
    {
        public string UserToken { get; set; }
        public User CurrentUser { get; set; }
        public string LastRecordId { get; set; }

        string RemoteBookCursor { get; set; }
        string RemotePageCursor { get; set; }
        string RemoteSectionCursor { get; set; }

        string LocalBookCursor { get; set; }
        string LocalPageCursor { get; set; }
        string LocalSectionCursor { get; set; }
    }
}
