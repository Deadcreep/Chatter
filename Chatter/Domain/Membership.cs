using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Domain
{
    public static class Membership
    {
        public static List<User> ClientList = new List<User>()
                                                                {
                                                                new User("Alex", "qwerty"),
                                                                new User("Leo", "qwe"),
                                                                new User("Max", "zxc"),
                                                                };

        public static bool CheckLogin(User user)
        {
            if ((user.Login != null) & (user.Password != null) & (Membership.ClientList.Any(client => client.Login == user.Login)) &
                (Membership.ClientList.Any(client => client.Login == user.Login && client.Password == user.Password)))
            {
                return Membership.ClientList.Any(client => client.Login == user.Login);
            }
            else return false;                      
        }
    }
}
