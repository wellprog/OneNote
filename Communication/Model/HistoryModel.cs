using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Communication.Model
{
    public class HistoryModel
    {
        List<HistoryRecord> Records { get; set; }
        List<HistoryDetail> Details { get; set; }
        public string LastHistory { get; set; }
    }
}
