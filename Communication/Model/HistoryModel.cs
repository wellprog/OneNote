using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Communication.Model
{
    public class HistoryModel
    {
        public List<HistoryRecord> Records { get; set; }
        public List<HistoryDetail> Details { get; set; }
    }
}
