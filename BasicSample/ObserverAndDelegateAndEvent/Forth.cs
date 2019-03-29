using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverAndDelegateAndEvent
{
    public delegate void Cat4thShoutEventHandler();

    class Forth
    {
        public static void Do()
        {
            Cat4th cat = new Cat4th();
            Rat4th r1 = new Rat4th("A");
            Rat4th r2 = new Rat4th("B");

            cat.Cat4thShoutEvent += r1.Run;
            cat.Cat4thShoutEvent += r2.Run;

            cat.Shout();
            Console.ReadLine();
        }

    }

    public class Cat4th
    {
        public event Cat4thShoutEventHandler Cat4thShoutEvent;

        public void Shout()
        {
            Console.WriteLine("喵喵喵");

            if (Cat4thShoutEvent != null)
            {
                Cat4thShoutEvent();
            }
        }
    }

    public class Rat4th
    {
        public string Name { get; set; }

        public Rat4th() { }

        public Rat4th(string name)
        {
            this.Name = name;
        }

        public void Run()
        {
            Console.WriteLine("{0} 跑了", Name);
        }
    }
}
