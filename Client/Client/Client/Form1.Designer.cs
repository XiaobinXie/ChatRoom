namespace Client
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (Connected)
            {
                byte[] data = new byte[4];
                data = System.Text.Encoding.Default.GetBytes("STOP");
                int i = Newclient.Send(data);
                Newclient.Close();
            }
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Msg_Box = new System.Windows.Forms.TextBox();
            this.Input_Msg = new System.Windows.Forms.TextBox();
            this.Login = new System.Windows.Forms.Button();
            this.Sent_Message = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Msg_Box
            // 
            this.Msg_Box.Location = new System.Drawing.Point(32, 28);
            this.Msg_Box.Multiline = true;
            this.Msg_Box.Name = "Msg_Box";
            this.Msg_Box.Size = new System.Drawing.Size(445, 368);
            this.Msg_Box.TabIndex = 0;
            // 
            // Input_Msg
            // 
            this.Input_Msg.Location = new System.Drawing.Point(32, 421);
            this.Input_Msg.Name = "Input_Msg";
            this.Input_Msg.Size = new System.Drawing.Size(445, 21);
            this.Input_Msg.TabIndex = 1;
            // 
            // Login
            // 
            this.Login.Location = new System.Drawing.Point(483, 28);
            this.Login.Name = "Login";
            this.Login.Size = new System.Drawing.Size(75, 23);
            this.Login.TabIndex = 2;
            this.Login.Text = "登陆";
            this.Login.UseVisualStyleBackColor = true;
            this.Login.Click += new System.EventHandler(this.Login_Click);
            // 
            // Sent_Message
            // 
            this.Sent_Message.Location = new System.Drawing.Point(483, 421);
            this.Sent_Message.Name = "Sent_Message";
            this.Sent_Message.Size = new System.Drawing.Size(75, 23);
            this.Sent_Message.TabIndex = 3;
            this.Sent_Message.Text = "发送";
            this.Sent_Message.UseVisualStyleBackColor = true;
            this.Sent_Message.Click += new System.EventHandler(this.Sent_Message_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 476);
            this.Controls.Add(this.Sent_Message);
            this.Controls.Add(this.Login);
            this.Controls.Add(this.Input_Msg);
            this.Controls.Add(this.Msg_Box);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Msg_Box;
        private System.Windows.Forms.TextBox Input_Msg;
        private System.Windows.Forms.Button Login;
        private System.Windows.Forms.Button Sent_Message;
    }
}

