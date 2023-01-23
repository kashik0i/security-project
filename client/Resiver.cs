using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace client
{
    public partial class ResiverForm
    {
        public class Resiver
        {
            private TcpListener listener;
            private TcpClient client;

            private volatile bool start;

            private ResiverForm resiverForm;
            private readonly int port;

            public Resiver(ResiverForm resiverForm, int port)
            {
                start = true;
                this.resiverForm = resiverForm;
                this.port = port;
            }

            public void Close()
            {
                start = false;
                try
                {
                    if (client != null)
                    {
                        client.Close();
                    }
                    if (listener != null)
                    {
                        listener.Stop();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    client = null;
                    listener = null;
                }
            }

            public void Send(string mensagem,string planeMensagem)
            {
                if (start)
                {
                    try
                    {
                        
                        resiverForm.sender.Send("m", "Servidor:" + mensagem);
                        resiverForm.AddMensagem("Você: " + planeMensagem.Trim());
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            private IPAddress GetLocalIP()
            {
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());
                foreach (IPAddress localIP in localIPs)
                {
                    if (localIP.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return localIP;
                    }
                }
                return null;
            }

            public void Run()
            {
                try
                {
                    IPAddress ipLocal = GetLocalIP();
                    try
                    {
                        listener = new TcpListener(ipLocal, port);
                        listener.Start();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    resiverForm.AddMensagem("Chat Started.");
                    while (start)
                    {
                        client = listener.AcceptTcpClient();
                        RecieverHandler sender = new RecieverHandler(resiverForm, client);
                        resiverForm.sender = sender;
                        Thread thread = new Thread(new ThreadStart(resiverForm.sender.Run));
                        thread.Start();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }
    }
}
