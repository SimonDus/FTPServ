using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RvFtpServer
{
    public class ClientConnection
    {
        private TcpClient controlClient;

        private NetworkStream controlStream;
        private StreamReader controlReader;
        private StreamWriter controlWriter;

        private string userName;

        public ClientConnection(TcpClient client)
        {
            controlClient = client;
            controlStream = controlClient.GetStream();

            controlReader = new StreamReader(controlStream);
            controlWriter = new StreamWriter(controlStream);
        }

        public ClientConnection()
        {
        }

        public void HandleClient(object obj)
        {
            controlWriter.WriteLine("220 : Connected to  FTP SERVER .");
            controlWriter.Flush();

            string line;

            try
            {
                while (!string.IsNullOrEmpty(line = controlReader.ReadLine()))
                {
                    string response = null;

                    string[] command = line.Split(' ');

                    string cmd = command[0].ToLowerInvariant();
                    string arguments = command.Length > 1 ? line.Substring(command[0].Length + 1) : null;

                    if (string.IsNullOrWhiteSpace(arguments))
                        arguments = null;

                    if (string.IsNullOrWhiteSpace(arguments))
                        arguments = null;

                    //FTP Commandes ==> à modifier.
                    if (response != null)
                    {
                        switch (cmd)
                        {
                            case "USER":
                                response = User(arguments);
                                break;
                            case "PASS":
                                response = Password(arguments);
                                break;
                            case "CWD":
                                response = ChangeWorkingDirectory(arguments);
                                break;
                            case "CDUP":
                                response = ChangeWorkingDirectory("..");
                                break;
                            case "PWD":
                                response = "27 \"/\" is the current directory.";
                                break;
                            case "QUIT":
                                response = "221 Service Closing the connection";
                                break;

                            default:
                                response = "502 Unknown Command.";
                                break;
                        }
                    }

                    if (controlClient == null || !controlClient.Connected)
                    {
                        break;
                    }
                    else
                    {
                        controlWriter.WriteLine(response);
                        controlWriter.Flush();

                        if (response.StartsWith("221"))
                        {
                            break;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
        //gestion login & pswd
        private string User(string _userName)
        {
            userName = _userName;

            return "331 Username Correct. Enter Password.";
        }

        private string Password(string password)
        {
            if(true)
            {
                return "230 User Logged In.";
            }
            else
            {
                return "530 Authentication Failed";
            }
        }
        
        private string ChangeWorkingDirectory(string pathName)
        {
            return "250 Changed to new directory.";
        }
    }
}
