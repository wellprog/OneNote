using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace OneNote.Application.ViewModel
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserName)));
                canClick();
            }
        }
        private string _eMail;
        public string EMail
        {
            get
            {
                return _eMail;
            }
            set
            {
                _eMail = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EMail)));
                canClick();
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                canClick();
            }
        }
        private string _phone;
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Phone)));
                canClick();
            }
        }
        private string _age;
        public string Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Age)));
                canClick();
            }
        }
        private string _avatar;
        public string Avatar
        {
            get
            {
                return _avatar;
            }
            set
            {
                _avatar = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Avatar)));
                canClick();
            }
        }
        private bool _gender;
        public bool Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Gender)));
            }
        }
        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
                canClick();
            }
        }
        public ClickCommand SignUp { get; }

        private ICommunication _communication;
        private IEnviroment _enviroment;

        public SignUpViewModel()
        {
            SignUp = new ClickCommand(OnExecuted);
            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();
        }

        protected void OnExecuted(object param)
        {
            var user = new Model.User()
            {
                Age = int.Parse(Age),
                Avatar = Avatar,
                EMail = EMail,
                IsDeleted = false,
                Password = Password,
                Phone = Phone,
                Status = Status,
                UserName = UserName,
                Gender = Gender
            };

            var token = _communication.Register(user, new CancellationTokenSource().Token);

            if (string.IsNullOrWhiteSpace(token))
            {
                new MessageBox(5, "Введенные данные не верны").Show();
                return;
            }

            var currentUser = _communication.GetUserDetails(token, new CancellationTokenSource().Token);
            if (currentUser == null)
            {
                new MessageBox(5, "Не получилось получить текущего пользователя").Show();
                return;
            }

            _enviroment.UserToken = token;
            _enviroment.CurrentUser = currentUser;

            new GeneralWindow().Show();
            //Application.Close(); //Отправить окну авторизации сигнал, что ему нужно закрыться //TODO
        }

        private void canClick()
        {
            bool isCorrectRegex(string str, string regex)
            {
                if (string.IsNullOrWhiteSpace(str))
                    return false;

                MatchCollection phoneRegexResult = Regex.Matches(str, regex, RegexOptions.IgnoreCase);
                if (phoneRegexResult.Count != 1)
                    return false;
                    
                return true;
            }
            
            if (string.IsNullOrWhiteSpace(UserName)
            || string.IsNullOrWhiteSpace(EMail) || !isCorrectRegex(EMail, @"^[-a-z0-9!#$%&'*+/=?^_`{|}~]+(\.[-a-z0-9!#$%&'*+/=?^_`{|}~]+)*@([a-z0-9]([-a-z0-9]{0,61}[a-z0-9])?\.)*(aero|arpa|asia|biz|cat|com|coop|edu|gov|info|int|jobs|mil|mobi|museum|name|net|org|pro|tel|travel|[a-z][a-z])$")
            || string.IsNullOrWhiteSpace(Password)
            || string.IsNullOrWhiteSpace(Phone) || !isCorrectRegex(Phone, @"^((8|\+7)[\d]{10})$")
            || string.IsNullOrWhiteSpace(Age)
            || string.IsNullOrWhiteSpace(Status) || Status.Length >= 64)
                SignUp.SetCanExecuted(false);
            else
                SignUp.SetCanExecuted(true);
        }
    }
}
