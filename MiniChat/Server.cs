using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MiniChat
{
    internal class Server
    {
        private readonly int PORT = 0;
        public Server(int port)
        {
            PORT = port;
        }

        public void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Loopback, PORT);
            server.Start();

            while (true)
            {
                TcpClient socket = server.AcceptTcpClient();
                Task.Run(() =>
                {
                    TcpClient LocalSocket = socket;
                    DoClient(LocalSocket);
                });
                //DoClient(socket);
            }
        }

        private void DoClient(TcpClient socket)
        {
            String nameofclient = "";
            String nameofserver = "";

            using (NetworkStream ns = socket.GetStream())
            using (StreamReader sr = new StreamReader(ns))
            using (StreamWriter sw = new StreamWriter(ns))
            {
                /*
                 *  Hello who are we
                 */
                 // server
                Console.Write("Who are the server: ");
                String myLine = Console.ReadLine();
                nameofserver = myLine;
                // client
                String line = sr.ReadLine();
                String[] sline = line.Split(' ');
                nameofclient = sline[1];
                sw.WriteLine($"Hello: {nameofserver}");
                sw.Flush();

                while (!String.IsNullOrWhiteSpace(line) &&
                       !line.Trim().ToLower().Equals("stop"))
                {
                    // start chat loop

                    // read from client
                    line = sr.ReadLine();
                    Console.WriteLine($"{nameofclient}: {line}");

                    // write to client
                    Console.Write($"{nameofserver}: ");
                    myLine = Console.ReadLine().Trim() ?? ""; // if readline = null then empty string
                    sw.WriteLine(myLine);
                    sw.Flush();

                    if (myLine.ToLower().Equals("stop"))
                    {
                        line = "stop";
                    }
                }
            }
        }
    }
}