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
            BooksPanel.OnBookSelected += SectionsPanel.FromBook; //Оповещаем секции о смене книги, чтобы они обновились

            PagesPanel = new PagesViewModel();
            PagesPanel.OnPageSelected += PagesPanel.PageChanged; //Оповещаем страницы о смене страницы (?), чтобы они сохранились и вывели новую

            SectionsPanel.OnSectionSelected += PagesPanel.SectionChanged; //Оповещаем страницы о смене секции, чтобы они сохранились
            SectionsPanel.OnSectionSelected += PagesPanel.FromSection; //Оповещаем страницы о смене секции, чтобы они обновились
            BooksPanel.OnBookSelected += PagesPanel.BookChanged; //Оповещаем страницы о смене книги, чтобы они сохранились
        }
    }
}
