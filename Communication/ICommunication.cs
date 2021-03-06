﻿using OneNote.Communication.Model;
using OneNote.Model;
using OneNote.SQLite.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace OneNote.Communication
{

    /// <summary>
    /// 1 - Конструктор принимает базовый URL
    /// 2 - Для отправки запроса используется POST базовый URL + название функции
    /// 3 - Все параметры отправляются POST www-form-url-encoded или multipart-form-data
    /// 4 - Переменные при отправке имеют название переменных из параметров функций
    /// </summary>
    public interface ICommunication
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>Если все ок, то возвращается токен авторизации, если нет то NULL</returns>
        string Authorize(string login, string password, CancellationToken cToken);

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="user">Пользователя целиком</param>
        /// <returns>Если все ок, то возвращается токен авторизации, если нет то NULL</returns>
        string Register(User user, CancellationToken cToken);

        /// <summary>
        /// Получение деталей пользователя
        /// </summary>
        /// <param name="Token"></param>
        /// <returns></returns>
        User GetUserDetails(string token, CancellationToken cToken);

        /// <summary>
        /// Получение всех секций
        /// </summary>
        /// <param name="Token">Токен пользователя</param>
        /// <returns>Модель секций</returns>
        SectionModel GetSections(string token, CancellationToken cToken);
        BookModel GetBooks(string token, CancellationToken cToken);
        PageModel GetPages(string token, CancellationToken cToken);
        
        /// <summary>
        /// Получение истории изменений по таблице и последней версии
        /// </summary>
        /// <param name="Token">Токен пользователя</param>
        /// <param name="Table">Наименование таблицы</param>
        /// <param name="LastID">Последний ID истории</param>
        /// <returns>Массив истории</returns>
        HistoryModel GetHistory(string token, string table, string lastID, CancellationToken cToken);

        /// <summary>
        /// Отправка истории
        /// </summary>
        /// <param name="Token">Токен пользователя</param>
        /// <param name="history">История</param>
        /// <returns>ID истории</returns>
        string SetBookHistory(String token, HistoryModel history, CancellationToken cToken);
        string SetSectionHistory(String token, HistoryModel history, CancellationToken cToken);
        string SetPageHistory(String token, HistoryModel history, CancellationToken cToken);
    }
}
