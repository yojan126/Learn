using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    class BackgroundThread
    {
        public static void DoThis()
        {
            // 创建一个新线程（默认为前台线程）
            Thread backThread = new Thread(Worker);

            // 使线程成为一个后台线程
            backThread.IsBackground = true;

            // 通过Start方法启动线程
            backThread.Start();

            // Join()方法能保证主线程（前台线程）在异步线程thread（后台线程）运行结束后才会运行。
            // 调用Join方法后后台线程会阻塞主线程所以主线程会后输出
            backThread.Join();

            // 如果backThread是前台线程，则应用程序大约5秒后才终止
            // 如果backThread是后台线程，则应用程序立即终止
            Console.WriteLine("Return from Main Thread");
        }

        public static void DoThis2nd()
        {
            // Thread backThread = new Thread(new ParameterizedThreadStart(ObjWorker));
            Thread backThread = new Thread(ObjWorker);      // 带参的线程方法可以省略ParameterizedThreadStart

            backThread.Start("123");

            Console.WriteLine("Return from Main Thread");
        }

        private static void Worker()
        {
            // 模拟做5秒
            Thread.Sleep(5000);

            // 下面语句，只有由一个前台线程执行时，才会显示出来
            Console.WriteLine("Return from Worker Thread");
        }

        private static void ObjWorker(object data)
        {
            // 模拟做5秒
            Thread.Sleep(5000);

            // 下面语句，只有由一个前台线程执行时，才会显示出来
            Console.WriteLine(data + " Return from Worker Thread");
            Console.ReadKey();
        }
    }


}
