using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using algorithms.aes.AES;
using algorithms.aes.AESEncoder;
using algorithms.aes.des.DES;

namespace client
{
    public enum Algorithm
    {
        RSA, AES, ELGAMAL, DES
    }
    public partial class SenderForm : Form
    {
        public int _Port = 8080;
        public BigInteger _keyRSA;
        public byte[,] _AESKey;
        public uint? _DESKey;
        public BigInteger _keyElGamal;
        public SenderHandler sender;
        public AES _AES;
        public DES _DES;

        public SenderForm()
        {
            InitializeComponent();
        }

        public void AddMessage(string text)
        {
            BeginInvoke(new Action(() =>
            {
                if (richTextBox1.Text.Length == 0)
                {
                    richTextBox1.AppendText(text);
                }
                else if (!string.IsNullOrEmpty(text))
                {
                    richTextBox1.AppendText("\n" + text);
                }
            }));
        }
        public void ProcessMessage(string mensagem)
        {
            if (!string.IsNullOrEmpty(mensagem))
            {
                switch (mensagem[0])
                {
                    case 'm':
                        AddMessage(mensagem.Substring(1));
                        break;
                }
            }
        }
        private void Conection(string ip, int port, string name)
        {
            sender = new SenderHandler(this, ip, port, name);

            Thread thread = new Thread(new ThreadStart(sender.Run));

            thread.Start();

        }
        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            string ip = "";
            string name = "";
            try
            {
                ip = IPAddress.Parse(Reciever_IP.Text).ToString();
                name = Sender_Name.Text.Trim();
            }
            catch (System.FormatException exc)
            {
                MessageBox.Show(exc.Message);

            }
            if (ip == "")
            {
                MessageBox.Show("ip not set");
                return;
            }
            if (name == "")
            {
                MessageBox.Show("name not set");
                return;
            }
            connectBtn.Text = "connecting";
            Conection(ip, _Port, name);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void SenderForm_Load(object sender, EventArgs e)
        {

        }
        private void SendMessage()
        {

            string mensagem = richTextBox2.Text;
            string planeMensagem = richTextBox2.Text;
            if (isDES.Checked)
            {
                if (_DESKey == null)
                {
                    MessageBox.Show("DES Key Not Set");
                    return;
                }
                byte[] msgBytes1 = Encoding.Latin1.GetBytes(mensagem);
                var cifir1 = DES.EncryptString(msgBytes1, _DESKey.Value, false);
                mensagem += Encoding.Latin1.GetString(cifir1);

            }
            else if (isAES.Checked)
            {
                if (_AESKey == null)
                {
                    MessageBox.Show("AES Key Not Set");
                    return;
                }
                if (_AES == null)
                {
                    _AES = new AES(_AESKey);
                }
                var planeText = AESEncoder.EncodePlaneText(mensagem);
                var cypherText = _AES.Encrypt(planeText);
                mensagem = AESEncoder.DecodeCypherText(cypherText);

            }
            else if (isElGamal.Checked)
            {
                MessageBox.Show("unimplemented");
                return;
            }
            else if (isRSA.Checked)
            {
                MessageBox.Show("unimplemented");
                return;
            }



            try
            {
                richTextBox2.Clear();
                sender.Send(mensagem, planeMensagem.Trim());
                richTextBox2.Focus();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show("Não foi possível enviar a mensagem.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void GetKey(object sender, EventArgs e)
        {
            if (!isDES.Checked && !isAES.Checked)
            {
                MessageBox.Show("DES or AES algorithm must be selected for this operation");
                return;
            }
            string ip = "";
            int port = -1;
            try
            {
                ip = IPAddress.Parse(TTP_IP.Text).ToString();
                port = Int32.Parse(TTP_PORT.Text);
            }
            catch (System.FormatException exc)
            {
                MessageBox.Show(exc.Message);

            }
            if (ip == "")
            {
                MessageBox.Show("ip not set");
                return;
            }
            if (port == -1)
            {
                MessageBox.Show("Port not set");
                return;
            }


            TcpClient? client;

            try
            {
                client = new TcpClient(ip, port);
                
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                return;
            }
            //for (int tries = 0; tries < 5; tries++)
            //{
            //}

            if (client == null)
            {
                MessageBox.Show("Coulnd't Connect to TTP, unexpected error");
                return;
            }

            NetworkStream ns = client.GetStream();
            string msg = "";

            if (isDES.Checked)
            {
                msg = "DES";
            }
            else if (isAES.Checked)
            {
                msg = "AES";
            }

            byte[] sendBytes = ASCIIEncoding.Latin1.GetBytes(msg);
            // Encrpt
            ns.Write(sendBytes, 0, sendBytes.Length);


            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = ns.Read(bytesToRead, 0, client.ReceiveBufferSize);
            MessageBox.Show("DES Key : " + BitConverter.ToUInt32(bytesToRead));//Encoding.Latin1.GetString(bytesToRead, 0, bytesRead));
            //MessageBox.Show("Received : " + bytesRead);
            if (isDES.Checked)
            {
              
                _DESKey = BitConverter.ToUInt32(bytesToRead);

                _AESKey = null;
            }
            else if (isAES.Checked)
            {
                _AESKey = new byte[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        _AESKey[j, i] = bytesToRead[4 * i + j];
                    }
                }

                _DESKey = 0;
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }


        private void autofill_btn_Click(object sender, EventArgs e)
        {
            string hostname = Dns.GetHostEntry(Dns.GetHostName()).AddressList
           .First(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
           .ToString();
            Reciever_IP.Text = hostname;
            TTP_IP.Text = hostname;
            TTP_PORT.Text = "8081";
        }

        private void selectedAlgorithm_Enter(object sender, EventArgs e)
        {

        }

        private void isDES_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
