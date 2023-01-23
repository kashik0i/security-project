using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using algorithms.aes.AES;
using algorithms.aes.AESEncoder;
using algorithms.aes.des.DES;

namespace client
{
    public partial class ResiverForm : Form
    {
        public int _Port = 8080;
        public Resiver resiver;
        public RecieverHandler sender;

        public byte[,] _AESKey;
        public uint _DESKey;
        public bool[] _EcrptionAlogrthim;
        public AES _AES;
        public DES _DES;

        public ResiverForm()
        {
            InitializeComponent();
            _EcrptionAlogrthim = new bool[4];
        }
        public void AddMensagem(string text)
        {
            BeginInvoke(new Action(() =>
            {
                if (richTextBox1.Text.Length == 0)
                {
                    richTextBox1.AppendText(text);
                }
                else
                {
                    richTextBox1.AppendText("\n" + text);
                }
            }));
        }

        public void SetResiveingConnections()
        {
            resiver = new Resiver(this, this._Port);
            Thread thread = new Thread(new ThreadStart(resiver.Run));
            thread.Start();
            AddMensagem("wating for sender");
        }
        public void button2_Click(object sender, EventArgs e)
        {
            SendMensagem();
        }
        public void SendMensagem()
        {
            string mensagem = richTextBox2.Text;
            string planeMensagem = richTextBox2.Text;
            richTextBox2.Text = string.Empty;
            richTextBox2.Focus();
            if (_AESKey != null && _EcrptionAlogrthim[1] == true)
            {
                if (_AES == null)
                {
                    _AES = new AES(_AESKey);
                }
                var planeText = AESEncoder.EncodePlaneText(mensagem);
                var cypherText = _AES.Encrypt(planeText);
                mensagem = AESEncoder.DecodeCypherText(cypherText);
            }
            else if (_DESKey != 0 && _EcrptionAlogrthim[0] == true)
            {
                byte[] msgBytes1 = Encoding.Latin1.GetBytes(mensagem);
                var cifir1 = DES.EncryptString(msgBytes1, _DESKey, false);
                mensagem += Encoding.Latin1.GetString(cifir1);
            }
            resiver.Send(mensagem,planeMensagem.Trim());
        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            SetResiveingConnections();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = !radioButton2.Checked;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = !radioButton1.Checked;
        }

        private void GetKey(object sender, EventArgs e)
        {
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
            string msg = "";
            TcpClient client = new TcpClient(ip, port);
            NetworkStream ns = client.GetStream();
            // string -> bytes[]
            if (radioButton1.Checked)
            {
                msg = "DES";
            }
            else if (radioButton2.Checked)
            {
                msg = "AES";
            }
            byte[] sendBytes = Encoding.Latin1.GetBytes(msg);
            // Encrpt
            ns.Write(sendBytes, 0, sendBytes.Length);


            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = ns.Read(bytesToRead, 0, client.ReceiveBufferSize);
            MessageBox.Show("Received : " + Encoding.Latin1.GetString(bytesToRead, 0, bytesRead));
            MessageBox.Show("Received : " + bytesToRead);
            MessageBox.Show("Received : " + bytesRead);

            if (radioButton1.Checked)
            {
                msg = "DES";
                _DESKey = BitConverter.ToUInt32(bytesToRead);
                _EcrptionAlogrthim = new bool[4] { true, false, false, false };
                _AESKey = null;
            }
            else if (radioButton2.Checked)
            {
                _AESKey = new byte[4, 4];
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        _AESKey[j, i] = bytesToRead[4 * i + j];
                    }
                }
                _EcrptionAlogrthim = new bool[4] { false, true, false, false };
                _DESKey = 0;
            }
        }

        private void ResiverForm_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
