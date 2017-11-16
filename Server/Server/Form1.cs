using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace TCPServer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public bool btnstatu = true;  //开始停止服务按钮状态
        public Thread myThread;       //声明一个线程实例
        public Socket newsock;        //声明一个Socket实例
        public Socket server1;
        public Socket Client;
        public delegate void MyInvoke(string str);
        public IPEndPoint localEP;
        public int localPort;
        public EndPoint remote;
        public string ClientName;
        public string msg1, msg2;

        //登陆及注册处理
        public struct UsrInfo
        {
            public string NickName;
            public string password;
            public Socket Usr_Socket;
            public EndPoint clientip;
            public bool connected;
        }
        public UsrInfo[] FILE = new UsrInfo[100];
        public int count = 0;
        public string client_name;

        public void register(string nickname, string password, EndPoint clientip)//注册
        {
            FILE[count].NickName = nickname;
            FILE[count].password = password;
            FILE[count].clientip = clientip;
            FILE[count].Usr_Socket = Client;
            count++;
        }
        public bool login(string nickname, string password, EndPoint clientip)//登陆
        {
            bool temp = false;
            for (int i = 0;i<count ; i++)
            {
                if (FILE[i].NickName == null) break;
                else if (FILE[i].NickName == nickname && FILE[i].password == password)
                {
                    temp = true;
                    FILE[i].clientip = clientip;
                    FILE[i].connected = true;
                    FILE[i].Usr_Socket = Client;
                    break;
                }
            }
            return temp;
        }
        public void ReadFile()
        {
            string path = "C:\\Users\\Administrator\\Desktop\\UsrInfo.txt"; 
            StreamReader sr = new StreamReader(path,Encoding.UTF8);
            String line;
            string[] temp = new string[2];
            while ((line = sr.ReadLine()) != null)
            {
                temp = line.ToString().Split('@');
                FILE[count].NickName = temp[0];
                FILE[count].password = temp[1];
                FILE[count].connected = false;
                count++;
                //showClientMsg(temp[0]);
                //showClientMsg(temp[1]);
            }
            sr.Close();
        }
        public void WriteFile()
        {
            string path = "C:\\Users\\Administrator\\Desktop\\UsrInfo.txt";
            StreamWriter fs = new StreamWriter(path);
            //获得字节数组
            for(int i = 0; i < count; i++)
            {
                string UsrInfo = FILE[i].NickName+"@"+FILE[i].password;
                fs.WriteLine(UsrInfo);
            }
                       
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        public bool m_Listening;
        //用来设置服务端监听的端口号
        public int setPort
        {
            get { return localPort; }
            set { localPort = value; }
        }
        //用来往richtextbox框中显示消息
        public void showClientMsg(string msg)
        {
            //在线程里以安全方式调用控件
            if (showinfo.InvokeRequired)
            {
                MyInvoke _myinvoke = new MyInvoke(showClientMsg);
                showinfo.Invoke(_myinvoke, new object[] { msg });
            }
            else
            {
                showinfo.AppendText(msg);
            }
        }
        public void userListOperate(string msg)
        {
            //在线程里以安全方式调用控件
            if (userList.InvokeRequired)
            {
                MyInvoke _myinvoke = new MyInvoke(userListOperate);
                userList.Invoke(_myinvoke, new object[] { msg });
            }
            else
            {
                userList.Items.Add(msg);
            }
        }
        //监听函数
        public void Listen()
        {   //设置端口
            setPort = int.Parse(serverport.Text.Trim());
            //初始化SOCKET实例
            newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //允许SOCKET被绑定在已使用的地址上。
            newsock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //初始化终结点实例
            localEP = new IPEndPoint(IPAddress.Any, setPort);
            try
            {
                //绑定
                newsock.Bind(localEP);
                //监听
                newsock.Listen(10);
                //开始接受连接，异步。
                newsock.BeginAccept(new AsyncCallback(OnConnectRequest), newsock);
            }
            catch (Exception ex)
            {
                showClientMsg(ex.Message);
            }
        }
        //当有客户端连接时的处理
        public void OnConnectRequest(IAsyncResult ar)
        {
            server1 = (Socket)ar.AsyncState;
            Socket Client = server1.EndAccept(ar);
            remote = Client.RemoteEndPoint;
            server1.BeginAccept(new AsyncCallback(OnConnectRequest), server1); //等待新的客户端连接

            byte[] YES = System.Text.Encoding.UTF8.GetBytes("YES");
            //byte[] NO_bool = System.Text.Encoding.UTF8.GetBytes("BUBING");
            //开始进行登陆与注册工作
            bool a=true;
           while (a)
            {
                Spare();
                if (msg1 == "LOGIN")
                {
                    tip.Text = "1";
                    Spare();
                    if (login(msg1, msg2, remote))
                    {
                        Client.SendTo(YES, remote);
                        tip.Text = "2";
                        setStatus(msg1, true);
                        a = false;
                    }
                    else{
                        //temp_Client.SendTo(NO_bool, remote);
                        tip.Text = "3";
                    }
                }
                else if (msg1 == "REGISTER")
                {
                    Spare();
                    register(msg1, msg2, remote);
                    setStatus(msg1, true);
                    a = false;
                }
            }
            Thread.Sleep(300);
            //将要发送给连接上来的客户端的提示字符串
            DateTimeOffset now = DateTimeOffset.Now;
            string strDateLine = "管理员@"+msg1+"，欢迎登录到服务器!";
            Byte[] byteDateLine = System.Text.Encoding.UTF8.GetBytes(strDateLine);
            //将提示信息发送给客户端,并在服务端显示连接信息。
            showClientMsg(Client.RemoteEndPoint.ToString() + "连接成功。" + now.ToString("G") + "\r\n");
            Client.Send(byteDateLine, byteDateLine.Length, 0);
            //userListOperate(Client.RemoteEndPoint.ToString());    
             
           
            while (true)
            {
                Spare();
                string ip = Client.RemoteEndPoint.ToString();
                //获取客户端的IP和端口

                if (msg2 == "STOP")
                {
                    //当客户端终止连接时
                    showClientMsg(msg1 + now.ToString("G") + "  " + "已从服务器断开" + "\r\n");
                    setStatus(msg1,false);
                    break;
                }
                //显示客户端发送过来的信息
                showClientMsg(ip + "  " + now.ToString("G") + "   " + msg2 + "\r\n");
                TransMsg(msg1,msg2);
            }

        }
        public void setStatus(string name,bool set_bool)
        {
            for(int i = 0; i < count; i++)
            {
                if (FILE[i].NickName == name)
                    FILE[i].connected = set_bool;
            }
        }
        public void Spare()
        {
            byte[] data = new byte[1024];
            int recv = Client.Receive(data);
            string stringdata = Encoding.UTF8.GetString(data, 0, recv);
            string[] temp = stringdata.Split('@');
                msg1 = temp[0];
                msg2 = temp[1];
        }
        //以下实现发送广播消息
        public void SendBroadMsg()
        {
            Socket New;
            string strDataLine = sendmsg.Text;
            Byte[] sendData = Encoding.UTF8.GetBytes("服务器"+"@"+strDataLine);
            for(int i=0;i<count;i++)
            {
                EndPoint temp = FILE[i].clientip;
                New = FILE[i].Usr_Socket;
                New.SendTo(sendData, FILE[i].clientip);
            }
            sendmsg.Text = "";
        }
        public void TransMsg(string client_name,string msg)//转发客户端消息
        {
            Socket New;
            for(int i=0;i<count;i++)
            {
                msg = FILE[i].NickName+"@"+msg;
                Byte[] sendData = Encoding.UTF8.GetBytes(msg);
                if (FILE[i].NickName != client_name&&FILE[i].connected==true)
                {
                    New = FILE[i].Usr_Socket;
                    New.SendTo(sendData, FILE[i].clientip);
                }
            }
        }
        //开始停止服务按钮
        private void startService_Click(object sender, EventArgs e)
        {
            //新建一个委托线程
            ThreadStart myThreadDelegate = new ThreadStart(Listen);
            //实例化新线程
            myThread = new Thread(myThreadDelegate);

            if (btnstatu)
            {
                myThread.Start();
                statuBar.Text = "服务已启动，等待客户端连接";
                count = 0;
                ReadFile();
                btnstatu = false;
                startService.Text = "停止服务";
            }
            else
            {
                //停止服务（绑定的套接字没有关闭,因此客户端还是可以连接上来）
                myThread.Interrupt();
                myThread.Abort();
                WriteFile();
                //showClientMsg("服务器已停止服务"+"\r\n");
                btnstatu = true;
                startService.Text = "开始服务";
                statuBar.Text = "服务已停止";

            }
        }
        //窗口关闭时中止线程。
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (myThread != null)
            {
                WriteFile();
                myThread.Abort();
            }
        }
        private void send_Click(object sender, EventArgs e)
        {
            SendBroadMsg();
        }
    }
}
