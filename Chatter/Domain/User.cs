using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class User
    {
        public const string BroadcastLogin = "Broadcast";
        public string Login { get;  set; }
        public string Password { get;  set; }        

        public User(string login, string pass)
        {
            Login = login;
            Password = pass;            
        }

        public User()
        {
        }
    }
}
