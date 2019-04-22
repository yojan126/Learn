using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadTest
{
    class InterruptThreadTest
    {
        public static void DoThis()
        {
            Thread interruptThread = new Thread(InterruptMethod);
            interruptThread.Name = "Interrupt Thread";
            interruptThread.Start();
            interruptThread.Interrupt();

            interruptThread.Join();
            Console.WriteLine("{0} Status is :{1} ", interruptThread.Name, interruptThread.ThreadState);
            Console.ReadKey();
        }

        public static void DoThis2nd()
        {
            Thread thread = new Thread(TestMethod);
            thread.Name = "Interrupt Thread";
            thread.Start();
            Thread.Sleep(100);

            thread.Interrupt();
            // thread.Abort();
            Thread.Sleep(3000);
            Console.WriteLine("after finnally block,the thread Status is:{0}", thread.ThreadState);
            Console.ReadKey();
        }

        private static void InterruptMethod()
        {
            try
            {
                Thread.Sleep(5000);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.GetType().Name);
                Console.WriteLine("{0} Exception happen In Interrupt Thread", Thread.CurrentThread.Name);
                Console.WriteLine("{0} Status is:{1} In Interrupt Thread ", Thread.CurrentThread.Name, Thread.CurrentThread.ThreadState);
            }
            finally
            {
                Console.WriteLine("{0} Status is:{1} In Interrupt Thread", Thread.CurrentThread.Name, Thread.CurrentThread.ThreadState);
            }
            /*
             从结果中可以得到，调用Interrupt方法抛出的异常为：ThreadInterruptException, 以及当调用Interrupt方法后线程的状态应该是中断的，
             但是从运行结果看此时的线程因为了Join,Sleep方法而唤醒了线程，为了进一步解释调用Interrupt方法的线程可以被唤醒， 
             我们可以在线程执行的方法中运用循环，如果线程可以唤醒，则输出结果中就一定会有循环的部分，
             然而调用Abort方法线程就直接终止，就不会有循环的部分
             */

        }

        private static void TestMethod()
        {
            for (int i = 0; i < 4; i++)
            {
                try
                {
                    Thread.Sleep(2000);
                    Console.WriteLine("Thread is Running");
                }
                catch (Exception e)
                {
                    if (e != null)
                    {
                        Console.WriteLine("Exception {0} throw", e.GetType().Name);
                    }
                }
                finally
                {
                    Console.WriteLine("Current Thread status is:{0}", Thread.CurrentThread.ThreadState);
                }
            }
        }
    }
}
