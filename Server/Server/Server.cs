using System;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
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
        public string msg1, msg2;//消息分段

        
        /*登陆及注册处理*/
        
        public struct UsrInfo
        {
            public string Account;
            public string Password;
            public int Connected;
            public Socket UsrSocket;
        }
        public UsrInfo[] FILE = new UsrInfo[100];//用户信息
        public int count = 0;

        public void register(string nickname, string Password, Socket tempSocket)//注册
        {
            FILE[count].Account = nickname;
            FILE[count].Password = Password;
            FILE[count].UsrSocket = tempSocket;
            FILE[count].Connected = 1;
            count++;
            WriteFile();
        }
        public bool login(string nickname, string Password, Socket tempSocket)//验证登陆
        {
            bool temp = false;
            for (int i = 0;i<count ; i++)
            {
                if (FILE[i].Account == null) break;
                else if (FILE[i].Account == nickname && FILE[i].Password == Password)
                {
                    temp = true;
                    FILE[i].UsrSocket = tempSocket;
                    FILE[i].Connected = 1;
                    break;
                }
            }
            return temp;
        }
        public void ReadFile()//读取本地服务器用户信息
        {
            string path = @"UsrInfo.txt"; 
            StreamReader sr = new StreamReader(path,Encoding.UTF8);
            String line;
            string[] temp = new string[2];
            while ((line = sr.ReadLine()) != null)
            {
                temp = line.ToString().Split('@');
                FILE[count].Account = temp[0];
                FILE[count].Password = temp[1];
                FILE[count].Connected = 0;
                count++;
                showClientMsg(temp[0]+" ");
                showClientMsg(temp[1]+"\n");
            }
            sr.Close();
        }
        public void WriteFile()//将用户信息写入文件
        {
            string path = @"UsrInfo.txt";
            StreamWriter fs = new StreamWriter(path);
            for(int i = 0; i < count; i++)
            {
                string UsrInfo = FILE[i].Account+"@"+FILE[i].Password;
                fs.WriteLine(UsrInfo);
            }
            fs.Flush();
            fs.Close();
        }
        public void setStatus(string temp_name, int temp)//设置在线或下线状态
        {
            for (int i = 0; i < count; i++)
            {
                if (FILE[i].Account == temp_name)
                {
                    FILE[i].Connected = temp;
                    break;
                }
            }
        }
        //用来设置服务端监听的端口号
        public bool m_Listening;
        public int setPort
        {
            get { return localPort; }
            set { localPort = value; }
        }
        
        public void showClientMsg(string msg)//显示客户端消息
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
        /*public void userListOperate(string msg)//后期可能会添加的客户端信息列表
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
        }*/
        

        /*监听客户端及接受消息*/
        
        public void Listen()//监听函数
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
        
        public void OnConnectRequest(IAsyncResult ar)//当有客户端连接时的处理
        {
            //初始化一个SOCKET，用于其它客户端的连接
            server1 = (Socket)ar.AsyncState;
            Client = server1.EndAccept(ar);
            remote = Client.RemoteEndPoint;
            server1.BeginAccept(new AsyncCallback(OnConnectRequest), server1); //等待新的客户端连接
            Socket tempSocket=Client;//将全局变量与线程分开，避免冲突

            //开始进行登陆与注册工作
            byte[] YES = System.Text.Encoding.UTF8.GetBytes("YES");
            byte[] NO = System.Text.Encoding.UTF8.GetBytes("NO");
            bool a=true;
            int flag = 1;//设置客户端标记，避免服务器崩溃
            while (a)
            {
                Spare(tempSocket);
                if (msg1 == "LOGIN")
                {
                    tip.Text = "1";
                    Spare(tempSocket);
                    if (login(msg1, msg2, tempSocket))
                    {
                        tempSocket.SendTo(YES, remote);
                        tip.Text = "2";
                        a = false;
                    }
                    else{
                        Client.SendTo(NO, remote);
                        tip.Text = "3";
                    }
                }
                else if (msg1 == "REGISTER")
                {
                    Spare(tempSocket);
                    register(msg1, msg2, tempSocket);
                    //WriteFile();
                    a = false;
                }
                else
                {
                    flag = 0;
                    break;
                }
            }

            //将要发送给连接上来的客户端的提示字符串
            DateTimeOffset now = DateTimeOffset.Now;
            string strDateLine = "管理员@"+msg1+"，欢迎登录到服务器!";
            Byte[] byteDateLine = System.Text.Encoding.UTF8.GetBytes(strDateLine);

            //将提示信息发送给客户端,并在服务端显示连接信息。
            if (flag == 1) {
                showClientMsg(tempSocket.RemoteEndPoint.ToString() + "连接成功。" + now.ToString("G") + "\r\n");
                tempSocket.Send(byteDateLine, byteDateLine.Length, 0);
                //userListOperate(Client.RemoteEndPoint.ToString());    
            }
           //接受消息
            while (true&&flag==1)
            {
                Spare(tempSocket);
                string ip = tempSocket.RemoteEndPoint.ToString();//获取客户端的IP和端口
                if (msg1 == "STOP")//当客户端终止连接时，消息格式：STOP@账户名
                {
                    showClientMsg(msg2 + now.ToString("G") + "  " + "已从服务器断开" + "\r\n");
                    setStatus(msg2, 0);
                    tempSocket.Close();
                    tip.Text = "6";
                    break;
                }
                showClientMsg(ip + "  " + now.ToString("G") + "   " + msg2 + "\r\n");//显示客户端发送过来的信息
                TransMsg(msg1,msg2);//转发消息
            }
            if (flag == 0)
            {
                tempSocket.Close();
            }          
        }
        public void Spare(Socket tempSocket)//分离账号及消息
        {
            byte[] data = new byte[1024];
            int recv = tempSocket.Receive(data);
            string stringdata = Encoding.UTF8.GetString(data, 0, recv);
            string[] temp = stringdata.Split('@');
                msg1 = temp[0];
                msg2 = temp[1];
        }
        
        public void SendBroadMsg()//发送广播消息
        {
            string strDataLine = sendmsg.Text;
            Byte[] sendData = Encoding.UTF8.GetBytes("管理员"+"@"+strDataLine);
            
            for(int i=0;i<count;i++)
            {
                int temp=FILE[i].Connected;
                Socket tempSocket=FILE[i].UsrSocket;
                if (temp==1)
                {
                    tempSocket.Send(sendData);
                }
            }
            sendmsg.Text = "";
        }
        public void TransMsg(string client_name,string msg)//转发客户端消息
        {
            for(int i=0;i<count;i++)
            {
                msg = msg1 + "@"+msg2;
                Byte[] sendData = Encoding.UTF8.GetBytes(msg);
                if (FILE[i].Account != client_name && FILE[i].Connected==1)
                {
                    try
                    {
                        FILE[i].UsrSocket.Send(sendData);
                    }
                    catch (SocketException e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
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

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void send_Click(object sender, EventArgs e)
        {
            SendBroadMsg();
        }
    }
}
