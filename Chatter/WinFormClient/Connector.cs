using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Domain;
using System.Threading;


namespace WinFormClient
{
    class Connector
    {
        TcpClient tcpClientIn;
        TcpClient senderClient;
        NetworkStream tcpStream;
        int key;
        string currentLogin;
        string serverAddress;

        public delegate void SendMessages(Domain.Message mess);

        public event NewMessageEvent NewMessageReceived;

        public Connector(string serverAddress)
        {
            this.serverAddress = serverAddress;
        }

        public bool TryConnect(string login, string pass, out List<string> contacts)
        {
            
            senderClient = new TcpClient(serverAddress, 9050);
            tcpStream =  senderClient.GetStream();
            var sentUser = new User(login, pass);
            currentLogin = login;
            var formatter = new BinaryFormatter();
            formatter.Serialize(tcpStream, 1);
            formatter.Serialize(tcpStream, sentUser);
            var loginResult = (bool)formatter.Deserialize(tcpStream);

            if(loginResult == false)
            {
                contacts = new List<string>();
                return false;
            }
            else
            {
                key = (int)formatter.Deserialize(tcpStream);
                contacts = (List<string>)formatter.Deserialize(tcpStream);
                
                Receive();        
                return true;
            }
        }

        public void Receive()
        {
            Thread ReceiveTread = new Thread(new ThreadStart(ReceiveClient));
            ReceiveTread.Start();
        }
        
        public void ReceiveClient()
        {
            tcpClientIn = new TcpClient(serverAddress, 9050);
            NetworkStream stream = tcpClientIn.GetStream();
            var formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, 2);
                formatter.Serialize(stream, currentLogin);
                formatter.Serialize(stream, key);
                while (true)
                {
                    
                    {
                        var temp = formatter.Deserialize(stream);
                        var m = (Message)temp;
                        NewMessageReceived(new NewMessageArgs(m));
                    }
                    
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                stream.Close();
                tcpClientIn.Close();
            }
        }

        public void Send(NewMessageArgs mess)
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(tcpStream, mess.Message);
        }
        // senderClent
        //event new message from server

        // bool tryLogin (in login, pass , out list contact)
        // senderClient = new tcpclient
        // send User
        //if(inccorect User)
        //then return false
        //else 
        // get key from server
        // get contacts from server
        // assign contacts to out param

        //start receiveThread 
        //return true


        //void OnNewMessageSent -- handler to event from window
        // sender.stream
        // form.ser(mess)
    }
}
