using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverAndDelegateAndEvent
{
    class First
    {
        public static void Do()
        {
            Cat cat = new Cat("Tom");
            cat.Add(new Rat("A", cat));
            cat.Add(new Rat("B", cat));

            cat.Shout();
            Console.ReadLine();
        }

    }

    class Rat
    {
        private string Name { get; set; }
        private Cat MyCat { get; set; }

        internal Rat() { }
        internal Rat(string name, Cat cat)
        {
            this.Name = name;
            this.MyCat = cat;
        }

        internal void Run()
        {
            Console.WriteLine("{0} 猫来了，大家快跑。 我是 {1}", MyCat.Name, Name);
        }
    }

    public class Cat
    {
        public string Name { get; set; }
        List<Rat> lst = new List<Rat>();

        internal Cat() { }
        internal Cat(string name)
        {
            this.Name = name;
        }

        internal void Add(Rat rat)
        {
            lst.Add(rat);
        }

        internal void Remove(Rat rat)
        {
            lst.Remove(rat);
        }

        internal void Shout()
        {
            Console.WriteLine("喵喵喵");
            if (lst != null)
            {
                foreach (Rat item in lst)
                {
                    item.Run();
                }
            }
        }
    }
}
