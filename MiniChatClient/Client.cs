using System;
using System.IO;
using System.Net.Sockets;

namespace MiniChatClient
{
    internal class Client
    {
        private readonly int PORT;

        public Client(int port)
        {
            this.PORT = port;
        }

        public void Start()
        {
            String nameofclient = "";
            String nameofserver = "";
            Console.Write("Who are you ");
            nameofclient = Console.ReadLine();

            using (TcpClient socket = new TcpClient("localhost", PORT))
            using (NetworkStream ns = socket.GetStream())
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns){AutoFlush = true})
            {
                /*
                 * Hello between client server
                 */

                // client
                String line = "";
                String myLine = nameofclient;
                sw.WriteLine($"Hello: {nameofclient}");

                // server
                String fromServer = sr.ReadLine();
                String[] strs = fromServer.Split(' ');
                nameofserver = strs[1];

                while (!myLine.Trim().ToLower().Equals("stop"))
                {
                    // start chat loop
                    Console.Write($"{nameofclient}: ");
                    myLine = Console.ReadLine().Trim();
                    sw.WriteLine(myLine);

                    if (myLine.ToLower().Equals("stop"))
                    {
                        line = "stop";
                    }
                    else
                    {
                        line = sr.ReadLine();
                    }
                    Console.WriteLine($"{nameofserver}: {line}");
                }
            }
            

        }
    }
}