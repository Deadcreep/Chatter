using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;
using System.Configuration;


namespace WinFormClient
{
    public partial class MainForm : Form
    {
        public delegate void UpdateMessages(Domain.Message mess);
        event NewMessageEvent NewMessageSent;
        string selectedContact;
        Dictionary<string, List<Domain.Message>> messageHistory;
        string currentLogin;        

        public MainForm()
        {
            InitializeComponent();
            messageHistory = new Dictionary<string, List<Domain.Message>>();
            messageHistory.Add(User.BroadcastLogin, new List<Domain.Message>());
        }

        private void tryLoginButton_Click(object sender, EventArgs e)
        {
            currentLogin = loginTextBox.Text;
            var tempPass = passwordTextBox.Text;
            List<string> contactsList;
            var connector = new Connector(ConfigurationManager.AppSettings["serverAddress"]);

            connector.NewMessageReceived += Connector_NewMessageReceived;
            NewMessageSent += connector.Send;

            if (connector.TryConnect(currentLogin, tempPass, out contactsList))
            {
                contactsListBox.DataSource = contactsList;
            }
            else
            {
                MessageBox.Show("Wrong login or password");
            }
        }

        private void Connector_NewMessageReceived(NewMessageArgs args)
        {
            if (args.Message.RecipientName == User.BroadcastLogin)
            {
                messageHistory[User.BroadcastLogin].Add(args.Message);
            }
            else
            {
                if (!messageHistory.ContainsKey(args.Message.SenderName))
                {
                    messageHistory.Add(args.Message.SenderName, new List<Domain.Message>());
                }         
            
                messageHistory[args.Message.SenderName].Add(args.Message);
            }      
            UpdateMessages temp = new UpdateMessages(UpdateView);
            this.Invoke(temp, args.Message);
        }

        private void UpdateView(Domain.Message mess)
        {
            if(mess.RecipientName == User.BroadcastLogin && contactsListBox.SelectedItem.ToString() == User.BroadcastLogin)
            {
                UpdateMessagesTextBox(mess.ToString());
            }
            else if (contactsListBox.SelectedItem.ToString() == mess.SenderName && mess.RecipientName != User.BroadcastLogin)
            {
                UpdateMessagesTextBox(mess.ToString());
            }
            else
            {
                //contactsListBox.
            }
        }

        private void UpdateMessagesTextBox(string message)
        {
            MessagesTextBox.AppendText(message);
            MessagesTextBox.AppendText(System.Environment.NewLine);
            MessagesTextBox.AppendText(System.Environment.NewLine);
        }

        private void contactsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedContact = contactsListBox.SelectedItem.ToString();
            MessagesTextBox.Clear();
            if (messageHistory.ContainsKey(contactsListBox.SelectedItem.ToString()))
            {
                for (int i = 0; i <= messageHistory[selectedContact].Count - 1; i++)
                {
                    MessagesTextBox.AppendText(messageHistory[selectedContact][i].ToString());
                    MessagesTextBox.AppendText(System.Environment.NewLine);
                    MessagesTextBox.AppendText(System.Environment.NewLine);
                }
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            SendMessage();
        }               

        private void newMessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Modifiers == Keys.Control && e.KeyCode == Keys.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            if (!messageHistory.ContainsKey(selectedContact))
            {
                messageHistory.Add(selectedContact, new List<Domain.Message>());
            }
            var messText = newMessageTextBox.Text;
            var mess = new Domain.Message(messText, currentLogin, selectedContact);
            messageHistory[selectedContact].Add(mess);
            UpdateMessagesTextBox(mess.ToString());
            NewMessageSent(new NewMessageArgs(mess));
            newMessageTextBox.Clear();
        }
    }
}
