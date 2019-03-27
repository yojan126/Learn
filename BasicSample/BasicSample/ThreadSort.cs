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

            foreach (int i in intArray)
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
