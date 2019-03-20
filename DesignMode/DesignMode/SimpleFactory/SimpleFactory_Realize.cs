using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleFactory
{
    public class SimpleFactory_Realize
    {

    }

    /**************************点菜（简单工厂模式）**********************************/

    /// <summary>
    /// 顾客充当客户端，负责调用简单工厂来生产对象
    /// 即客户点菜，厨师（相当于简单工厂）负责烧菜（生产的对象）
    /// </summary>
    public class Customer2
    {
        public void Main()
        {
            // 顾客想点一份番茄炒蛋
            Food2 food1 = FoodSimpleFactory.CreateFood("番茄炒蛋");
            food1.Print();

            // 顾客想点一份土豆炒肉
            Food2 food2 = FoodSimpleFactory.CreateFood("土豆肉丝");
            food2.Print();
        }
    }

    /// <summary>
    /// 菜抽象类
    /// </summary>
    public abstract class Food2
    {
        public abstract string Print();
    }

    /// <summary>
    /// 番茄炒蛋这道菜
    /// </summary>
    public class TomatoScrambledEggs2 : Food2
    {
        public override string Print()
        {
            return "一份番茄炒蛋";
        }
    }

    /// <summary>
    /// 土豆肉丝这道菜
    /// </summary>
    public class ShreddedPorkWithPotatoes2 : Food2
    {
        public override string Print()
        {
            return "一份土豆肉丝";
        }
    }

    /// <summary>
    /// 简单工厂，负责炒菜
    /// </summary>
    public class FoodSimpleFactory
    {
        public static Food2 CreateFood(string type)
        {
            Food2 food = null;
            if (type.Equals("番茄炒蛋"))
            {
                food = new TomatoScrambledEggs2();
            }
            else if (type.Equals("土豆肉丝"))
            {
                food = new ShreddedPorkWithPotatoes2();
            }
            return food;
        }
    }
}
