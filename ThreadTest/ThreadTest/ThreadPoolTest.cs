using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    class ThreadPoolTest
    {
        public static void DoThis()
        {
            // 设置线程池处于活动的线程的最大数目
            // 设置线程池中工作者线程数量为1000，I/O线程数量为1000
            ThreadPool.SetMaxThreads(1000, 1000);
            Console.WriteLine("Main Thread: queue an asynchronous method");
            PrintMessage("Main Thread Start");

            // 把工作项添加到队列中，此时线程池会用工作者线程去执行回调方法
            ThreadPool.QueueUserWorkItem(asyncMethod);
            Console.ReadKey();
        }


        /// <summary>
        /// 方法必须匹配WaitCallback委托
        /// </summary>
        /// <param name="state"></param>
        private static void asyncMethod(object state)
        {
            Thread.Sleep(1000);
            PrintMessage("Asynchronous Method");
            Console.WriteLine("Asynchronous Thread has worked ");
        }

        /// <summary>
        /// 打印线程池信息
        /// </summary>
        /// <param name="data"></param>
        private static void PrintMessage(string data)
        {
            int workthreadnumber;
            int iothreadnumber;

            // 获得线程池中可用的线程，把获得的可用工作者线程数量赋给workthreadnumber变量
            // 获得的可用I/O线程数量给iothreadnumber变量
            ThreadPool.GetAvailableThreads(out workthreadnumber, out iothreadnumber);

            Console.WriteLine("{0}\n CurrentThreadId is {1}\n CurrentThread is background :{2}\n WorkThreadNumber is:{3}\n IOThreadNumbers is: {4}\n"
                , data
                , Thread.CurrentThread.ManagedThreadId.ToString()
                , Thread.CurrentThread.IsBackground.ToString()
                , workthreadnumber.ToString()
                , iothreadnumber.ToString());
        }
    }
}
