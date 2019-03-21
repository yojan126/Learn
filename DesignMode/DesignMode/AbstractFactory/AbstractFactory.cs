using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class AbstractFactory
    {
        static void Main()
        {
            ProduceFactory PFNanchang = new NanchangFactory();
            PFNanchang.CreateYabo().Print();
            PFNanchang.CreateYajia().Print();

            ProduceFactory PFShanghai = new ShanghaiFactory();
            PFShanghai.CreateYabo().Print();
            PFShanghai.CreateYajia().Print();

            // 有南京口味的鸭脖鸭架了
            ProduceFactory PFNanjing = new NanjingFactory();
            PFNanjing.CreateYabo().Print();
            PFNanjing.CreateYajia().Print();
        }
    }

    public abstract class ProduceFactory
    {
        public abstract Yabo CreateYabo();
        public abstract Yajia CreateYajia();
    }

    public class NanchangFactory : ProduceFactory
    {
        public override Yabo CreateYabo()
        {
            return new NanchangYabo();
        }

        public override Yajia CreateYajia()
        {
            return new NanchangYajia();
        }
    }

    public class ShanghaiFactory : ProduceFactory
    {
        public override Yabo CreateYabo()
        {
            return new ShanghaiYabo();
        }
        public override Yajia CreateYajia()
        {
            return new ShanghaiYajia();
        }
    }

    public abstract class Yabo
    {
        public abstract string Print();
    }

    public abstract class Yajia
    {
        public abstract string Print();
    }

    public class NanchangYabo:Yabo
    {
        public override string Print()
        {
            return "南昌鸭脖";
        }
    }

    public class ShanghaiYabo : Yabo
    {
        public override string Print()
        {
            return "上海鸭脖";
        }
    }

    public class NanchangYajia : Yajia
    {
        public override string Print()
        {
            return "南昌鸭架";
        }
    }

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
