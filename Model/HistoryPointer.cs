using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class HistoryPointer
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public bool IsClient { get; set; }
        public string RecordId { get; set; }
    }
}
