using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SocketTest
{
    // 客户端离线委托
    public delegate void ClientOfflineHandler(ClientInfo client);

    // 客户端上线委托
    public delegate void ClientOnlineHandler(ClientInfo client);

    class Program
    {
        /// <summary>
        /// 客户端离线提示
        /// </summary>
        /// <param name="clientInfo"></param>
        private static void ClientOffline(ClientInfo clientInfo)
        {
            Console.WriteLine(string.Format("客户端{0}离线，离线时间：\t{1}", clientInfo.ClientID, clientInfo.LastHeartbeatTime));
        }

        /// <summary>
        /// 客户端上线提示
        /// </summary>
        /// <param name="clientInfo"></param>
        private static void ClientOnline(ClientInfo clientInfo)
        {
            Console.WriteLine(string.Format("客户端{0}上线，上线时间：\t{1}", clientInfo.ClientID, clientInfo.LastHeartbeatTime));
        }



        static void Main(string[] args)
        {
            // 服务端
            Server server = new Server();

            // 服务端离线事件
            server.OnClientOffline += ClientOffline;

            // 服务端上线事件
            server.OnClientOnline += ClientOnline;

            // 开启服务器
            server.Start();

            // 模拟100个客户端
            Dictionary<Int32, Client> dicClient = new Dictionary<int, Client>();
            for (int i = 0; i < 100; i++)
            {
                // 这里传入server只是为了方便而已
                Client client = new Client(i + 1, server);
                dicClient.Add(i + 1, client);

                // 开启客户端
                client.Start();
            }

            Thread.Sleep(1000);

            while (true)
            {
                Console.WriteLine("请输入要离线的ClientID，输入0则退出程序");
                String ClientID = Console.ReadLine();
                if (!string.IsNullOrEmpty(ClientID))
                {
                    Int32 iClientID = 0;
                    Int32.TryParse(ClientID, out iClientID);
                    if (iClientID > 0)
                    {
                        Client client;
                        if (dicClient.TryGetValue(iClientID, out client))
                        {
                            // 客户端离线
                            client.Offline = true;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 服务器
    /// </summary>
    public class Server
    {
        public event ClientOfflineHandler OnClientOffline;
        public event ClientOnlineHandler OnClientOnline;

        private Dictionary<Int32, ClientInfo> _DicClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Server()
        {
            _DicClient = new Dictionary<int, ClientInfo>();
        }

        public void Start()
        {
            // 开启扫描离线线程
            Thread t = new Thread(ScanOffline);
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// 扫描离线
        /// </summary>
        private void ScanOffline()
        {
            while (true)
            {
                // 1秒扫描一次
                Thread.Sleep(1000);

                lock (_DicClient)
                {
                    foreach (Int32 clientID in _DicClient.Keys)
                    {
                        ClientInfo clientInfo = _DicClient[clientID];

                        // 如果已经离线则不用管
                        if (!clientInfo.State)
                        {
                            continue;
                        }

                        TimeSpan sp = DateTime.Now - clientInfo.LastHeartbeatTime;
                        if (sp.Seconds >= 3)
                        {
                            // 离线，触发离线事件
                            if (OnClientOffline != null)
                            {
                                OnClientOffline(clientInfo);
                            }

                            // 修改状态
                            clientInfo.State = false;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 接收心跳包
        /// </summary>
        /// <param name="clientID">客户端ID</param>
        public void ReceiveHeartbeat(Int32 clientID)
        {
            lock (_DicClient)
            {
                ClientInfo clientInfo;
                if (_DicClient.TryGetValue(clientID, out clientInfo))
                {
                    // 如果客户端已上线，则更新最后心跳时间
                    clientInfo.LastHeartbeatTime = DateTime.Now;
                }
                else
                {
                    // 客户端不存在，则认为是新上线
                    clientInfo = new ClientInfo();
                    clientInfo.ClientID = clientID;
                    clientInfo.LastHeartbeatTime = DateTime.Now;
                    clientInfo.State = true;

                    _DicClient.Add(clientID,clientInfo);

                    // 触发上线事件
                    if (OnClientOnline != null)
                    {
                        OnClientOnline(clientInfo);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 客户端
    /// </summary>
    public class Client
    {
        public Server Server;
        public Int32 ClientID;
        public Boolean Offline;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="clientID"></param>
        /// <param name="server"></param>
        public Client(Int32 clientID, Server server)
        {
            ClientID = clientID;
            Server = server;
            Offline = false;
        }

        /// <summary>
        /// 开启客户端
        /// </summary>
        public void Start()
        {
            // 开启心跳线程
            Thread t = new Thread(Heartbeat);
            t.IsBackground = true;
            t.Start();
        }

        /// <summary>
        /// 向服务器发送心跳包
        /// </summary>
        private void Heartbeat()
        {
            while (!Offline)
            {
                // 向服务端发送心跳包
                Server.ReceiveHeartbeat(ClientID);

                Thread.Sleep(1000);
            }
        }
    }

    /// <summary>
    /// 客户端信息
    /// </summary>
    public class ClientInfo
    {
        // 客户端ID
        public Int32 ClientID;

        // 最后心跳时间
        public DateTime LastHeartbeatTime;

        // 状态
        public Boolean State;
    }
}
