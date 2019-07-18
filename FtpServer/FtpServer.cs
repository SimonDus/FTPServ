using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RvFtpServer
{
    public class FtpServer
    {
        //Ecoute les connexions TCP.
        private TcpListener listener;

        public FtpServer()
        {

        }

        public void Start()
        {
            // -> A implémenter : Selection de la carte réseau.
            listener = new TcpListener(IPAddress.Any, 21);
            listener.Start();
            listener.BeginAcceptTcpClient(HandleAcceptTcpClient, listener);
        }

        public void Stop()
        {
            if(listener != null)
            {
                listener.Stop();
            }
        }

        // Gere en async multiples connexions.
        private void HandleAcceptTcpClient(IAsyncResult result)
        {
            listener.BeginAcceptTcpClient(HandleAcceptTcpClient, listener);
            TcpClient client = listener.EndAcceptTcpClient(result);

            ClientConnection connection = new ClientConnection(client);

            ThreadPool.QueueUserWorkItem(connection.HandleClient, client);
        }

    }
}
