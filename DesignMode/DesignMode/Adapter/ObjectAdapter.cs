using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***********************适配器模式（对象的）*******************************/
namespace Adapter
{
    public class ObjectAdapter
    {
        static void Main()
        {
            // 现在客户可以通过适配器调用2孔插头的方法了
            ThreeHole threeHole = new AnotherPowerAdapter();
            threeHole.Request();
        }
    }

    /// <summary>
    /// 3孔的插座，也就是适配器模式中的目标（Target）角色
    /// </summary>
    public class ThreeHole
    {
        /// <summary>
        /// 客户端需要的方法
        /// </summary>
        public virtual void Request()
        {
            // 可以把一般实现放在这里
        }
    }

    /// <summary>
    /// 两个孔的插头，源角色---需要适配的类
    /// </summary>
    public class AnotherTwoHole
    {
        public string SpecificRequest()
        {
            return "这是两孔插头";
        }
    }

    /// <summary>
    /// 适配器类，这里适配器没有TwoHole类
    /// 而是引用了TwoHole对象，所以是对象的适配器模式的实现
    /// </summary>
    public class AnotherPowerAdapter : ThreeHole
    {
        /// <summary>
        /// 引用2孔插头的实例，从而将客户端与TwoHole联系起来
        /// </summary>
        public AnotherTwoHole twoholeAdapter = new AnotherTwoHole();

        /// <summary>
        /// 实现3孔插座接口方法
        /// </summary>
        public override void Request()
        {
            twoholeAdapter.SpecificRequest();
        }
    }
}
