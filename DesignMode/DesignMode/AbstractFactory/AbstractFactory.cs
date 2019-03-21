using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    /**************************抽象工厂模式********************************/
    /// <summary>
    /// 下面以绝味鸭脖连锁店为例子演示下抽象工厂模式
    /// 因为每个地方的口味各不相同，有些地方喜欢辣一点，有的地方不喜欢辣
    /// 客户端调用
    /// </summary>
    class AbstractFactory
    {
        static void Main()
        {
            // 南昌工厂制作南昌的鸭脖和鸭架
            ProduceFactory PFNanchang = new NanchangFactory();
            PFNanchang.CreateYabo().Print();
            PFNanchang.CreateYajia().Print();

            // 上海工厂制作上海的鸭脖和鸭架
            ProduceFactory PFShanghai = new ShanghaiFactory();
            PFShanghai.CreateYabo().Print();
            PFShanghai.CreateYajia().Print();

            // 有南京口味的鸭脖鸭架了
            ProduceFactory PFNanjing = new NanjingFactory();
            PFNanjing.CreateYabo().Print();
            PFNanjing.CreateYajia().Print();
        }
    }

    /// <summary>
    /// 抽象工厂类，提供创建两个不同地方的鸭脖和鸭架的接口
    /// </summary>
    public abstract class ProduceFactory
    {
        public abstract Yabo CreateYabo();
        public abstract Yajia CreateYajia();
    }

    /// <summary>
    /// 南昌绝味鸭脖工厂负责制作南昌的鸭脖和鸭架
    /// </summary>
    public class NanchangFactory : ProduceFactory
    {
        /// <summary>
        /// 制作南昌鸭脖
        /// </summary>
        /// <returns></returns>
        public override Yabo CreateYabo()
        {
            return new NanchangYabo();
        }

        /// <summary>
        /// 制作南昌鸭架
        /// </summary>
        /// <returns></returns>
        public override Yajia CreateYajia()
        {
            return new NanchangYajia();
        }
    }

    /// <summary>
    /// 上海绝味鸭脖工厂负责制作上海的鸭脖和鸭架
    /// </summary>
    public class ShanghaiFactory : ProduceFactory
    {
        /// <summary>
        /// 制作上海鸭脖
        /// </summary>
        /// <returns></returns>
        public override Yabo CreateYabo()
        {
            return new ShanghaiYabo();
        }

        /// <summary>
        /// 制作上海鸭架
        /// </summary>
        /// <returns></returns>
        public override Yajia CreateYajia()
        {
            return new ShanghaiYajia();
        }
    }

    /// <summary>
    /// 鸭脖抽象类，供每个地方的鸭脖类继承
    /// </summary>
    public abstract class Yabo
    {
        public abstract string Print();
    }

    /// <summary>
    /// 鸭架抽象类，供每个地方的鸭架类继承
    /// </summary>
    public abstract class Yajia
    {
        public abstract string Print();
    }

    /// <summary>
    /// 南昌的鸭脖类，因为江西人喜欢吃辣的，所以南昌的鸭脖稍微会比上海做的辣
    /// </summary>
    public class NanchangYabo:Yabo
    {
        public override string Print()
        {
            return "南昌鸭脖";
        }
    }

    /// <summary>
    /// 上海的鸭脖没有南昌的鸭脖做的辣
    /// </summary>
    public class ShanghaiYabo : Yabo
    {
        public override string Print()
        {
            return "上海鸭脖";
        }
    }

    /// <summary>
    /// 南昌的鸭架
    /// </summary>
    public class NanchangYajia : Yajia
    {
        public override string Print()
        {
            return "南昌鸭架";
        }
    }

    /// <summary>
    /// 上海的鸭架
    /// </summary>
    public class ShanghaiYajia : Yajia
    {
        public override string Print()
        {
            return "上海鸭架";
        }
    }



    /***********************急需在南京新增工厂*************************/
    /// <summary>
    /// 南京建厂
    /// </summary>
    public class NanjingFactory : ProduceFactory
    {
        public override Yabo CreateYabo()
        {
            return new NanjingYabo();
        }
        public override Yajia CreateYajia()
        {
            return new NanjingYajia();
        }
    }

    /// <summary>
    /// 南京鸭脖产线
    /// </summary>
    public class NanjingYabo : Yabo
    {
        public override string Print()
        {
            return "南京鸭脖";
        }
    }

    /// <summary>
    /// 南京鸭架产线
    /// </summary>
    public class NanjingYajia : Yajia
    {
        public override string Print()
        {
            return "南京鸭架";
        }
    }

}
