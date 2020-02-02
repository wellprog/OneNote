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

        public string RemoteBookCursor { get; set; }
        public string RemotePageCursor { get; set; }
        public string RemoteSectionCursor { get; set; }

        public string LocalBookCursor { get; set; }
        public string LocalPageCursor { get; set; }
        public string LocalSectionCursor { get; set; }
    }
}
