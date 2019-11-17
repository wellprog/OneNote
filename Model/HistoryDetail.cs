using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class HistoryDetail : Base
    {
        public string   Field { get; set; }
        public string   PrevValue { get; set; }
        public string   NewValue { get; set; }
        public string   HistoryRecord { get; set; }
    }
}
