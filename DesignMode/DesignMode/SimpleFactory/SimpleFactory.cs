using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    public class SimpleFactory
    {

    }

    /*************************自己做饭的情况（没有工厂模式）*****************************/

    /// <summary>
    /// 没有简单工厂之前，客户想吃什么菜只能自己炒
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 烧菜方法
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Food Cook(string type)
        {
            Food food = null;

            // 客户A说：我想吃番茄炒蛋怎么办
            // 客户B说：那你就自己烧啊
            // 客户A说：好吧那我就自己烧吧
            if (type.Equals("番茄炒蛋"))
            {
                food = new TomatoScrambledEggs();
            }

            // 我又想吃土豆肉丝，这个还是得自己烧
            // 我觉得自己做好累，如果有人能帮我做就好了
            else if (type.Equals("土豆肉丝"))
            {
                food = new ShreddedPorkWithPotatoes();
            }
            return food;
        }

        static void Main()
        {
            // 做番茄炒蛋
            Food food1 = Cook("番茄炒蛋");
            food1.Print();

            // 做土豆肉丝
            Food food2 = Cook("土豆肉丝");
            food2.Print();
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
            return "一份番茄炒蛋";
        }
    }

    /// <summary>
    /// 土豆肉丝这个菜
    /// </summary>
    public class ShreddedPorkWithPotatoes : Food
    {
        public override string Print()
        {
            return "一份土豆肉丝";
        }
    }


}
