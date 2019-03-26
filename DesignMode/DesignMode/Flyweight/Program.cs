using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/********************************享元模式***************************************/
namespace Flyweight
{
    /// <summary>
    /// 客户端调用
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 定义外部状态，例如字母的位置等信息
            int extrinsicstate = 10;
            
            // 初始化享元工厂
            FlyweightFactory factory = new FlyweightFactory();

            // 判断是否创建了字母A，如果已经创建就直接使用创建的对象A
            Flyweight fa = factory.GetFlyweight("A");
            if (fa != null)
            {
                // 把外部对象作为享元对象的方法调用参数
                fa.Operation(--extrinsicstate);
            }

            // 判断是否创建了字母B，如果已经创建就直接使用创建的对象B
            Flyweight fb = factory.GetFlyweight("B");
            if (fb != null)
            {
                fb.Operation(--extrinsicstate);
            }

            // 判断是否创建了字母C，如果已经创建就直接使用创建的对象C
            Flyweight fc = factory.GetFlyweight("C");
            if (fc != null)
            {
                fc.Operation(--extrinsicstate);
            }

            // 判断是否创建了字母D，如果已经创建就直接使用创建的对象D
            Flyweight fd = factory.GetFlyweight("D");
            if (fd != null)
            {
                fd.Operation(--extrinsicstate);
            }
            else
            {
                Console.WriteLine("驻留池中不存在字符串D");
                // 这时候需要创建一个对象并放入驻留池中
                ConcreteFlyweight d = new ConcreteFlyweight("D");
                factory.flyweights.Add("D", d);
            }

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 享元工厂，负责创建和管理享元对象
    /// </summary>
    public class FlyweightFactory
    {
        public Dictionary<string, Flyweight> flyweights = new Dictionary<string, Flyweight>();

        public FlyweightFactory()
        {
            flyweights.Add("A", new ConcreteFlyweight("A"));
            flyweights.Add("B", new ConcreteFlyweight("B"));
            flyweights.Add("C", new ConcreteFlyweight("C"));
        }

        public Flyweight GetFlyweight(string key)
        {
            Flyweight flyweight;
            if (flyweights.ContainsKey(key) == false)
            {
                Console.WriteLine("驻留池中不存在字符串---" + key);
                flyweight = new ConcreteFlyweight(key);
            }
            else
            {
                flyweight = flyweights[key] as Flyweight;
            }
            return flyweight;
        }
    }

    /// <summary>
    /// 抽象享元类，提供具体享元类具有的方法
    /// </summary>
    public abstract class Flyweight
    {
        public abstract void Operation(int extrinsicstate);
    }

    /// <summary>
    /// 具体享元对象，这样我们不把每个字母设计成单独的类了，而是把共享的字母作为享元对象的内部状态
    /// </summary>
    public class ConcreteFlyweight : Flyweight
    {
        /// <summary>
        /// 内部状态
        /// </summary>
        private string intrinsicstate;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="innerState"></param>
        public ConcreteFlyweight(string innerState)
        {
            this.intrinsicstate = innerState;
        }

        /// <summary>
        /// 享元的实例方法
        /// </summary>
        /// <param name="extrinsicstate">外部状态</param>
        public override void Operation(int extrinsicstate)
        {
            Console.WriteLine("具体实现类：intrinsicstate {0}, extrinsicstate {1}", intrinsicstate, extrinsicstate);
        }
    }
}
