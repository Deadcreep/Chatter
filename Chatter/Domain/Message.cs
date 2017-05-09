using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    [Serializable]
    public class Message
    {
        public string MessageText { get; private set; }
        public string SenderName { get; private set; }
        public string RecipientName { get; private set; }


        public Message (string text, string sender, string recipient)
        {
            MessageText = text;
            SenderName = sender;
            RecipientName = recipient;         
        }

        public Message()
        {
        }
    } 
}
