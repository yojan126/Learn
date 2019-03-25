using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    class Program
    {
        /// <summary>
        /// 通过一些简单图形以及一些复杂图形构建图形树来演示组合模式
        /// 客户端调用
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ComplexGraphics complexGraphics = new ComplexGraphics("一个复杂图形和两条线段组成的复杂图形");
            complexGraphics.Add(new Line("线段A"));
            ComplexGraphics CompositeCG = new ComplexGraphics("一个圆和一条线组成的复杂图形");
            CompositeCG.Add(new Circle("圆"));
            CompositeCG.Add(new Line("线段B"));
            complexGraphics.Add(CompositeCG);
            Line l = new Line("线段C");
            complexGraphics.Add(l);

            // 显示复杂图形的画法
            Console.WriteLine("复杂图形的绘制如下");
            Console.WriteLine("------------------------");
            complexGraphics.Draw();
            Console.WriteLine("复杂图形绘制完成");
            Console.WriteLine("------------------------");
            Console.WriteLine();

            // 移除一个组建再显示复杂图形的画法
            complexGraphics.Remove(l);
            Console.WriteLine("移除线段C后，复杂图形的绘制如下");
            Console.WriteLine("------------------------");
            complexGraphics.Draw();
            Console.WriteLine("复杂图形绘制完成");
            Console.WriteLine("------------------------");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 图形抽象类
    /// </summary>
    public abstract class Graphics
    {
        public string Name { get; set; }
        public Graphics(string name)
        {
            this.Name = name;
        }

        public abstract void Draw();
        public abstract void Add(Graphics g);
        public abstract void Remove(Graphics g);
    }

    /// <summary>
    /// 简单图形类--线
    /// </summary>
    public class Line : Graphics
    {
        public Line(string name)
            : base(name)
        {

        }

        /// <summary>
        /// 重写父类抽象方法
        /// </summary>
        public override void Draw()
        {
            Console.WriteLine("画  " + Name);
        }

        /// <summary>
        /// 因为简单图形不存在添加或移除其它图形，所以简单图形Add或Remove方法没有任何意义
        /// 如果客户端调用了简单图形的Add或Remove方法将会在运行时抛出异常
        /// 我们可以在客户端捕获该类移除并处理
        /// </summary>
        /// <param name="g"></param>
        public override void Add(Graphics g)
        {
            throw new Exception("不能向简单图形Line添加其它图形");
        }

        public override void Remove(Graphics g)
        {
            throw new Exception("不能向简单图形Line移除其它图形");
        }
    }

    /// <summary>
    /// 简单图形类--圆
    /// </summary>
    public class Circle : Graphics
    {
        public Circle(string name)
            :base(name)
        {

        }

        /// <summary>
        /// 重写父类抽象方法
        /// </summary>
        public override void Draw()
        {
            Console.WriteLine("画  " + Name);
        }

        public override void Add(Graphics g)
        {
            throw new Exception("不能向简单图形Circle添加其它图形");
        }

        public override void Remove(Graphics g)
        {
            throw new Exception("不能向简单图形Circle移除其它图形");
        }
    }

    /// <summary>
    /// 复杂图形，由一些简单图形组成，这里假设该复杂图形由一个圆两条线组成的复杂图形
    /// </summary>
    public class ComplexGraphics : Graphics
    {
        private List<Graphics> complexGraphicsList = new List<Graphics>();

        public ComplexGraphics(string name)
            :base(name)
        {

        }

        /// <summary>
        /// 复杂图形的画法
        /// </summary>
        public override void Draw()
        {
            foreach (Graphics g in complexGraphicsList)
            {
                g.Draw();
            }
        }

        public override void Add(Graphics g)
        {
            complexGraphicsList.Add(g);
        }

        public override void Remove(Graphics g)
        {
            complexGraphicsList.Remove(g);
        }
    }
}
