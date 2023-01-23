using System;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using algorithms.aes.AES;
using algorithms.aes.AESEncoder;
using algorithms.aes.des.DES;

namespace client
{
    public class SenderHandler
    {
        private TcpClient? client;

        private volatile bool connented;

        private SenderForm senderForm;

        private readonly string ip;
        private readonly int port;
        private readonly string name;
        ~SenderHandler()
        {
            if (client != null)
                client.Dispose();
        }
        public SenderHandler(SenderForm senderForm, string ip, int port, string name)
        {
            senderForm.connectBtn.Text = "connected";
            connented = true;
            this.senderForm = senderForm;
            this.ip = ip;
            this.port = port;
            this.name = name;
        }
        public void Close()
        {
            connented = false;
            try
            {
                if (client != null)
                {
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

        public void Send(string mensagem, string planeMensagem)
        {
            if (connented)
            {
                try
                {
                    NetworkStream ns = client.GetStream();
                    // string -> bytes[]
                    byte[] sendBytes = Encoding.Latin1.GetBytes(mensagem);
                    // Encrpt
                    ns.Write(sendBytes, 0, sendBytes.Length);
                    senderForm.AddMessage("Você: " + planeMensagem.Trim());
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
        }

        public void Run()
        {

            int tries = 0;
            while (tries < 5)
            {
                try
                {

                    client = new TcpClient(ip, port);
                    try
                    {
                        NetworkStream ns = client.GetStream();
                        byte[] sendBytes = Encoding.Latin1.GetBytes(name);
                        ns.Write(sendBytes, 0, sendBytes.Length);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                    while (connented)
                    {
                        try
                        {
                            //senderForm.connectBtn.Text = "connected";
                            NetworkStream ns = client.GetStream();
                            byte[] buffer = new byte[client.ReceiveBufferSize];
                            int bytesRead = ns.Read(buffer, 0, client.ReceiveBufferSize);
                            string mensagem = Encoding.Latin1.GetString(buffer, 0, bytesRead);
                            mensagem = mensagem.Remove(0, 10);
                            if (senderForm._AESKey != null)
                            {
                                if (senderForm._AES == null) senderForm._AES = new AES(senderForm._AESKey);
                                var cypherText = AESEncoder.EncodePlaneTextWithoutPadding(mensagem);
                                var platText = senderForm._AES.Decrpyt(cypherText);
                                mensagem = AESEncoder.RemovePaddingAndDecode(platText);
                            }
                            else if (senderForm._DESKey != 0)
                            {
                                var cifir1 = Encoding.Latin1.GetBytes(mensagem);
                                var plain1 = DES.EncryptString(cifir1, senderForm._DESKey.Value, true);
                                mensagem = Encoding.Latin1.GetString(plain1);
                            }
                            senderForm.ProcessMessage("mServidor:" + mensagem);
                            break;
                        }
                        catch (Exception e)
                        {
                            tries += 1;
                            MessageBox.Show(e.Message);
                            // System.Threading.Thread.Sleep(150);
                        }
                    }
                }
                catch (Exception e)
                {
                    tries += 1;
                    MessageBox.Show(e.Message + ", Tries: " + tries);
                    // System.Threading.Thread.Sleep(150);
                }
                if (tries >= 2)
                {
                    MessageBox.Show("Couldn't connect");
                    // senderForm.connectBtn.Text = "Failed";
                    break;
                }

            }

        }

    }
}
