using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLSGAI
{
    public partial class mainForm : Form
    {
        private AI ki;

        public mainForm()
        {
            InitializeComponent();
            ki = new AI(this);
        }

        private void rtxt_chatlog_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_msg_TextChanged(object sender, EventArgs e)
        {

        }

        private void but_sendMsg_Click(object sender, EventArgs e)
        {
            this.passMessageToAI();
        }

        private void txt_msg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.passMessageToAI();
                //e.Handled = true;
            }
            else
            {
                
            }
        }

        private void passMessageToAI()
        {
            this.showMessage(txt_msg.Text, "User");
            ki.analyseMessage(txt_msg.Text);
            txt_msg.Text = "";
        }

        public void showMessage(String msg, String person)
        {
            if (rtxt_chatlog.Text == "")
            {
                rtxt_chatlog.Text += person + ": " + msg;
            }
            else
            {
                rtxt_chatlog.Text += "\n" + person + ": " + msg;
            }
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            shutdown();
        }

        public void shutdown()
        {
            Application.Exit();
        }
    }
}
