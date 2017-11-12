using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public partial class Form1 : Form
    {
        public Socket Newclient;
        public bool Connected;
        public Thread myThread;
        public delegate void MyInvoke(string str);
        public Form1()
        {
            InitializeComponent();
        }
        public void connect()
        {
            Newclient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string ip = "60.205.176.169";
            int port = 8081;
            IPEndPoint ie = new IPEndPoint(IPAddress.Parse(ip), port);
            try
            {
                Newclient.Connect(ie);
                Connected = true;
                Login.Enabled = false;
            }
            catch (SocketException e)
            {
                MessageBox.Show("connect failed" + e.Message);
                return;
            }
            ThreadStart myThread_delegate = new ThreadStart(ReceiveMsg);
            myThread = new Thread(myThread_delegate);
            myThread.Start();
        }
        public void ReceiveMsg()
        {
            while (true)
            {
                byte[] data = new byte[1024];
                int recv = Newclient.Receive(data);
                string stringdata = Encoding.UTF8.GetString(data, 0, recv);
                ShowMsg(stringdata + "\r\n");
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

        private void Sent_Message_Click(object sender, EventArgs e)
        {
            int m_length = Input_Msg.Text.Length;
            byte[] data = new byte[m_length];
            data = Encoding.UTF8.GetBytes(Input_Msg.Text);
            int i = Newclient.Send(data);
            ShowMsg(Input_Msg.Text + "\r\n");
            Input_Msg.Text = "";
        }

        private void Login_Click(object sender, EventArgs e)
        {
            connect();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}