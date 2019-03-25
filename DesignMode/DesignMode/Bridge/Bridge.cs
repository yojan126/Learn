using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/******************************桥接模式**************************************/
namespace Bridge
{
    /// <summary>
    /// 以电视机遥控器的例子来演示桥接模式
    /// </summary>
    public class Bridge
    {
        static void Main()
        {
            // 创建一个遥控器
            RemoteControl remoteControl = new RemoteControl();

            // 长虹电视机
            remoteControl.Implementor = new Changhong();
            remoteControl.On();
            remoteControl.SetChannel(1);
            remoteControl.Off();

            // 三星电视机
            remoteControl.Implementor = new Samsung();
            remoteControl.On();
            remoteControl.SetChannel(2);
            remoteControl.Off();

        }
    }

    /// <summary>
    /// 抽象概念中的遥控器，扮演抽象化角色
    /// </summary>
    public class RemoteControl
    {
        /// <summary>
        /// 字段
        /// </summary>
        private TV implementor;

        /// <summary>
        /// 属性
        /// </summary>
        public TV Implementor
        {
            get { return implementor; }
            set { implementor = value; }
        }

        /// <summary>
        /// 打开电视，这里抽象类中不提供实现了，而是调用实现类中的实现
        /// </summary>
        public virtual void On()
        {
            implementor.On();
        }

        /// <summary>
        /// 关闭电视
        /// </summary>
        public virtual void Off()
        {
            implementor.Off();
        }

        /// <summary>
        /// 环频道
        /// </summary>
        /// <param name="i"></param>
        public virtual void SetChannel(int i)
        {
            implementor.tuneChannel(i);
        }
    }

    /// <summary>
    /// 电视机，提供抽象方法
    /// </summary>
    public abstract class TV
    {
        public abstract bool On();
        public abstract bool Off();
        public abstract int tuneChannel(int CurrentChannel);
    }

    /// <summary>
    /// 长虹电视机，重写基类的抽象方法
    /// 提供具体的实现
    /// </summary>
    public class Changhong : TV
    {
        public override bool On()
        {
            return true;
        }

        public override bool Off()
        {
            return false;
        }

        public override int tuneChannel(int CurrentChannel)
        {
            return CurrentChannel + 1;
        }
    }

    /// <summary>
    /// 三星电视机，重写基类的抽象方法
    /// </summary>
    public class Samsung : TV
    {
        public override bool On()
        {
            return true;
        }

        public override bool Off()
        {
            return false;
        }

        public override int tuneChannel(int CurrentChannel)
        {
            return CurrentChannel - 1;
        }
    }
}
