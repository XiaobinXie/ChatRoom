using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static Client.Login;

namespace Client
{
    public partial class ChatForm : Form
    {
        public string msg1,msg2;
        public Thread myThread;
        public Socket Chat_Client = UsrInfo.NewClient;
        public string account = UsrInfo.Account;
        public delegate void MyInvoke(string str);
        public bool Connected = true;
        public ChatForm()
        {
            InitializeComponent();
        }
        public void connect()
        {
            ThreadStart myThread_delegate = new ThreadStart(ReceiveMsg);
            myThread = new Thread(myThread_delegate);
            myThread.Start();
        }
        public void ReceiveMsg()
        {
            while (true)
            {
                byte[] data = new byte[100];
                int recv = Chat_Client.Receive(data);
                string stringdata = Encoding.UTF8.GetString(data, 0, recv);
                SpareMsg(stringdata);
                ShowMsg(msg1 +":"+msg2+ "\r\n");
            }
        }
        public void ShowMsg(string msg)
        {
            {
                if (Msg_Box.InvokeRequired)
                {
                    MyInvoke _myinvoke = new MyInvoke(ShowMsg);
                    Msg_Box.Invoke(_myinvoke, new object[] { msg });
                }
                else
                {
                    Msg_Box.AppendText(msg);
                }
            }
        }
        public void SpareMsg(string receive_msg)//分离账号和消息
        {
            string[] temp =new string[2];
            temp = receive_msg.Split('@');
            msg1 = temp[0];
            msg2 = temp[1];
        }
        private void Send_Click(object sender, EventArgs e)//发送触发的事件
        {
            int m_length = MsgToSend.Text.Length;
            byte[] data = new byte[m_length];
            data = Encoding.UTF8.GetBytes(account+"@"+MsgToSend.Text);
            int i = Chat_Client.Send(data);
            ShowMsg(account + "：" + MsgToSend.Text + "\r\n");
            MsgToSend.Text = "";
        }

        private void ChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            byte[] data = new byte[4];
            data = System.Text.Encoding.Default.GetBytes("STOP@STOP");
            int i = Chat_Client.Send(data);
            Chat_Client.Close();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            connect();
        }
    }
}