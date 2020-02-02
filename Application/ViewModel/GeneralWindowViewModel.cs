using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using OneNote.SQLite;
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

        public ClickCommand bookAdd { get; set; }
        public ClickCommand sectionAdd { get; set; }

        ICommunication _communication; //Client
        IEnviroment _enviroment; //User

        protected void OnAddBookEventExecuted(object param)
        {
            AddBox addBookWindow = new AddBox();
            addBookWindow.ShowDialog();
        }

        protected void OnAddSectionEventExecuted(object param)
        {
            AddBox addSectionWindow = new AddBox();
            addSectionWindow.ShowDialog();
        }

        public GeneralWindowViewModel()
        {
            Content = "Here will be content";
            bookAdd = new ClickCommand(OnAddBookEventExecuted);
            sectionAdd = new ClickCommand(OnAddSectionEventExecuted);
            bookAdd.SetCanExecuted(true);
            sectionAdd.SetCanExecuted(true);

            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();
        }
    }
}
