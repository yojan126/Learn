using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Factory
{
    /***************************************工厂模式***********************************************/

    /// <summary>
    /// 客户端调用
    /// </summary>
    public class Factory
    {
        static void Main()
        {
            // 初始化做菜的两个工厂
            Creator tomatoScrambledEggsFactory = new TomatoScrambledEggsFactory();
            Creator shreddedPorkWithPotatoesFactory = new ShreddedPorkWithPotatoesFactory();

            // 开始做番茄炒蛋
            Food tomatoScrambledEggs = tomatoScrambledEggsFactory.CreateFoodFactory();
            tomatoScrambledEggs.Print();
            // 开始做土豆肉丝
            Food shreddedPorkWithPotatoes = shreddedPorkWithPotatoesFactory.CreateFoodFactory();
            shreddedPorkWithPotatoes.Print();

            // 想增加肉末茄子这道菜
            Creator roumoqieziFactory = new RoumoqieziFactory();
            Food roumoqiezi = roumoqieziFactory.CreateFoodFactory();
            roumoqiezi.Print();
        }
    }

    /// <summary>
    /// 菜抽象类
    /// </summary>
    public abstract class Food
    {
        // 输出点了什么菜
        public abstract string Print();
    }

    /// <summary>
    /// 番茄炒蛋这个菜
    /// </summary>
    public class TomatoScrambledEggs : Food
    {
        public override string Print()
        {
            return "番茄炒蛋炒好了";
        }
    }

    /// <summary>
    /// 土豆肉丝这个菜
    /// </summary>
    public class ShreddedPorkWithPotatoes : Food
    {
        public override string Print()
        {
            return "土豆肉丝炒好了";
        }
    }

    /// <summary>
    /// 抽象工厂类
    /// </summary>
    public abstract class Creator
    {
        // 工厂方法
        public abstract Food CreateFoodFactory();
    }

    /// <summary>
    /// 番茄炒蛋工厂类
    /// </summary>
    public class TomatoScrambledEggsFactory : Creator
    {
        /// <summary>
        /// 负责创建番茄炒蛋这道菜
        /// </summary>
        /// <returns></returns>
        public override Food CreateFoodFactory()
        {
            return new TomatoScrambledEggs();
        }
    }

    /// <summary>
    /// 土豆肉丝工厂类
    /// </summary>
    public class ShreddedPorkWithPotatoesFactory : Creator
    {
        /// <summary>
        /// 负责创建土豆肉丝这道菜
        /// </summary>
        /// <returns></returns>
        public override Food CreateFoodFactory()
        {
            return new ShreddedPorkWithPotatoes();
        }
    }

    /********************增加肉末茄子这道菜******************************/
    public class Roumoqiezi : Food
    {
        public override string Print()
        {
            return "肉末茄子炒好了";
        }
    }

    public class RoumoqieziFactory : Creator
    {
        public override Food CreateFoodFactory()
        {
            return new Roumoqiezi();
        }
    }
}
