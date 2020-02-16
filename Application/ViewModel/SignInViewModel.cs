using OneNote.Communication;
using OneNote.Communication.Helpers;
using OneNote.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneNote.Application.ViewModel
{
    public class SignInViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _login = "Enter your Login";
        public string Login {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Login)));
                canClick();
            }
        }
        private string _password = "Enter your Password";
        public string Password {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Password)));
                canClick();
            }
        }
        public ClickCommand SignIn { get; }

        private ICommunication _communication;
        private IEnviroment _enviroment;

        public SignInViewModel() {
            SignIn = new ClickCommand(OnExecuted);
            _communication = ClassLoader.Instance.GetElement<ICommunication>();
            _enviroment = ClassLoader.Instance.GetElement<IEnviroment>();
        }

        protected void OnExecuted(object param)
        {
            string token = _communication.Authorize(Login, Password);
            if (string.IsNullOrWhiteSpace(token))
            {
                new MessageBox(5, "Логин или пароль не верен").Show();
                return;
            }

            var currentUser = _communication.GetUserDetails(token);
            if (currentUser == null)
            {
                new MessageBox(5, "Не получилось получить текущего пользователя").Show();
                return;
            }

            _enviroment.UserToken = token;
            _enviroment.CurrentUser = currentUser;

            new GeneralWindow().Show();
        }

        private void canClick()
        {
            if (!string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password))
                SignIn.SetCanExecuted(true);
            else
                SignIn.SetCanExecuted(false);
        }
    }
}
