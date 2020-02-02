using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using OneNote.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNote.Application.ViewModel
{
   public class SectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private ICommunication _communication;
        private IEnviroment _enviroment;
        private Client _client;

        private string token = "";
        private User currentUser;

        private List<Section> _sections = new List<Section>();
        public List<Section> Sections
        {
            get
            {
                return _sections;
            }
            set
            {
                _sections = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Section)));
            }
        }
        public SectionViewModel()
        {
            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();
            _client = ClassLoader.Instance.GetElement<Client>();

            token = _enviroment.UserToken;
            currentUser = _communication.GetUserDetails(token);
        }

        public IEnumerable<Section> FormBook(string book)
        {
           return _client.GetLocalSections(book);
        }

    }
}
