using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using OneNote.SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace OneNote.Application.ViewModel
{
    public class GeneralWindowViewModel
    {
        public BooksViewModel BooksPanel { get; }
        public SectionViewModel SectionsPanel { get; }
        public PagesViewModel PagesPanel { get; }

        public ObservableCollection<Page> Pages { get; } = new ObservableCollection<Page>();

        public GeneralWindowViewModel()
        {
            BooksPanel = new BooksViewModel();

            SectionsPanel = new SectionViewModel();
            BooksPanel.OnBookSelected += SectionsPanel.FromBook;

            PagesPanel = new PagesViewModel();
            PagesPanel.OnPageSelected += PagesPanel.PageChanged;

            SectionsPanel.OnSectionSelected += PagesPanel.FromSection;
            BooksPanel.OnBookSelected += PagesPanel.SectionChanged;
        }
    }
}
