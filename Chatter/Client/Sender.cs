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

namespace Client
{
    class Sender
    {
        User currentUser = new User();
        TcpClient tcpClientOut;
        TcpClient tcpClientIn;
        List<string> contactsList;
        int connectionKey;
        bool flagSendMessage = true;
        public Sender()
        {
            tcpClientOut = new TcpClient("127.0.0.1", 9050);
            contactsList = new List<string>();
        }

        public void StartCommunication()
        {
            NetworkStream outStream = tcpClientOut.GetStream();
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                formatter.Serialize(outStream, 1);

               
                bool loginResult = false;


                while (!loginResult)
                {
                    if (tcpClientOut.GetLocalState() == System.Net.NetworkInformation.TcpState.Established)
                    {
                        Console.WriteLine("Enter login \t");
                        currentUser.Login = Console.ReadLine();
                        Console.WriteLine("Login accepted \t Enter password \t");
                        currentUser.Password = Console.ReadLine();
                        Console.WriteLine("Password accepted ");
                        
                        formatter.Serialize(outStream, currentUser);
                        loginResult = (bool)formatter.Deserialize(outStream);

                        if (loginResult)
                        {
                            break;
                        }

                        if (!loginResult)
                        {
                            Console.WriteLine("Wrong passord or login");
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                if (loginResult)
                {
                    connectionKey = (int)formatter.Deserialize(outStream);
                    Console.WriteLine(connectionKey);

                    Receive();

                    contactsList = (List<string>)formatter.Deserialize(outStream);

                    foreach(var temp in contactsList)
                    {
                        Console.WriteLine(temp);
                    }

                    while (flagSendMessage)
                    {
                        if (tcpClientOut.GetLocalState() == System.Net.NetworkInformation.TcpState.Established)
                        {
                            Send(outStream, currentUser.Login);                            
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                outStream.Close();
                tcpClientOut.Close();
                Console.ReadKey();
            }

        }

        public void Send(NetworkStream strm, string senderName)
        {
            var text = Console.ReadLine();
            
            var recipient = Console.ReadLine();
            var sender = senderName;
            Message m = new Message(text, sender, recipient);
            Console.WriteLine("Message created \t");

            IFormatter formatter = new BinaryFormatter();
            Console.WriteLine("Formatter created \t");

            formatter.Serialize(strm, m);
            Console.WriteLine("Serialized \t \t");
        }

        public void Receive()
        {
            Thread ReceiveTread = new Thread(new ThreadStart(ReceiveClient));
            ReceiveTread.Start();
        }

        public void ReceiveClient()
        {
            tcpClientIn = new TcpClient("127.0.0.1", 9050);
            NetworkStream stream = tcpClientIn.GetStream();           
            var formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(stream, 2);
                formatter.Serialize(stream, currentUser.Login);
                formatter.Serialize(stream, connectionKey);
                while (true)
                {
                    if (tcpClientIn.GetLocalState() == System.Net.NetworkInformation.TcpState.Established)
                    {
                        var temp = formatter.Deserialize(stream);
                        var m = (Message)temp;
                        Console.WriteLine(m.MessageText + "\t");
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                stream.Close();
                tcpClientIn.Close();                
            }
        }
    }
}

