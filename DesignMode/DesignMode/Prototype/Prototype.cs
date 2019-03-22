using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    /***************************原型模式*************************************/
    public class Prototype
    {
        static void Main()
        {
            // 孙悟空原型
            MonkeyKingPrototype monkeyKingPrototype = new ConcretePrototype("MonkeyKing");

            // 变一个
            MonkeyKingPrototype cloneMonkeyKing1 = monkeyKingPrototype.Clone() as ConcretePrototype;

            // 变两个
            MonkeyKingPrototype cloneMonkeyKing2 = monkeyKingPrototype.Clone() as ConcretePrototype;

        }
    }

    /// <summary>
    /// 孙悟空原型
    /// </summary>
    public abstract class MonkeyKingPrototype
    {
        public string ID { get; set; }
        public MonkeyKingPrototype(string id)
        {
            this.ID = id;
        }

        /// <summary>
        /// 克隆方法
        /// </summary>
        /// <returns></returns>
        public abstract MonkeyKingPrototype Clone();
    }

    /// <summary>
    /// 创建具体原型
    /// </summary>
    public class ConcretePrototype : MonkeyKingPrototype
    {
        public ConcretePrototype(string id) : base(id)
        {

        }

        /// <summary>
        /// 浅拷贝
        /// </summary>
        /// <returns></returns>
        public override MonkeyKingPrototype Clone()
        {
            // 调用 MemberwiseClone 方法实现的是浅拷贝，另外还有深拷贝
            return (MonkeyKingPrototype)this.MemberwiseClone();
        }
    }
}
