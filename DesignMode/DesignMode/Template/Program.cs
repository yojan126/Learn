using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*********************模板方法模式**************************/
namespace Template
{
    /// <summary>
    /// 客户端调用
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // 创建一个菠菜实例并调用模板
            Vegetable spinach = new Spinach();
            spinach.CookVegetable();
            Console.WriteLine("\n");

            // 创建一个大白菜实例并调用模板
            Vegetable chineseCabbage = new ChineseCabbage();
            chineseCabbage.CookVegetable();
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 创建炒蔬菜模板抽象类,包含流程实例
    /// </summary>
    public abstract class Vegetable
    {
        /// <summary>
        /// 模板方法，不要把模板方法定义为virtual或abstract方法，避免子类重写，防止更改流程的执行顺序
        /// </summary>
        public void CookVegetable()
        {
            Console.WriteLine("炒蔬菜的一般做法");
            this.pourOil();
            this.HeatOil();
            this.pourVegetable();
            this.stir_fry();
        }

        /// <summary>
        /// 第一步倒油
        /// </summary>
        public void pourOil()
        {
            Console.WriteLine("倒油");
        }

        /// <summary>
        /// 第二步热油
        /// </summary>
        public void HeatOil()
        {
            Console.WriteLine("把油烧热");
        }

        /// <summary>
        /// 第三步油热了之后倒蔬菜下去，具体哪种蔬菜由子类决定
        /// </summary>
        public abstract void pourVegetable();

        /// <summary>
        /// 第四步翻炒蔬菜
        /// </summary>
        public void stir_fry()
        {
            Console.WriteLine("翻炒");
        }
    }

    /// <summary>
    /// 菠菜
    /// </summary>
    public class Spinach : Vegetable
    {
        public override void pourVegetable()
        {
            Console.WriteLine("倒菠菜进锅中");
        }
    }

    /// <summary>
    /// 大白菜
    /// </summary>
    public class ChineseCabbage : Vegetable
    {
        public override void pourVegetable()
        {
            Console.WriteLine("倒大白菜进锅中");
        }
    }
}
