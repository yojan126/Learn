using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor
{
    public class NotVisitor
    {
        public static void DoThis()
        {
            ObjectStructure objectStructure = new ObjectStructure();
            foreach (Element e in objectStructure.Elements)
            {
                e.Print();
            }
            Console.ReadLine();
        }

    }

    /// <summary>
    /// 抽象元素角色
    /// </summary>
    public abstract class Element
    {
        public abstract void Print();
    }

    /// <summary>
    /// 具体元素A
    /// </summary>
    public class ElementA : Element
    {
        public override void Print()
        {
            Console.WriteLine("元素A");
        }
    }

    /// <summary>
    /// 具体元素B
    /// </summary>
    public class ElementB : Element
    {
        public override void Print()
        {
            Console.WriteLine("元素B");
        }
    }

    /// <summary>
    /// 对象结构
    /// </summary>
    public class ObjectStructure
    {
        private ArrayList elements = new ArrayList();

        public ArrayList Elements
        {
            get { return elements; }
        }

        public ObjectStructure()
        {
            Random r = new Random();
            for (int i = 0; i < 10; i++)
            {
                int ranNum = r.Next(10);
                if (ranNum > 5)
                {
                    elements.Add(new ElementA());
                }
                else
                {
                    elements.Add(new ElementB());
                }
            }
        }
    }
}
