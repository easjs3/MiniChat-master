using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniChatClient
{
    class Program
    {
        private const int PORT = 7070;
        static void Main(string[] args)
        {
            Client server = new Client(PORT);
            server.Start();

            Console.ReadLine();
        }
    }
}
