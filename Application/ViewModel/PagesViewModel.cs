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
    public class PagesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void PageSelected(string pageId);
        public event PageSelected OnPageSelected;

        private string _presentPageText = "";
        public string PresentPageText {
            get
            {
                return _presentPageText;
            }
            set
            {
                _presentPageText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PresentPageText)));
            }
        }

        private ObservableCollection<Page> _pages = new ObservableCollection<Page>();
        public ObservableCollection<Page> Pages
        {
            get
            {
                //Здесь можно отслеживать смену секции
                return _pages;
            }
            set
            {
                _pages = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Pages)));
            }
        }

        private int _pageIndex = -1;
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                if (_pageIndex != -1)
                {
                    try //TODO (Костыль) (Ещё и кривой немного)
                    {
                        Pages[_pageIndex].Text = PresentPageText;
                        _database.UpdatePage(Pages[_pageIndex]);
                    }
                    catch (Exception exp) { } //do nothing
                }
                _pageIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PageIndex)));
                if (_pageIndex != -1 && Pages.Count > _pageIndex)
                {
                    OnPageSelected?.Invoke(Pages[_pageIndex].ID);
                }

            }
        }

        public ClickCommand AddCommand { get; }

        private ICommunication _communication;
        private IEnviroment _enviroment;
        private IDatabase _database;

        private string token = "";
        private User currentUser;
        private string _currentSectionId = "";

        public PagesViewModel ()
        {
            AddCommand = new ClickCommand(OnAddCommand);

            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();
            _database = ClassLoader.Instance.GetElement<IDatabase>();

            token = _enviroment.UserToken;
            currentUser = _communication.GetUserDetails(token);
        }

        public void FromSection(string Section)
        {
            _currentSectionId = Section;
            AddCommand.SetCanExecuted(true);

            Pages.Clear();

            _database.GetPages(_currentSectionId)?.ToList().ForEach(f => Pages.Add(f));
        }
        public void SectionChanged(string Section)
        {
            AddCommand.SetCanExecuted(false);

            Pages.Clear();
        }
        public void PageChanged(string Section)
        {
            PresentPageText = Pages[PageIndex].Text;
        }


        private void OnAddCommand(object param)
        {
            AddBox box = new AddBox();
            box.ShowDialog();

            string result = box.GetResult;

            if (!string.IsNullOrWhiteSpace(result))
            {
                Page page = _database.GetPages(_currentSectionId).Where(f => f.Name == result).FirstOrDefault();
                if (page == null)
                {
                    page = new Page()
                    {
                        Name = result,
                        Description = "",
                        Text = "",
                        Section = _currentSectionId
                    };
                    _database.AddPage(page);
                    Pages.Add(page);

                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Pages)));
                }
            }
        }
    }
}
