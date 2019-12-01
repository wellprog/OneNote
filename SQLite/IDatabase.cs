using OneNote.Model;
using OneNote.SQLite.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.SQLite
{
    interface IDatabase
    {
        /// <summary>
        /// Функции просто возвращают значения
        /// </summary>
        /// <param name="Autor"></param>
        IEnumerable<Book> GetBooks(string Autor);
        IEnumerable<Section> GetSections(string Book);
        IEnumerable<Page> GetPages(string Section);

        /// <summary>
        /// Функции обновляют записи, а так же добавляют информацию в историю
        /// </summary>
        /// <param name="value"></param>
        void UpdateBook(Book value);
        void UpdateSection(Section value);
        void UpdatePage(Page value);

        /// <summary>
        /// Функции добавляют записи, а так же добавляют информацию в историю
        /// </summary>
        /// <param name="value"></param>
        void AddBook(Book value);
        void AddSection(Section value);
        void AddPage(Page value);

        /// <summary>
        /// Функции обновляют записи по истории
        /// либо создают записи если их нет
        /// </summary>
        /// <param name="records"></param>
        /// <param name="details"></param>
        void UpdateBookByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details);
        void UpdateSectionByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details);
        void UpdatePageByHistory(IEnumerable<HistoryRecord> records, IEnumerable<HistoryDetail> details);

        /// <summary>
        /// Функции просто возвращают записи по последниму ID
        /// </summary>
        /// <param name="LastID"></param>
        /// <returns></returns>
        HistoryModel GetBookHistory(string LastID);
        HistoryModel GetSectionHistory(string LastID);
        HistoryModel GetPageHistory(string LastID);
    }
}
