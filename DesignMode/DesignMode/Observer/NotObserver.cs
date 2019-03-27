using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    public class NotObserver
    {
        /// <summary>
        /// 客户端
        /// </summary>
        public static void Dothis()
        {
            // 实例化订阅者和订阅号对象
            Subscriber subscriber = new Subscriber("John");
            TengXunGame Tengxun = new TengXunGame();

            Tengxun.Subscriber = subscriber;
            Tengxun.Symbol = "Game New Conference";
            Tengxun.Info = "Have a new game create by ...";
            Tengxun.Update();
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 腾讯游戏订阅号类
    /// </summary>
    public class TengXunGame
    {
        /// <summary>
        /// 订阅者对象
        /// </summary>
        public Subscriber Subscriber { get; set; }

        public string Symbol { get; set; }

        public string Info { get; set; }

        public void Update()
        {
            if (Subscriber != null)
            {
                // 调用订阅者对象来通知订阅者
                Subscriber.ReceiveAndPrintData(this);
            }
        }

    }

    /// <summary>
    /// 订阅者类
    /// </summary>
    public class Subscriber
    {
        public string Name { get; set; }
        public Subscriber(string name)
        {
            this.Name = name;
        }

        public void ReceiveAndPrintData(TengXunGame tengxunGame)
        {
            Console.WriteLine("Notified {0} of {1} is {2}'s", Name, tengxunGame.Symbol, tengxunGame.Info);
        }
    }
}
