using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BasicSample
{
    class ThreadSort
    {
        public static void ThreadSortMethod()
        {
            int[] intArray = new int[] { 3, 1, 5, 7, 2, 9, 6, 8, 4 };
            List<int> lst = new List<int>();
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                lst.Add(random.Next(1, 99));
            }
            foreach (int i in lst)
            {
                new Thread(x =>
                {
                    Thread.Sleep(i * 50);   // 经验值 相隔50ms基本不出现排序错误
                    Console.WriteLine(i);
                }).Start();
            }
            Console.ReadLine();
        }
    }
}
