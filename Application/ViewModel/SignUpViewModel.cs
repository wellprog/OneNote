using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OneNote.Application.ViewModel
{
    public class SignUpViewModel
    {
        public string UserName { get; set; }
        public string EMail { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Age { get; set; }
        public string Avatar { get; set; }
        public bool Gender { get; set; }
        public string Status { get; set; }
        public ICommand SignUp { get; set; }

        public SignUpViewModel()
        {
            UserName = "Enter your Username";
            EMail = "Enter your EMail";
            Password = "Enter your Password";
            Phone = "Enter your Phone";
            Age = "Enter your Age";
            //Gender = "Enter ...";
            Status = "Enter your Status";
            SignUp = new RoutedCommand();
        }
        //<!--UserName, EMail, Password, Phone, Age, Avatar, Gender, Status -->
    }
}
