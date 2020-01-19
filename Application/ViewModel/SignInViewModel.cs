using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneNote.Application.ViewModel
{
    public class SignInViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public ICommand SignIn { get; set; }

        public SignInViewModel()
        {
            SignIn = new RoutedCommand();
        }
    }
}
