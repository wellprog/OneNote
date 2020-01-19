using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Model
{
    public class HistoryRecord : Base
    {
        public string   Table { get; set; }     // Имя Таблицы
        public string   RecordID { get; set; }  // ID изменяемого элемента 
        public string   Autor { get; set; }     // ID автора
    }
}
