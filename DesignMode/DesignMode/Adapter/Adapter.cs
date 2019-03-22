using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/********************适配器模式（类的）*************************/

/// <summary>
/// 这里以插座和插头的例子来诠释适配器模式
/// 现在我们买的插头是两孔的，但我们的插座是三孔的
/// 这时我们想把电器插在插座上就需要一个适配器
/// </summary>
namespace Adapter
{

    /// <summary>
    /// 客户端，客户想要把2孔的插头转变成3孔的插头，这个转变交给适配器就好了
    /// 既然适配器要完成这个功能，那么它就必须具备2孔插头和3孔插头的特征
    /// </summary>
    public class Adapter
    {
        static void Main()
        {
            // 现在客户可以调用3孔插座的接口来实现2孔插头的功能了
            IThreeHole threeHole = new PowerAdapter();
            threeHole.Request();
        }
    }

    /// <summary>
    /// 3个孔的插座，也就是适配器模式中的目标角色
    /// </summary>
    public interface IThreeHole
    {
        void Request();
    }

    /// <summary>
    /// 2个孔的插头，源角色需要适配的类
    /// </summary>
    public abstract class TwoHole
    {
        public string SpecificRequest()
        {
            return "我是两孔的插头";
        }
    }

    /// <summary>
    /// 适配器类，接口要放在类的后面
    /// 适配器提供了3孔插座的行为，但其本质是调用了2孔插头的方法
    /// </summary>
    public class PowerAdapter : TwoHole, IThreeHole
    {
        /// <summary>
        /// 实现3孔插座接口方法
        /// </summary>
        public void Request()
        {
            // 调用2孔插头方法
            this.SpecificRequest();
        }
    }
}
