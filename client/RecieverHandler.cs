using algorithms.aes.AES;
using algorithms.aes.AESEncoder;
using algorithms.aes.des.DES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace client
{
    public class RecieverHandler
    {
        private TcpClient client;

        private ResiverForm resiverForm;
        private readonly string ip;
        private string name;

        private volatile bool connected;

        public RecieverHandler(ResiverForm resiverForm, TcpClient client)
        {
            connected = true;
            this.resiverForm = resiverForm;
            ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            this.client = client;
        }

        public string GetName()
        {
            return name;
        }

        public void Close()
        {
            try
            {
                if (client != null)
                {
                    Send("e", "");
                    connected = false;
                    client.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                client = null;
            }
        }

        public void Send(string tipo, string mensagem)
        {
            if (connected)
            {
                try
                {
                    NetworkStream ns = client.GetStream();
                    byte[] sendBytes = Encoding.Latin1.GetBytes(tipo + mensagem);
                    ns.Write(sendBytes, 0, sendBytes.Length);
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void Run()
        {
            int mensagensVazias = 0;
            while (connected)
            {
                try
                {
                    NetworkStream ns = client.GetStream();
                    byte[] buffer = new byte[client.ReceiveBufferSize];
                    int bytesRead = ns.Read(buffer, 0, client.ReceiveBufferSize);
                    string mensagem = Encoding.Latin1.GetString(buffer, 0, bytesRead);
                    if (resiverForm._AESKey != null)
                    {
                        if (resiverForm._AES == null) resiverForm._AES = new AES(resiverForm._AESKey);
                        var cypherText = AESEncoder.EncodePlaneTextWithoutPadding(mensagem);
                        var platText = resiverForm._AES.Decrpyt(cypherText);
                        mensagem = AESEncoder.RemovePaddingAndDecode(platText);
                    }
                    else if (resiverForm._DESKey != 0)
                    {
                        var cifir1 = Encoding.Latin1.GetBytes(mensagem);
                        var plain1 = DES.EncryptString(cifir1, resiverForm._DESKey, true);
                        mensagem = Encoding.Latin1.GetString(plain1);
                    }
                    if (!string.IsNullOrEmpty(mensagem))
                    {
                        mensagensVazias = 0;
                        resiverForm.AddMensagem(name + ": " + mensagem);
                        //resiverForm.sender.Send("m", nome + ": " + mensagem);
                    }
                    else if (mensagensVazias == 3)
                    {
                        throw new SocketException();
                    }
                    else
                    {
                        mensagensVazias++;
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
