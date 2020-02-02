using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using OneNote.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OneNote.Application.Services
{
    class SyncService
    {
        private ICommunication _communication;
        private IEnviroment _enviroment;
        private IDatabase _database;

        public SyncService ()
        {
            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();
            _database = ClassLoader.Instance.GetElement<IDatabase>();
        }

        public void Start() {
            Thread th = new Thread(threadStart);
            th.Start();
        }
        public void Stop() {
            //TODO stop timer
        }


        void threadStart()
        {
            SyncBooks();
            SyncSections();
            SyncPages();

            Thread.Sleep(60000);
        }

        private void SyncBooks()
        {
            var data = _database.GetBooks(_enviroment.CurrentUser.ID);
            //Если у нас ничего нет то нам нужно доставть все книги
            if (data.Count() == 0)
            {
                var element = _communication.GetBooks(_enviroment.UserToken);

                foreach (var book in element.Books)
                    _database.AddBook(book);

                _enviroment.RemoteBookCursor = element.LastHistory;
            }
            else
            {
                //Отправить
                var toSend = _database.GetBookHistory(_enviroment.LocalBookCursor);
                _communication.SetBookHistory(_enviroment.UserToken, toSend);

                //Получаем
                var element  = _communication.GetHistory(_enviroment.UserToken, "Book", _enviroment.RemoteBookCursor);

                if (element.Records.Count() > 0)
                {
                    _database.UpdateBookByHistory(element.Records, element.Details);
                    _enviroment.RemoteBookCursor = element.Records.Last().ID;
                }
            }
        }

        private void SyncSections()
        {
            var data = _database.GetSections(_enviroment.CurrentUser.ID);
            //Если у нас ничего нет то нам нужно доставть все книги
            if (data.Count() == 0)
            {
                var element = _communication.GetSections(_enviroment.UserToken);

                foreach (var section in element.Sections)
                    _database.AddSection(section);

                _enviroment.RemoteSectionCursor = element.LastHistory;
            }
            else
            {
                //Отправить
                var toSend = _database.GetSectionHistory(_enviroment.LocalSectionCursor);
                _communication.SetSectionHistory(_enviroment.UserToken, toSend);

                //Получаем
                var element = _communication.GetHistory(_enviroment.UserToken, "Section", _enviroment.RemoteSectionCursor);

                if (element.Records.Count() > 0)
                {
                    _database.UpdateSectionByHistory(element.Records, element.Details);
                    _enviroment.RemoteSectionCursor = element.Records.Last().ID;
                }
            }
        }

        private void SyncPages()
        {
            var data = _database.GetPages(_enviroment.CurrentUser.ID);
            //Если у нас ничего нет то нам нужно доставть все книги
            if (data.Count() == 0)
            {
                var element = _communication.GetPages(_enviroment.UserToken);

                foreach (var page in element.Pages)
                    _database.AddPage(page);

                _enviroment.RemotePageCursor = element.LastHistory;
            }
            else
            {
                //Отправить
                var toSend = _database.GetPageHistory(_enviroment.LocalPageCursor);
                _communication.SetPageHistory(_enviroment.UserToken, toSend);

                //Получаем
                var element = _communication.GetHistory(_enviroment.UserToken, "Page", _enviroment.RemotePageCursor);

                if (element.Records.Count() > 0)
                {
                    _database.UpdatePageByHistory(element.Records, element.Details);
                    _enviroment.RemotePageCursor = element.Records.Last().ID;
                }
            }
        }
    }
}
