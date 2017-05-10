using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace WinFormClient
{
    delegate void NewMessageEvent(NewMessageArgs args);

    class NewMessageArgs : EventArgs
    {
        public Message Message;

        public NewMessageArgs(Message mess)
        {
            Message = mess;
        }
    }
}
