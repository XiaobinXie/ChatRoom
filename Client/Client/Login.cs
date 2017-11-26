using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public class UsrInfo
        {//设置全局变量，方便多窗口
            public static string Account;
            public static string Password;
            public static Socket NewClient;
        }
     
        public void Connect()//在程序启动后，开始连接
        {
            UsrInfo.NewClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string ip = "60.205.176.169";
            int port = 9001;
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                UsrInfo.NewClient.Connect(ie);
                tip.Text = "连接成功";
            }
            catch (SocketException e)
            {
                MessageBox.Show("连接失败" + e.Message);
                return;
            }
        }
        public void Check_Account()//发送账号信息，验证账号
        {
            byte[] data = new byte[100];
            data = Encoding.UTF8.GetBytes("LOGIN|LOGIN");
            int i = UsrInfo.NewClient.Send(data);
            data = Encoding.UTF8.GetBytes(Account.Text + "|" + Password.Text);
            i = UsrInfo.NewClient.Send(data);

            byte[] temp = new byte[100];
            int recv = UsrInfo.NewClient.Receive(temp);
            string stringdata = Encoding.UTF8.GetString(temp, 0, recv);
            if (stringdata == "YES")
            {
                this.Hide();//隐藏登陆界面
                UsrInfo.Account = Account.Text;
                ChatForm ChatForm = new ChatForm();
                ChatForm.Show();
            }
            else
            {
                tip.Text = "账号或密码错误";
                Account.Text = "";
                Password.Text = "";
            }
        }
        public void Register()//发送注册信息
        {
            byte[] data = new byte[100];
            data = Encoding.UTF8.GetBytes("REGISTER|REGISTER");
            int i = UsrInfo.NewClient.Send(data);
            data = Encoding.UTF8.GetBytes(Account.Text + "|" + Password.Text);
            i = UsrInfo.NewClient.Send(data);

            byte[] temp = new byte[100];
            int recv = UsrInfo.NewClient.Receive(temp);
            string stringdata = Encoding.UTF8.GetString(temp, 0, recv);
            if (stringdata == "YES")
            {
                this.Hide();//隐藏注册界面
                UsrInfo.Account = Account.Text;
                ChatForm ChatForm = new ChatForm();
                ChatForm.Show();
            }
            else
            {
                tip.Text = "该账号已被注册，请再次尝试";
                Account.Text = "";
                Password.Text = "";
            }
        }
        private void Login_Bt_Click(object sender, EventArgs e)
        {
            Check_Account();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            Register_R.Hide();
            Connect();
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {
            byte[] data = new byte[4];
            data = System.Text.Encoding.Default.GetBytes("STOP|STOP");
            int i = UsrInfo.NewClient.Send(data);
            UsrInfo.NewClient.Close();
        }

        private void Register_Link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register_Link.Text = "";
            Login_Bt.Hide();
            Register_R.Show();
        }

        private void Register_R_Click(object sender, EventArgs e)
        {
            Register();
        }
    }
}
