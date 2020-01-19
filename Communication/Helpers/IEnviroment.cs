﻿using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNote.Communication.Helpers
{
    public interface IEnviroment
    {
        string UserToken { get; set; }
        User   CurrentUser { get; set; }
        string LastRecordId { get; set; }
    }
}