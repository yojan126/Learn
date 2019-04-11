using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor
{
    public class VistorPattern
    {
        public static void DoThis()
        {
            ObjectStructureClass objectStructureClass = new ObjectStructureClass();
            foreach (ElementClass e in objectStructureClass.Elements)
            {
                // 每个元素接受访问者访问
                e.Accept(new ConcreteVisitor());
            }

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 抽象元素角色
    /// </summary>
    public abstract class ElementClass
    {
        public abstract void Accept(IVisitor visitor);
        public abstract void Print();
    }

    /// <summary>
    /// 具体元素A
    /// </summary>
    public class ElementAClass : ElementClass
    {
        public override void Accept(IVisitor visitor)
        {
            // 调用访问者visit方法
            visitor.Visit(this);
        }

        public override void Print()
        {
            Console.WriteLine("元素A");
        }
    }

    /// <summary>
    /// 具体元素B
    /// </summary>
    public class ElementBClass : ElementClass
    {
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public override void Print()
        {
            Console.WriteLine("元素B");
        }
    }

    /// <summary>
    /// 抽象访问者
    /// </summary>
    public interface IVisitor
    {
        void Visit(ElementAClass a);
        void Visit(ElementBClass b);
    }

    /// <summary>
    /// 具体访问者对象
    /// </summary>
    public class ConcreteVisitor : IVisitor
    {
        // visit方法再去调用元素的Accept方法
        public void Visit(ElementAClass a)
        {
            a.Print();
        }

        public void Visit(ElementBClass b)
        {
            b.Print();
        }
    }

    public class ObjectStructureClass
    {
        private ArrayList elements = new ArrayList();

        public ArrayList Elements
        {
            get { return elements; }
        }

        public ObjectStructureClass()
        {
            Random ran = new Random();
            for (int i = 0; i < 10; i++)
            {
                int RanNum = ran.Next(10);
                if (RanNum < 6)
                {
                    Elements.Add(new ElementAClass());
                }
                else
                {
                    Elements.Add(new ElementBClass());
                }
            }
        }
    }
}
