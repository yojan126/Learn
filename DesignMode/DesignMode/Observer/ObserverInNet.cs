using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    /// <summary>
    /// 委托充当订阅者接口类
    /// </summary>
    /// <param name="sender"></param>
    public delegate void NotifyEventHandler(object sender);

    /// <summary>
    /// 
    /// </summary>
    class ObserverInNet
    {
        public static void DoThis()
        {
            Tencent tencent = new TencentGame("Tencent Game", "We Have A New Game Conference Which Is Create By ......");

            SubscriberMember member1 = new SubscriberMember("John");
            SubscriberMember member2 = new SubscriberMember("Tom");

            // 添加订阅者
            tencent.AddObserver(new NotifyEventHandler(member1.ReceiveAndPrint));
            tencent.AddObserver(new NotifyEventHandler(member2.ReceiveAndPrint));

            tencent.Update();

            Console.WriteLine("-----------------------------");
            Console.WriteLine("移除Tom订阅者");
            tencent.RemoveObserver(new NotifyEventHandler(member2.ReceiveAndPrint));
            tencent.Update();

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 抽象订阅号类
    /// </summary>
    public class Tencent
    {
        public NotifyEventHandler NotifyEvent;

        public string Symbol { get; set; }

        public string Info { get; set; }

        public Tencent(string symbol, string info)
        {
            this.Symbol = symbol;
            this.Info = info;
        }

        // 订阅者维护操作
        public void AddObserver(NotifyEventHandler ob)
        {
            NotifyEvent += ob;
        }

        public void RemoveObserver(NotifyEventHandler ob)
        {
            NotifyEvent -= ob;
        }

        public void Update()
        {
            //if (NotifyEvent != null)
            //{
            //    NotifyEvent(this);
            //}
            // 等效
            NotifyEvent?.Invoke(this);

        }
    }

    /// <summary>
    /// 具体订阅号类
    /// </summary>
    public class TencentGame : Tencent
    {
        public TencentGame(string symbol, string info)
            : base(symbol, info)
        {

        }
    }

    /// <summary>
    /// 具体订阅者类
    /// </summary>
    public class SubscriberMember
    {
        public string Name { get; set; }

        public SubscriberMember(string name)
        {
            this.Name = name;
        }

        public void ReceiveAndPrint(Object obj)
        {
            Tencent tencent = obj as Tencent;

            if (tencent != null)
            {
                Console.WriteLine("Notified {0} of {1}'s" + " Info is : {2}", Name, tencent.Symbol, tencent.Info);
            }
        }
    }
}
