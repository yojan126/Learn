using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public class Class1
    {
    }

    /// <summary>
    /// 客户类
    /// </summary>
    public class Customer
    {
        static void Main()
        {
            // 客户找老板说要买电脑，这里要装两台电脑
            // 创建指挥者和建造者
            Director director = new Director();
            Builder builder1 = new ConcreterBuilder1();
            Builder builder2 = new ConcreterBuilder2();

            // 老板找员工装第一台电脑
            director.Construct(builder1);

            // 组装完成，员工搬来组装好的第一台电脑
            Computer computer1 = builder1.GetComputer();
            computer1.Show();

            // 老板找员工装第二台电脑
            director.Construct(builder2);

            // 组装完成，员工搬来组装好的第二台电脑
            Computer computer2 = builder2.GetComputer();
            computer2.Show();
        }
    }

    /// <summary>
    /// 小王和小李难道会自愿地去组装嘛，谁不想休息的，这必须有一个人叫他们去组装才会去的
    /// 这个人当然就是老板了，也就是建造者模式中的指挥者
    /// 指挥创建过程类
    /// </summary>
    public class Director
    {
        public void Construct(Builder builder)
        {
            builder.BuildPartCPU();
            builder.BuildPartMainBoard();
        }
    }

    /// <summary>
    /// 电脑类
    /// </summary>
    public class Computer
    {
        /// <summary>
        /// 电脑组建集合
        /// </summary>
        private List<string> lstParts = new List<string>();

        /// <summary>
        /// 把单个组建添加到电脑组建集合中
        /// </summary>
        /// <param name="str_Part"></param>
        public void Add(string str_Part)
        {
            lstParts.Add(str_Part);
        }

        public string Show()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("电脑正在组装.........");
            foreach (string str in lstParts)
            {
                sb.Append(str);
            }
            sb.Append("电脑组装好了");
            return sb.ToString();
        }
    }

    /// <summary>
    /// 抽象建造者，这个场景下为 组装人，这里也可以定义为接口
    /// </summary>
    public abstract class Builder
    {
        /// <summary>
        /// 装CPU
        /// </summary>
        public abstract void BuildPartCPU();

        /// <summary>
        /// 装主板
        /// </summary>
        public abstract void BuildPartMainBoard();

        // 还可以装其它

        /// <summary>
        /// 获得组装好的电脑
        /// </summary>
        /// <returns></returns>
        public abstract Computer GetComputer();
    }

    /// <summary>
    /// 具体创建者，具体的某个人为具体创建者，如，装机小王
    /// </summary>
    public class ConcreterBuilder1 : Builder
    {
        Computer computer = new Computer();
        public override void BuildPartCPU()
        {
            computer.Add("CPU1");
        }
        public override void BuildPartMainBoard()
        {
            computer.Add("Main Board1");
        }

        public override Computer GetComputer()
        {
            return computer;
        }
    }

    /// <summary>
    /// 具体创建者，具体的某个人为具体创建者，如，装机小李
    /// 又装另一台电脑了
    /// </summary>
    public class ConcreterBuilder2 : Builder
    {
        Computer computer = new Computer();
        public override void BuildPartCPU()
        {
            computer.Add("CPU2");
        }
        public override void BuildPartMainBoard()
        {
            computer.Add("Main Board2");
        }

        public override Computer GetComputer()
        {
            return computer;
        }
    }
}
