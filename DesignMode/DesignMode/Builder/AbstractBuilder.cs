using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    /****************抽象建造者模式（即是指挥者又是建造者）*******************/
    /// <summary>
    /// 建造者抽象
    /// </summary>
    public abstract class AbstractBuilder
    {
        // 设为protected，制造细节对外不可见，做到封装
        protected abstract void Part1();
        protected abstract void Part2();
        // 只暴露获取产品的方法
        public abstract Product GetProduct();
    }

    /// <summary>
    /// 产品
    /// </summary>
    public class Product
    {
        private List<string> lst = new List<string>();

        // 设为internal，这样Add方法对当前命名空间外不可见，做到封装
        internal void Add(string part)
        {
            lst.Add(part);
        }
        public string Print()
        {
            return "已装配完成";
        }
    }

    public class ABuilder : AbstractBuilder
    {
        private Product Product
        {
            get; set;
        }

        public ABuilder()
        {
            Product = new Product();
        }

        protected override void Part1()
        {
            Product.Add("part1");
        }

        protected override void Part2()
        {
            Product.Add("part2");
        }

        public override Product GetProduct()
        {
            Part1();
            Part2();
            return Product;
        }
    }

    public class Programe
    {
        static void Main()
        {
            AbstractBuilder abstractBuilder = new ABuilder();
            Product product = abstractBuilder.GetProduct();
            product.Print();
        }
    }
}
