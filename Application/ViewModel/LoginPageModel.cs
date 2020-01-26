using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OneNote.Application.ViewModel
{
    public class LoginPageModel
    {
        public SignInViewModel SignInModel { get; set; } = new SignInViewModel();
        public SignUpViewModel SignUpModel { get; set; } = new SignUpViewModel();
    }
}
