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

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public class UsrInfo{
            public static string Account;
            public static string Password;
            public static Socket NewClient;
            
        }
        public bool Connected=false;
        public Thread myThread;
        public Socket Newclient;
        public void Connect()
        {
            UsrInfo.NewClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string ip = "60.205.176.169";
            int port = 8081;
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                UsrInfo.NewClient.Connect(ie);
            }
            catch (SocketException e)
            {
                MessageBox.Show("connect failed" + e.Message);
                return;
            }
            tip.Text = "连接成功";
        }
        public void Check_Account()
        {
            byte[] data = new byte[100];
            data = Encoding.UTF8.GetBytes("LOGIN@123");
            int i = UsrInfo.NewClient.Send(data);
            data = Encoding.UTF8.GetBytes(Account.Text + "@" + Password.Text);
            i = UsrInfo.NewClient.Send(data);
            int recv = UsrInfo.NewClient.Receive(data);
            string stringdata = Encoding.UTF8.GetString(data, 0, recv);
            if (stringdata == "YES")
            {
                this.Hide();
                UsrInfo.Account = Account.Text;
                ChatForm ChatForm = new ChatForm();
                ChatForm.Show();
            }
            else if (stringdata == "NO")
            {
                tip.Text = "账号或密码错误";
            }
        }
        public void Register()
        {
            Register_Link.Text = "";
            Login_Bt.Text = "注册";
            byte[] data = new byte[100];
            data = Encoding.UTF8.GetBytes("REGISTER@123");
            int i = UsrInfo.NewClient.Send(data);
            data = Encoding.UTF8.GetBytes(Account.Text + "@" + Password.Text);
            i = UsrInfo.NewClient.Send(data);
            this.Hide();
            UsrInfo.Account = Account.Text;
            ChatForm ChatForm = new ChatForm();
            ChatForm.Show();
        }
        private void Login_Bt_Click(object sender, EventArgs e)
        {
            Check_Account();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Connect();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            UsrInfo.NewClient.Close();
        }

        private void Register_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Register();
        }
    }
}
