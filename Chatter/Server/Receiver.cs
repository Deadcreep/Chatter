using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Domain;
using System.Threading;

namespace Server
{
    class Receiver
    {
        TcpListener server;
        ConcurrentDictionary<string, int> connectionKeys;
        ConcurrentDictionary<string, ConcurrentQueue<Message>> messagesForRecepient;

        public Receiver()
        {
            server = new TcpListener(IPAddress.Parse("192.168.1.105"), 9050);
            messagesForRecepient = new ConcurrentDictionary<string, ConcurrentQueue<Message>>();
            foreach (var client in Membership.ClientList)
            {
                messagesForRecepient.AddOrUpdate(client.Login, new ConcurrentQueue<Message>(), (key, value) => value);
            }
            connectionKeys = new ConcurrentDictionary<string, int>();
        }

        public void Start()
        {
            try
            {
                server.Start();
                Console.WriteLine("Server start \t \t");

                while (true)
                {
                    if (!server.Pending())
                    {
                        continue;
                    }
                    else
                    {
                        TcpClient tcpCl = server.AcceptTcpClient();
                        Thread ReceiveThread = new Thread(new ParameterizedThreadStart(ProcessNewClient));
                        ReceiveThread.Start((object)tcpCl);
                        Console.WriteLine("Thread created \t \t");
                    }

                    Thread.Sleep(10);
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine(se);
            }
            finally
            {
                server.Stop();
            }
        }

        private void ProcessNewClient(object objClient)
        {
            var client = (TcpClient)objClient;
            Console.WriteLine("Client accept \t \t");
            NetworkStream inputStream = client.GetStream();
            var formatter = new BinaryFormatter();

            var connectionType = (int)formatter.Deserialize(inputStream);
            if (connectionType == 1)
            {
                ReceiveMessages(client);
            }
            else if (connectionType == 2)
            {
                Thread messageThread = new Thread(new ParameterizedThreadStart(SendMessages));
                messageThread.Start(client);
            }
        }

        private void ReceiveMessages(object objTcpClient)
        {
            var client = (TcpClient)objTcpClient;
            NetworkStream inputStream = client.GetStream();
            var currentUser = new User();
            bool flagLogin = false;
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                while (!flagLogin)
                {
                    if (client.GetRemoteState() == System.Net.NetworkInformation.TcpState.Established)
                    {
                        currentUser = (User)formatter.Deserialize(inputStream);
                        flagLogin = Membership.CheckLogin(currentUser);
                        Console.WriteLine(flagLogin);
                        formatter.Serialize(inputStream, flagLogin);
                        Console.WriteLine(flagLogin);
                    }
                    else
                    {
                        Console.WriteLine("Connection was closed");
                        break;
                    }
                }

                if (flagLogin)
                {
                    var key = new Random().Next();
                    connectionKeys.AddOrUpdate(currentUser.Login, key, (k, v) => v);
                    formatter.Serialize(inputStream, key);
                    var contacts = Membership.ClientList.Where(tempClient => tempClient.Login != currentUser.Login).Select(x => x.Login).ToList();
                    formatter.Serialize(inputStream, contacts);
                    while (true)
                    {
                        if (client.GetRemoteState() == System.Net.NetworkInformation.TcpState.Established)
                        {
                            Message m = (Message)formatter.Deserialize(inputStream);
                            if(messagesForRecepient.ContainsKey(m.RecipientName) == false)
                            {
                                messagesForRecepient.AddOrUpdate(m.RecipientName, new ConcurrentQueue<Message>(), (k, v) => v);
                            }
                            messagesForRecepient[m.RecipientName].Enqueue(m);
                            Console.WriteLine(m + "\t");
                            Console.WriteLine("Message received \t \t");
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
                inputStream.Close();
                client.Close();
            }
        }

        private void SendMessages(object objTcpClient)
        {
            var client = (TcpClient)objTcpClient;
            NetworkStream outStream = client.GetStream();

            try
            {
                BinaryFormatter formatter = new BinaryFormatter();

                var login = (string)formatter.Deserialize(outStream);
                var key = (int)formatter.Deserialize(outStream);
                var ourKey = connectionKeys[login];
                if (key == ourKey)
                {
                    while (true)
                    {
                        if (client.GetRemoteState() == System.Net.NetworkInformation.TcpState.Established)
                        {
                            var queue = messagesForRecepient[login];
                            Message mess;
                            if (queue.TryPeek(out mess))
                            {
                                if (queue.TryDequeue(out mess))
                                {
                                    formatter.Serialize(outStream, mess);
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                        Thread.Sleep(10);
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
                client.Close();
            }
        }
    }
}