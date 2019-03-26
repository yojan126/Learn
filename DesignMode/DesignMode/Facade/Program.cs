using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Facade
{
    /**************************外观模式********************************/
    class Program
    {
        /// <summary>
        /// 以学生选课系统为例子演示外观模式的使用
        /// 学生选课模块包含功能有：
        /// 验证选课的人数是否已满
        /// 通知用户课程选择成功与否
        /// 客户端代码
        /// </summary>
        private static RegistrationFacade registrationFacade = new RegistrationFacade();
        static void Main(string[] args)
        {
            if (registrationFacade.RegisterCourse("设计模式", "John"))
            {
                Console.WriteLine("选课成功");
            }
            else
            {
                Console.WriteLine("选课失败");
            }
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 外观类
    /// </summary>
    public class RegistrationFacade
    {
        private RegisterCourse registerCourse;
        private NotifyStudent notifyStudent;

        public RegistrationFacade()
        {
            registerCourse = new RegisterCourse();
            notifyStudent = new NotifyStudent();
        }

        public bool RegisterCourse(string CourseName, string StudentName)
        {
            if (!registerCourse.CheckAvailable(CourseName))
            {
                return false;
            }

            return notifyStudent.Notify(StudentName);
        }
    }

    // 子系统

    /// <summary>
    /// 相当于子系统A
    /// </summary>
    public class RegisterCourse
    {
        public bool CheckAvailable(string CourseName)
        {
            Console.WriteLine("正在验证课程 {0} 是否人数已满", CourseName);
            return true;
        }
    }

    /// <summary>
    /// 相当于子系统B
    /// </summary>
    public class NotifyStudent
    {
        public bool Notify(string studentName)
        {
            Console.WriteLine("正在向 {0} 发送通知", studentName);
            return true;
        }
    }
}
