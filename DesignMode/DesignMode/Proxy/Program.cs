using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    /**************************代理模式******************************/
    class Program
    {
        static void Main(string[] args)
        {
            Person proxy = new Friend();
            proxy.BuyProduct();
            Console.ReadLine();
        }
    }

    public abstract class Person
    {
        public abstract void BuyProduct();
    }

    public class RealBuyPerson : Person
    {
        public override void BuyProduct()
        {
            Console.WriteLine("帮我买一台IPhone和一台Macbook");
        }
    }

    public class Friend : Person
    {
        RealBuyPerson realSubject;

        public override void BuyProduct()
        {
            Console.WriteLine("通过代理类方位真实实体对象的方法");
            if (realSubject == null)
            {
                realSubject = new RealBuyPerson();
            }

            this.PreBuyProduct();

            realSubject.BuyProduct();
            this.PostBuyProduct();
        }

        public void PreBuyProduct()
        {
            Console.WriteLine("我怕弄糊涂了，需要一张清单，May要带相机，April要带IPhone......");
        }

        public void PostBuyProduct()
        {
            Console.WriteLine("终于买完了，现在要对东西分一下，相机是May的，IPhone是April的......");
        }
    }
}
