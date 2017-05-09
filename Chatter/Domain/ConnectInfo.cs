using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class ConnectInfo
    {
        public User user;
        public int port;

        public ConnectInfo()
        {
        }
    }
}
