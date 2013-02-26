namespace SLSGAI
{
    partial class mainForm
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
            this.rtxt_chatlog = new System.Windows.Forms.RichTextBox();
            this.txt_msg = new System.Windows.Forms.TextBox();
            this.but_sendMsg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtxt_chatlog
            // 
            this.rtxt_chatlog.Location = new System.Drawing.Point(12, 12);
            this.rtxt_chatlog.Name = "rtxt_chatlog";
            this.rtxt_chatlog.ReadOnly = true;
            this.rtxt_chatlog.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtxt_chatlog.Size = new System.Drawing.Size(260, 344);
            this.rtxt_chatlog.TabIndex = 10;
            this.rtxt_chatlog.Text = "";
            this.rtxt_chatlog.TextChanged += new System.EventHandler(this.rtxt_chatlog_TextChanged);
            // 
            // txt_msg
            // 
            this.txt_msg.Location = new System.Drawing.Point(12, 362);
            this.txt_msg.Multiline = true;
            this.txt_msg.Name = "txt_msg";
            this.txt_msg.Size = new System.Drawing.Size(175, 20);
            this.txt_msg.TabIndex = 0;
            this.txt_msg.TextChanged += new System.EventHandler(this.txt_msg_TextChanged);
            this.txt_msg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_msg_KeyDown);
            // 
            // but_sendMsg
            // 
            this.but_sendMsg.Location = new System.Drawing.Point(197, 360);
            this.but_sendMsg.Name = "but_sendMsg";
            this.but_sendMsg.Size = new System.Drawing.Size(75, 23);
            this.but_sendMsg.TabIndex = 1;
            this.but_sendMsg.Text = "Send MSG";
            this.but_sendMsg.UseVisualStyleBackColor = true;
            this.but_sendMsg.Click += new System.EventHandler(this.but_sendMsg_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 395);
            this.Controls.Add(this.but_sendMsg);
            this.Controls.Add(this.txt_msg);
            this.Controls.Add(this.rtxt_chatlog);
            this.Name = "mainForm";
            this.Text = "SLSGAI";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxt_chatlog;
        private System.Windows.Forms.TextBox txt_msg;
        private System.Windows.Forms.Button but_sendMsg;
    }
}

