using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSample
{
    public delegate void RaiseEventHandler(string Hand);

    public delegate void FallEventHandler();

    class DelegateSample
    {
        public static void Do()
        {
            A a = new A();
            B b = new B(a);
            C c = new C(a);
            Random random = new Random(3);
            int i;
            while (true)
            {
                i = random.Next(1, 4);
                switch (i)
                {
                    case 1:
                        a.Raise("左");
                        break;
                    case 2:
                        a.Raise("右");
                        break;
                    case 3:
                        a.Fall();
                        break;
                    default:
                        break;
                }
                Console.ReadLine();
            }
        }

    }

    public class A
    {
        public event RaiseEventHandler RaiseEvent;

        public event FallEventHandler FallEvent;

        public void Raise(string Hand)
        {
            Console.WriteLine("首领A{0}手举杯", Hand);

            if (RaiseEvent != null)
            {
                RaiseEvent(Hand);
            }
        }

        public void Fall()
        {
            Console.WriteLine("首领A摔杯");

            if (FallEvent != null)
            {
                FallEvent();
            }
        }
    }

    public class B
    {
        A a;

        public B(A a)
        {
            this.a = a;
            a.RaiseEvent += new RaiseEventHandler(a_RaiseEvent);
            a.FallEvent += new FallEventHandler(a_FallEvent);
        }

        void a_RaiseEvent(string Hand)
        {
            if (Hand.Equals("左"))
            {
                Attack();
            }
        }

        void a_FallEvent()
        {
            Attack();
        }

        public void Attack()
        {
            Console.WriteLine("部下B发起攻击");
        }
    }

    public class C
    {
        A a;

        public C(A a)
        {
            this.a = a;
            a.RaiseEvent += new RaiseEventHandler(a_RaiseEvent);
            a.FallEvent += new FallEventHandler(a_FallEvent);
        }

        void a_RaiseEvent(string Hand)
        {
            if (Hand.Equals("右"))
            {
                Attack();
            }
        }

        void a_FallEvent()
        {
            Attack();
        }

        public void Attack()
        {
            Console.WriteLine("部下C发起攻击");
        }

    }
}
