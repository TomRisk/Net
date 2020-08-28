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

            UpAndDownCast();
            //Console.WriteLine("Hello World!");
            Console.ReadKey();
        }


        #region 多态
        public static void MulMethod()
        {
            Stock stock = new Stock { Name = "Stock.Name" };
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

        /// <summary>
        /// 向上向下转换
        /// 向下引用：基类创建一个子类引用
        /// </summary>
        public static void UpAndDownCast()
        {
            //Stock stock = new Stock { Name= "Stock.Name" };
            //Assets assets = stock;//子类转换成基类，向上转换
            //House house = (House)assets;//基类转换成子类，向下转换

            //Console.WriteLine(stock.Name);
            //Console.WriteLine(assets.Name);
            //Console.WriteLine(house.Name);


            //as  
            //向下转换时为null
            
            Stock stock2 = new Stock();
            Assets assets2 = stock2;
            Assets assets4 = new Assets();
            Stock stock3 = assets4 as Stock;
            if (stock3 == null)
            {

            }

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
