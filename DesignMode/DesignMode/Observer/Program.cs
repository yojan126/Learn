using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Observer
{
    /// <summary>
    /// 客户端
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 1.
            // NotObserver.Dothis();

            // 2.
            //Tengxun tengxun = new TengxunGame("Tengxun New Game Conference", "Release a New Game Create By ......");

            //// 添加订阅者
            //tengxun.AddObserver(new SubObserver("John"));
            //tengxun.AddObserver(new SubObserver("Michael"));

            //tengxun.Update();

            //Console.ReadLine();

            // 3.
            ObserverInNet.DoThis();
        }
    }

    /// <summary>
    /// 订阅号抽象类
    /// </summary>
    public abstract class Tengxun
    {
        /// <summary>
        /// 保存订阅者列表
        /// </summary>
        private List<IObserver> observers = new List<IObserver>();

        public string Symbol { get; set; }

        public string Info { get; set; }

        public Tengxun(string symbol, string info)
        {
            this.Symbol = symbol;
            this.Info = info;
        }

        // 对订阅者的维护操作
        public void AddObserver(IObserver observer)
        {
            observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Update()
        {
            // 遍历订阅者列表进行通知
            foreach (IObserver ob in observers)
            {
                if (ob != null)
                {
                    ob.ReceiveAndPrint(this);
                }
            }
        }
    }

    /// <summary>
    /// 具体订阅号类
    /// </summary>
    public class TengxunGame : Tengxun
    {
        public TengxunGame(string symbol, string info)
            : base(symbol, info)
        {

        }
    }

    /// <summary>
    /// 订阅者接口
    /// </summary>
    public interface IObserver
    {
        void ReceiveAndPrint(Tengxun tengxun);
    }

    /// <summary>
    /// 具体的订阅者
    /// </summary>
    public class SubObserver : IObserver
    {
        private string Name { get; set; }
        public SubObserver(string name)
        {
            this.Name = name;
        }

        public void ReceiveAndPrint(Tengxun tengxun)
        {
            Console.WriteLine("Notified {0} of {1}'s" + " Info is : {2}", Name, tengxun.Symbol, tengxun.Info);
        }
    }
}
