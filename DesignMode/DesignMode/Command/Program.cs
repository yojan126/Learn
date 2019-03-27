using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**************************命令模式************************************/
namespace Command
{
    class Program
    {
        /// <summary>
        /// 院领导
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 初始化receiver、command、invoke
            Receiver r = new Receiver();
            Command c = new ConcreteCommand(r);
            Invoke i = new Invoke(c);

            // 院领导发出命令
            i.ExcuteCommand();
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 教官，负责调用命令对象执行请求
    /// </summary>
    public class Invoke
    {
        public Command _command;

        public Invoke(Command command)
        {
            this._command = command;
        }

        public void ExcuteCommand()
        {
            _command.Action();
        }
    }

    /// <summary>
    /// 命令抽象类
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// 命令应该知道接收者是谁，所有由receiver这个成员变量
        /// </summary>
        protected Receiver _receiver;

        public Command(Receiver receiver)
        {
            this._receiver = receiver;
        }

        /// <summary>
        /// 命令执行方法
        /// </summary>
        public abstract void Action();
    }

    /// <summary>
    /// 具体命令执行的实现
    /// </summary>
    public class ConcreteCommand : Command
    {
        public ConcreteCommand(Receiver receiver)
            : base(receiver)
        {

        }

        public override void Action()
        {
            // 调用接受的方法，因为执行命令的是学生
            _receiver.Run1000Meters();
        }
    }

    /// <summary>
    /// 命令接收者，学生
    /// </summary>
    public class Receiver
    {
        public void Run1000Meters()
        {
            Console.WriteLine("跑1000米");
        }
    }
}
