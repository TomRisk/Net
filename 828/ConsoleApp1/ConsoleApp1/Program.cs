using ConsoleApp1.多态;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            MulMethod();
            //Console.WriteLine("Hello World!");
            Console.ReadKey();
        }


        #region 多态
        public static void MulMethod()
        {
            Stock stock = new Stock { Name="Stock.Name"};
            House house = new House { Name = "House.Name" };
            Assets asset = new Assets { Name = "Assets.Name" };


            Display(stock);
            Display(house);
            Display(asset);
        }
        public static void Display(Assets asset)
        {
            Console.WriteLine(asset.Name);
        }
        #endregion


        /// <summary>
        /// 客户端--服务端发送接收
        /// </summary>
        public void Message()
        {
            new Thread(Server).Start();
            Thread.Sleep(500);
            Client();
        }
        static void Client()
        {
            using (TcpClient client = new TcpClient("localhost", 51111))
            using (NetworkStream n = client.GetStream())
            {
                BinaryWriter w = new BinaryWriter(n);
                w.Write("hello");
                w.Flush();
                Console.WriteLine(new BinaryReader(n).ReadString());
            }
        }

        static void Server()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 51111);
            listener.Start();
            using (TcpClient c = listener.AcceptTcpClient())
            using (NetworkStream n = c.GetStream())
            {
                string msg = new BinaryReader(n).ReadString();
                BinaryWriter w = new BinaryWriter(n);
                w.Write(msg + " way back");
                w.Flush();
            }

            listener.Stop();
        }
    }
}
