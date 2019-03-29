using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverAndDelegateAndEvent
{
    class Second
    {
        public static void Do()
        {
            CatSec c1 = new CatSec("Tom");
            c1.Add(new RatSec("A", c1));
            c1.Add(new RatSec("B", c1));
            c1.Add(new BirdSec("C", c1));

            c1.Shout();
            Console.ReadLine();
        }
    }

    public abstract class Pet
    {
        public List<IRunable> lst = new List<IRunable>();
        public void Add(IRunable runable)
        {
            lst.Add(runable);
        }

        public void Remove(IRunable runable)
        {
            lst.Remove(runable);
        }

        public abstract void Shout();
    }

    public class CatSec : Pet
    {
        public string Name { get; set; }

        public CatSec() { }

        public CatSec(string name)
        {
            this.Name = name;
        }

        public override void Shout()
        {
            Console.WriteLine("喵喵喵");

            if (lst != null)
            {
                foreach (IRunable item in lst)
                {
                    item.Run();
                }
            }
        }
    }

    public interface IRunable
    {
        void Run();
    }

    public class RatSec : IRunable
    {
        public string Name { get; set; }

        public CatSec myCat { get; set; }

        public RatSec() { }

        public RatSec(string name, CatSec cat)
        {
            this.Name = name;
            this.myCat = cat;
        }

        public void Run()
        {
            Console.WriteLine("{0} 猫来了，快跑。我是老鼠 {1}", myCat.Name, Name);
        }
    }

    public class BirdSec : IRunable
    {
        public string Name { get; set; }

        public CatSec myCat { get; set; }

        public BirdSec() { }

        public BirdSec(string name, CatSec cat)
        {
            this.Name = name;
            this.myCat = cat;
        }

        public void Run()
        {
            Console.WriteLine("{0} 猫来了，快跑。我是小鸟 {1}", myCat.Name, Name);
        }
    }
}
