using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.SQLite.Model
{
    public class HistoryModel
    {
        public IEnumerable<HistoryRecord> Records { get; set; }
        public IEnumerable<HistoryDetail> Details { get; set; }
    }
}
