namespace Client
{
    partial class Login
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
            this.Login_Bt = new System.Windows.Forms.Button();
            this.Account = new System.Windows.Forms.TextBox();
            this.Password = new System.Windows.Forms.TextBox();
            this.Register_Link = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tip = new System.Windows.Forms.Label();
            this.Register_R = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Login_Bt
            // 
            this.Login_Bt.Location = new System.Drawing.Point(62, 132);
            this.Login_Bt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Login_Bt.Name = "Login_Bt";
            this.Login_Bt.Size = new System.Drawing.Size(151, 33);
            this.Login_Bt.TabIndex = 0;
            this.Login_Bt.Text = "登陆";
            this.Login_Bt.UseVisualStyleBackColor = true;
            this.Login_Bt.Click += new System.EventHandler(this.Login_Bt_Click);
            // 
            // Account
            // 
            this.Account.Location = new System.Drawing.Point(97, 59);
            this.Account.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Account.Name = "Account";
            this.Account.Size = new System.Drawing.Size(116, 23);
            this.Account.TabIndex = 1;
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(97, 90);
            this.Password.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(116, 23);
            this.Password.TabIndex = 2;
            // 
            // Register_Link
            // 
            this.Register_Link.AutoSize = true;
            this.Register_Link.Location = new System.Drawing.Point(83, 216);
            this.Register_Link.Name = "Register_Link";
            this.Register_Link.Size = new System.Drawing.Size(104, 17);
            this.Register_Link.TabIndex = 3;
            this.Register_Link.TabStop = true;
            this.Register_Link.Text = "没有账号？请注册";
            this.Register_Link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Register_Link_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "账号";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "密码";
            // 
            // tip
            // 
            this.tip.AutoSize = true;
            this.tip.Font = new System.Drawing.Font("微软雅黑", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tip.Location = new System.Drawing.Point(3, 235);
            this.tip.Name = "tip";
            this.tip.Size = new System.Drawing.Size(0, 16);
            this.tip.TabIndex = 6;
            // 
            // Register_R
            // 
            this.Register_R.Location = new System.Drawing.Point(62, 158);
            this.Register_R.Name = "Register_R";
            this.Register_R.Size = new System.Drawing.Size(151, 33);
            this.Register_R.TabIndex = 7;
            this.Register_R.Text = "注册";
            this.Register_R.UseVisualStyleBackColor = true;
            this.Register_R.Click += new System.EventHandler(this.Register_R_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(269, 260);
            this.Controls.Add(this.Register_R);
            this.Controls.Add(this.tip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Register_Link);
            this.Controls.Add(this.Password);
            this.Controls.Add(this.Account);
            this.Controls.Add(this.Login_Bt);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatRoom";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Login_FormClosed);
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Login_Bt;
        private System.Windows.Forms.TextBox Account;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.LinkLabel Register_Link;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label tip;
        private System.Windows.Forms.Button Register_R;
    }
}

