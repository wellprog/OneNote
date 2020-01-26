using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using OneNote.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            //TODO start tmer
        }
        public void Stop() {
            //TODO stop timer
        }

        private void SyncBooks()
        {
            var data = _database.GetBooks(_enviroment.CurrentUser.ID);
            if (data.Count() == 0)
            {
                var element = _communication.GetBooks(_enviroment.UserToken);

                foreach (var book in element.Books)
                    _database.AddBook(book);

                _enviroment.RemoteBookCursor = element.LastHistory;
            } else
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
    }
}
