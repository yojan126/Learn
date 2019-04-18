using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateTest
{
    delegate void NumberChangerEventHandler(int i);

    public class AnonymousMethod
    {
        NumberChangerEventHandler numberChangerEventHandler { get; set; }

        static int num = 10;
        public static void AddNum(int i)
        {
            num += i;
            Console.WriteLine("Name Method {0}", num);
        }

        public static void MultNum(int i)
        {
            num *= i;
            Console.WriteLine("Name Method {0}", num);
        }

        public static int GetNum()
        {
            return num;
        }

        public static void DoTest()
        {
            var v1 = new { name = "A", age = 17 };
            var v2 = new { age = 18, name = "B" };
            var v3 = new { name = "C", age = 19 };
            // v1 = v2;     error
            // v1 = v3;     correct
        }

        public static void DoThis()
        {
            // 使用匿名方法创建委托实例
            NumberChangerEventHandler nc = delegate (int i)
            {
                Console.WriteLine("Anonymous Method {0}", i);
            };

            // 使用匿名方法调用委托
            nc(10);
            // 使用命名方法实例化委托
            nc = new NumberChangerEventHandler(AddNum);
            // 使用命名方法调用委托
            nc(5);
            // 使用另一个命名方法实例化委托
            nc = new NumberChangerEventHandler(MultNum);
            // 使用命名方法调用委托
            nc(2);
            Console.ReadKey();
        }


    }


}
