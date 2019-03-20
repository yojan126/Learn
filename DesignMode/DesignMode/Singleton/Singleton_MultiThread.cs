using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singleton
{
    /***********************单例模式(多线程)***********************/
    /// <summary>
    /// 单例模式的实现
    /// </summary>
    public class Singleton_MultiThread
    {
        // 定义一个静态变量来保存类的实例
        private static Singleton_MultiThread uniqueInstance;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        // 定义私有构造函数，使外界不能创建该类实例
        private Singleton_MultiThread()
        {

        }

        public static Singleton_MultiThread GetInstance()
        {
            // 当第一个线程运行到这里，此时会对locker对象加锁
            // 当第二个线程运行到这里，首先检测到locker对象为加锁状态，线程就会挂起等待第一个线程解锁
            // lock语句运行完之后（即线程运行完之后）会对该对象解锁
            lock (locker)
            {

                if (uniqueInstance == null)
                {
                    uniqueInstance = new Singleton_MultiThread();
                }
            }
            return uniqueInstance;
        }
    }
}
