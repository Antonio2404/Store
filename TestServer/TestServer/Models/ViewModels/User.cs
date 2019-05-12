using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestServer.Models.ViewModels
{
    public class UserVM
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public string Token { get; set; }
        public string Role { get; set; }

        public UserVM() { }
        public UserVM(User user)
        {
            this.Login = user.Login;
            this.Password = user.Password;
            this.Token = user.Token;
            this.Role = user.Role!=null?user.Role.Name:null ;
        }
    }
}
