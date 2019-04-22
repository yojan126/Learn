using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    class AbortThreadTest
    {

        public static void DoThis()
        {
            Thread abortThread = new Thread(AbortMethod);
            abortThread.Name = "Abort Thread";
            abortThread.Start();
            Thread.Sleep(1000);
            try
            {
                abortThread.Abort();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception happen in Main Thread", Thread.CurrentThread.Name);
                Console.WriteLine("{0} Status is:{1} In Main Thread", Thread.CurrentThread.Name, Thread.CurrentThread.ThreadState);
            }
            finally
            {
                Console.WriteLine("{0} Status is:{1} In Main Thread", abortThread.Name, abortThread.ThreadState);
            }

            abortThread.Join();
            Console.WriteLine("{0} Status is:{1}", abortThread.Name, abortThread.ThreadState);
            Console.ReadKey();

        }

        private static void AbortMethod()
        {
            try
            {
                Thread.Sleep(5000);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.GetType().Name);
                Console.WriteLine("{0} Exception happen In Abort Thread", Thread.CurrentThread.Name);
                Console.WriteLine("{0} Status is:{1} In Abort Thread", Thread.CurrentThread.Name, Thread.CurrentThread.ThreadState);
            }
            finally
            {
                Console.WriteLine("{0} Status is:{1} In Abort Thread", Thread.CurrentThread.Name, Thread.CurrentThread.ThreadState);
            }
        }
    }

    /*
     从运行结果可以看出，调用Abort方法的线程引发的异常类型为ThreadAbortException, 以及异常只会在 调用Abort方法的线程中发生，
     而不会在主线程中抛出，并且调用Abort方法后线程的状态不是立即改变为Aborted状态，而是从AbortRequested->Aborted。
     */
}
