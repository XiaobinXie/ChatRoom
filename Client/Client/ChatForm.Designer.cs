namespace Client
{
    partial class ChatForm
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
            this.Msg_Box = new System.Windows.Forms.TextBox();
            this.MsgToSend = new System.Windows.Forms.TextBox();
            this.Send = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Msg_Box
            // 
            this.Msg_Box.Location = new System.Drawing.Point(14, 17);
            this.Msg_Box.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Msg_Box.Multiline = true;
            this.Msg_Box.Name = "Msg_Box";
            this.Msg_Box.Size = new System.Drawing.Size(495, 385);
            this.Msg_Box.TabIndex = 0;
            // 
            // MsgToSend
            // 
            this.MsgToSend.Location = new System.Drawing.Point(12, 415);
            this.MsgToSend.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MsgToSend.Name = "MsgToSend";
            this.MsgToSend.Size = new System.Drawing.Size(397, 23);
            this.MsgToSend.TabIndex = 1;
            // 
            // Send
            // 
            this.Send.Location = new System.Drawing.Point(422, 410);
            this.Send.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Send.Name = "Send";
            this.Send.Size = new System.Drawing.Size(87, 33);
            this.Send.TabIndex = 2;
            this.Send.Text = "发送";
            this.Send.UseVisualStyleBackColor = true;
            this.Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // ChatForm
            // 
            this.AcceptButton = this.Send;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 451);
            this.Controls.Add(this.Send);
            this.Controls.Add(this.MsgToSend);
            this.Controls.Add(this.Msg_Box);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "ChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatRoom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ChatForm_FormClosed);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Msg_Box;
        private System.Windows.Forms.TextBox MsgToSend;
        private System.Windows.Forms.Button Send;
    }
}