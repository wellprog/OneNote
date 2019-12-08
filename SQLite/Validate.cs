using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OneNote.SQLite
{
    class Validate
    {
        static public bool ValidateModel<T>(T model) where T : Base
        {
            Type type = model.GetType();

            foreach (var prop in type.GetProperties())
            {
                foreach (ValidateAttribute attr in prop.GetCustomAttributes(typeof(ValidateAttribute), false))
                {
                    if (attr.MinValue != -1)
                    {
                        int v = 0;
                        if (int.TryParse(prop.GetValue(model).ToString(), out v))
                        {
                            if (v < attr.MinValue) return false;
                        }
                    }
                    if(attr.Required != false)
                    {
                        if (String.IsNullOrWhiteSpace(prop.GetValue(model).ToString())) return false;
                    }
                    if(attr.Regex != "")
                    {
                        Regex regex = new Regex(attr.Regex);
                        MatchCollection matches = regex.Matches(prop.GetValue(model).ToString());
                        if (matches.Count == 0)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
            
        }
    }
}
