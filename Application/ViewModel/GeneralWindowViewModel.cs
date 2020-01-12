using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneNote.Application.ViewModel
{
    public class GeneralWindowViewModel
    {
        //rename it later
        public List<string> leftListBox { get; set; }
        public List<string> rightListBox { get; set; }
        public string Content { get; set; }
        public ICommand leftAdd { get; set; }
        public ICommand rightAdd { get; set; }

        public GeneralWindowViewModel()
        {
            Content = "Here will be content";
        }
    }
}
