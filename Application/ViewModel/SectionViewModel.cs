using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using OneNote.Model;
using OneNote.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private IDatabase _database;
        private string _currentBookId;

        private string token = "";
        private User currentUser;

        public delegate void SectionSelected(string sectionId);
        public event SectionSelected OnSectionSelected;

        private ObservableCollection<Section> _sections = new ObservableCollection<Section>();
        public ObservableCollection<Section> Sections
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

        private int _sectionIndex = -1;
        public int SectionIndex
        {
            get
            {
                return _sectionIndex;
            }
            set
            {
                _sectionIndex = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(SectionIndex)));
                if (_sectionIndex != -1 && Sections.Count > _sectionIndex)
                {
                    OnSectionSelected?.Invoke(Sections[_sectionIndex].ID);
                }
            }
        }

        public ClickCommand AddCommand { get; }

        public SectionViewModel()
        {
            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();
            _database = ClassLoader.Instance.GetElement<IDatabase>();

            AddCommand = new ClickCommand(OnAddCommand);

            token = _enviroment.UserToken;
            currentUser = _communication.GetUserDetails(token);
        }

        private void OnAddCommand(object param)
        {
            AddBox box = new AddBox();
            box.ShowDialog();

            string result = box.GetResult;

            if (!string.IsNullOrWhiteSpace(result))
            {
                Section section = _database.GetSections(_currentBookId).Where(f => f.Name == result).FirstOrDefault();
                if (section == null)
                {
                    section = new Section()
                    {
                        Name = result,
                        Description = "",
                        Book = _currentBookId
                    };
                    _database.AddSection(section);
                    Sections.Add(section);
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Sections)));
                }
            }
        }

        public void FromBook(string book)
        {
            _currentBookId = book;
            AddCommand.SetCanExecuted(true);

            Sections.Clear();
            _database.GetSections(book)
                ?.ToList().ForEach(f => Sections.Add(f));
        }

    }
}
