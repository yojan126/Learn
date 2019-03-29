using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverAndDelegateAndEvent
{
    public delegate void CatShoutEventHandler();

    class Third
    {
        public static void Do()
        {
            Cat3rd cat = new Cat3rd();
            Rat3rd r1 = new Rat3rd("A");
            Rat3rd r2 = new Rat3rd("B");

            //注!!添加的是整个方法 不需要加括号
            cat.CatShoutEvent += r1.Run;
            cat.CatShoutEvent += r2.Run;

            cat.Shout();
            Console.ReadLine();
        }

    }

    public class Cat3rd
    {
        public CatShoutEventHandler CatShoutEvent;

        public void Shout()
        {
            Console.WriteLine("喵喵喵");

            if (CatShoutEvent != null)
            {
                CatShoutEvent();
            }
        }
    }

    public class Rat3rd
    {
        public string Name { get; set; }

        public Rat3rd() { }

        public Rat3rd(string name)
        {
            this.Name = name;
        }

        public void Run()
        {
            Console.WriteLine("{0} 跑了", Name);
        }
    }
}
