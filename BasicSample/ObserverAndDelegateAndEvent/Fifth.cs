using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverAndDelegateAndEvent
{
    class Fifth
    {
        public static void Do()
        {
            Cat5th cat = new Cat5th("Tom");
            Rat5th r1 = new Rat5th("A");
            Rat5th r2 = new Rat5th("B");
            Rat5th r3 = new Rat5th();
            cat.catShout += r1.Run;
            cat.catShout += r2.Run;
            cat.catShout += r3.Run;
            cat.Shout();
            Console.ReadLine();
        }
    }

    public class Cat5thShoutEventArgs : EventArgs
    {
        public string CatName { get; set; }

        public Cat5thShoutEventArgs(string name)
        {
            this.CatName = name;
        }
    }

    public delegate void Cat5thShoutEventHandler(object sender, Cat5thShoutEventArgs e);

    public class Cat5th
    {
        public string Name { get; set; }

        public event Cat5thShoutEventHandler catShout;

        public void Shout()
        {
            Console.WriteLine("喵喵喵");

            if (catShout != null)
            {
                Cat5thShoutEventArgs e = new Cat5thShoutEventArgs(Name);

                catShout(this, e);
            }
        }

        public Cat5th() { }

        public Cat5th(string name)
        {
            this.Name = name;
        }
    }

    public class Rat5th
    {
        public string Name { get; set; }

        public void Run(object sender,Cat5thShoutEventArgs e)
        {
            Console.WriteLine("{0} 来了。 {1} 跑了", e.CatName, Name);
        }

        public Rat5th() { }

        public Rat5th(string name)
        {
            this.Name = name;
        }
    }
}
