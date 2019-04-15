using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateTest
{
    class Test1
    {
        static void OtherClassMethod()
        {
            Console.WriteLine("Delegate an other class's method");
        }

        public static void DoThis()
        {
            var test = new TestDelegate();
            test.delegateMethod = new TestDelegate.DelegateMethod(test.NonStaticMethod);
            test.delegateMethod += new TestDelegate.DelegateMethod(TestDelegate.StaticMethod);
            test.delegateMethod += OtherClassMethod;
            test.RunDelegateMethod();

            Console.ReadLine();
        }
    }

    class TestDelegate
    {
        public delegate void DelegateMethod();  // 声明了一个delegate type

        public DelegateMethod delegateMethod;   // 声明了一个delegate对象

        public static void StaticMethod()
        {
            Console.WriteLine("Delegate a static method");
        }

        public void NonStaticMethod()
        {
            Console.WriteLine("Delegate a non-static method");
        }

        public void RunDelegateMethod()
        {
            if (delegateMethod != null)
            {
                Console.WriteLine("-------------------");
                delegateMethod.Invoke();
                Console.WriteLine("-------------------");
            }
        }
    }
}
