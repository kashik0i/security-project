using System.IO;
using System.Windows.Forms;

namespace client
{
    partial class SenderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.connectBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Reciever_IP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TTP_IP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TTP_PORT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.Sender_Name = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.autofill_btn = new System.Windows.Forms.Button();
            this.isAES = new System.Windows.Forms.RadioButton();
            this.isDES = new System.Windows.Forms.RadioButton();
            this.algorthmsGroup = new System.Windows.Forms.GroupBox();
            this.isRSA = new System.Windows.Forms.RadioButton();
            this.isElGamal = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.algorthmsGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(694, 6);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(94, 29);
            this.connectBtn.TabIndex = 0;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(235, 409);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 29);
            this.button2.TabIndex = 1;
            this.button2.Text = "Enter";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.richTextBox1.Location = new System.Drawing.Point(119, 51);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(508, 246);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(119, 303);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(508, 100);
            this.richTextBox2.TabIndex = 5;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(134, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP :";
            // 
            // Reciever_IP
            // 
            this.Reciever_IP.Location = new System.Drawing.Point(168, 5);
            this.Reciever_IP.Name = "Reciever_IP";
            this.Reciever_IP.Size = new System.Drawing.Size(164, 27);
            this.Reciever_IP.TabIndex = 7;
            this.Reciever_IP.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(794, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 20);
            this.label2.TabIndex = 8;
            this.label2.Text = "TTP IP :";
            // 
            // TTP_IP
            // 
            this.TTP_IP.Location = new System.Drawing.Point(870, 12);
            this.TTP_IP.Name = "TTP_IP";
            this.TTP_IP.Size = new System.Drawing.Size(125, 27);
            this.TTP_IP.TabIndex = 9;
            this.TTP_IP.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(794, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "TTP Port :";
            // 
            // TTP_PORT
            // 
            this.TTP_PORT.Location = new System.Drawing.Point(870, 47);
            this.TTP_PORT.Name = "TTP_PORT";
            this.TTP_PORT.Size = new System.Drawing.Size(125, 27);
            this.TTP_PORT.TabIndex = 11;
            this.TTP_PORT.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(335, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 14;
            this.label4.Text = "Name :";
            // 
            // Sender_Name
            // 
            this.Sender_Name.Location = new System.Drawing.Point(397, 6);
            this.Sender_Name.Name = "Sender_Name";
            this.Sender_Name.Size = new System.Drawing.Size(145, 27);
            this.Sender_Name.TabIndex = 15;
            this.Sender_Name.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(924, 173);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(71, 29);
            this.button3.TabIndex = 16;
            this.button3.Text = "Get Key";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.GetKey);
            // 
            // autofill_btn
            // 
            this.autofill_btn.Location = new System.Drawing.Point(901, 208);
            this.autofill_btn.Name = "autofill_btn";
            this.autofill_btn.Size = new System.Drawing.Size(94, 29);
            this.autofill_btn.TabIndex = 21;
            this.autofill_btn.Text = "Autofill";
            this.autofill_btn.UseVisualStyleBackColor = true;
            this.autofill_btn.Click += new System.EventHandler(this.autofill_btn_Click);
            // 
            // isAES
            // 
            this.isAES.AutoSize = true;
            this.isAES.Location = new System.Drawing.Point(7, 56);
            this.isAES.Name = "isAES";
            this.isAES.Size = new System.Drawing.Size(56, 24);
            this.isAES.TabIndex = 23;
            this.isAES.Text = "AES";
            this.isAES.UseVisualStyleBackColor = true;
            // 
            // isDES
            // 
            this.isDES.AutoSize = true;
            this.isDES.Checked = true;
            this.isDES.Location = new System.Drawing.Point(6, 26);
            this.isDES.Name = "isDES";
            this.isDES.Size = new System.Drawing.Size(57, 24);
            this.isDES.TabIndex = 22;
            this.isDES.TabStop = true;
            this.isDES.Text = "DES";
            this.isDES.UseVisualStyleBackColor = true;
            this.isDES.CheckedChanged += new System.EventHandler(this.isDES_CheckedChanged);
            // 
            // algorthmsGroup
            // 
            this.algorthmsGroup.Controls.Add(this.isRSA);
            this.algorthmsGroup.Controls.Add(this.isElGamal);
            this.algorthmsGroup.Controls.Add(this.isDES);
            this.algorthmsGroup.Controls.Add(this.isAES);
            this.algorthmsGroup.Location = new System.Drawing.Point(12, 15);
            this.algorthmsGroup.Name = "algorthmsGroup";
            this.algorthmsGroup.Size = new System.Drawing.Size(101, 184);
            this.algorthmsGroup.TabIndex = 24;
            this.algorthmsGroup.TabStop = false;
            this.algorthmsGroup.Text = "Algorithms";
            this.algorthmsGroup.Enter += new System.EventHandler(this.selectedAlgorithm_Enter);
            // 
            // isRSA
            // 
            this.isRSA.AutoSize = true;
            this.isRSA.Location = new System.Drawing.Point(7, 116);
            this.isRSA.Name = "isRSA";
            this.isRSA.Size = new System.Drawing.Size(57, 24);
            this.isRSA.TabIndex = 25;
            this.isRSA.Text = "RSA";
            this.isRSA.UseVisualStyleBackColor = true;
            // 
            // isElGamal
            // 
            this.isElGamal.AutoSize = true;
            this.isElGamal.Location = new System.Drawing.Point(7, 86);
            this.isElGamal.Name = "isElGamal";
            this.isElGamal.Size = new System.Drawing.Size(85, 24);
            this.isElGamal.TabIndex = 24;
            this.isElGamal.Text = "ElGamal";
            this.isElGamal.UseVisualStyleBackColor = true;
            // 
            // SenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 462);
            this.Controls.Add(this.algorthmsGroup);
            this.Controls.Add(this.autofill_btn);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.Sender_Name);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.TTP_PORT);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TTP_IP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Reciever_IP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.connectBtn);
            this.Name = "SenderForm";
            this.Text = "ClientForm";
            this.Load += new System.EventHandler(this.SenderForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.algorthmsGroup.ResumeLayout(false);
            this.algorthmsGroup.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Button connectBtn;
        private Button button2;
        private FileSystemWatcher fileSystemWatcher1;
        private RichTextBox richTextBox2;
        private RichTextBox richTextBox1;
        private Label label1;
        private TextBox Reciever_IP;
        private TextBox TTP_PORT;
        private Label label3;
        private TextBox TTP_IP;
        private Label label2;
        private TextBox Sender_Name;
        private Label label4;
        private Button button3;
        private Button autofill_btn;
        private RadioButton isAES;
        private RadioButton isDES;
        private GroupBox algorthmsGroup;
        private RadioButton isRSA;
        private RadioButton isElGamal;
    }
}