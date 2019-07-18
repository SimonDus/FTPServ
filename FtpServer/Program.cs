using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RvFtpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            FtpServer server = new FtpServer();
            {
                server.Start();
                Console.ReadLine();        
            }
            
        }
    }
}
