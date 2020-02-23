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

        private int prevPageIndex;
        private int _pageIndex = -1;
        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }
            set
            {
                prevPageIndex = _pageIndex; //Для сохранения необходимо сохранять индекс страницы, с которой мы ушли
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
        
        public void FromSection(string Section) //Загрузка страниц секции
        {
            _currentSectionId = Section;
            AddCommand.SetCanExecuted(true);

            _database.GetPages(_currentSectionId)?.ToList().ForEach(f => Pages.Add(f));
        }

        public void BookChanged(string Book) //Если книга сменилась, сохраняем страницу и ничего не выводим
        {
            if (PageIndex != -1 && Pages.Count > 0)
            {
                Pages[PageIndex].Text = PresentPageText;
                _database.UpdatePage(Pages[PageIndex]);
            }
            PresentPageText = "";

            Pages.Clear();
        }
        public void SectionChanged(string Section) //Если секция сменилась, сохраняем страницу и ничего не выводим
        {
            if (PageIndex != -1 && Pages.Count > 0)
            {
                Pages[PageIndex].Text = PresentPageText;
                _database.UpdatePage(Pages[PageIndex]);
            }
            PresentPageText = "";

            Pages.Clear();
        }
        public void PageChanged(string Section) //Если страница сменилась, сохраняем прошлую страницу и выводим ту, которую выбрал пользователь
        {
            if (prevPageIndex != -1 && Pages.Count > 0)
            {
                Pages[prevPageIndex].Text = PresentPageText;
                _database.UpdatePage(Pages[prevPageIndex]);
            }
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
