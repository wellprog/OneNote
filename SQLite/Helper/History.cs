using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using OneNote.Model;
using OneNote.SQLite.Model;

namespace OneNote.SQLite.Helper
{
    public class History
    {
        public static HistoryModel GetHistoryFromModel<T>(T from, T to, string Autor) where T : Base
        {
            if (from == null)
            {
                from = (T)Activator.CreateInstance(to.GetType());
            }

            if (from.GetType() != to.GetType())
                throw new ArgumentException();

            string TableName = to.GetType().Name;
            string RecordID = to.ID;

            PropertyInfo[] properties = to.GetType().GetProperties();

            HistoryRecord record = new HistoryRecord();
            record.Table = TableName;
            record.RecordID = RecordID;
            record.Autor = Autor;

            List<HistoryDetail> details = new List<HistoryDetail>();

            foreach (PropertyInfo info in properties)
            {
                HistoryFieldAttribute attr = info.GetCustomAttribute<HistoryFieldAttribute>();
                if (attr == null)
                    continue;

                var fromValue = info.GetValue(from);
                var toValue = info.GetValue(to);

                if (fromValue != toValue)
                {
                    HistoryDetail detail = new HistoryDetail();
                    detail.Field = info.Name;
                    detail.PrevValue = fromValue == null ? "" : fromValue.ToString();
                    detail.NewValue = toValue == null ? "" : toValue.ToString();
                    detail.HistoryRecord = record.ID;

                    details.Add(detail);
                }
            }

            HistoryModel returnData = new HistoryModel();
            returnData.Details = details;
            returnData.Records = new List<HistoryRecord> { record };

            return returnData;
        }
    }
}
