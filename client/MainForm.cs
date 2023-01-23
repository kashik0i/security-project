using System;
using System.Windows.Forms;

namespace client
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton1.Checked = !radioButton2.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = !radioButton1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                this.Hide();
                ResiverForm resiverForm = new ResiverForm();
                resiverForm.Closed += (s, args) => this.Close();
                resiverForm.Show();
            }
            else if (radioButton2.Checked)
            {
                this.Hide();
                SenderForm senderForm = new SenderForm();
                senderForm.Closed += (s, args) => this.Close();
                senderForm.Show();
            }
        }
    }
}