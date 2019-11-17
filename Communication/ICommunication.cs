using OneNote.Communication.Model;
using OneNote.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OneNote.Communication
{
    interface ICommunication
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Если все ок, то возвращается токен авторизации, если нет то NULL</returns>
        string Authorize(string login, string password);
        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="user">Пользователя целиком</param>
        /// <returns>Если все ок, то возвращается токен авторизации, если нет то NULL</returns>
        string Register(User user);

        /// <summary>
        /// Получение деталей пользователя
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        User GetUserDetails(string Token);

        /// <summary>
        /// Получение всех секций
        /// </summary>
        /// <param name="Token">Токен пользователя</param>
        /// <returns>Модель секций</returns>
        SectionModel GetSections(string Token);
        BookModel GetBooks(string Token);
        PageModel GetPages(string Token);
        
        /// <summary>
        /// Получение истории изменений по таблице и последней версии
        /// </summary>
        /// <param name="Token">Токен пользователя</param>
        /// <param name="Table">Наименование таблицы</param>
        /// <param name="LastID">Последний ID истории</param>
        /// <returns>Массив истории</returns>
        HistoryModel GetHistory(string Token, string Table, int LastID);
        /// <summary>
        /// Отправка истории
        /// </summary>
        /// <param name="Token">Токен пользователя</param>
        /// <param name="history">История</param>
        /// <returns>ID истории</returns>
        string SetHistory(String Token, HistoryModel history);
    }
}
