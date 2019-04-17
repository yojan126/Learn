using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncTest
{
    public class logButton : Button
    {
        public delegate void ObserverEventHandler(Button button);       // 创建委托
        public event ObserverEventHandler ObserverEvent;                // 创建委托事件

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (ObserverEvent == null)                                  // 当委托事件为null时
            {
                new ObserverClass(this);                                // 实例化观察者类，观察者类中会添加需要进行委托的方法
            }                                                           // 如此定义的好处是对于被观察者并不知道观察者进行了哪些动作，并且方便扩展
            ObserverEvent.Invoke(this);
            //if (this.Parent != null)
            //{
            //    foreach (Control c in this.Parent.Controls)
            //    {
            //        if (c.Name == "textBox1")
            //        {
            //            ((TextBox)c).Text += this.Name + "\r\n";
            //        }
            //    }
            //}
        }
    }

    public class ObserverClass
    {
        public ObserverClass(logButton logButton)
        {
            logButton.ObserverEvent += new logButton.ObserverEventHandler(DoSomething);     // 观察者类往委托事件中添加委托方法
        }

        private void DoSomething(Button button)
        {
            Logging.listAdd(button);
        }
    }

    public class Logging
    {
        public static List<string> lstBtnName = new List<string>();                         

        public Logging()
        {

        }

        public static void listAdd(Button button)      // 静态方法
        {
            lstBtnName.Add(button.Tag.ToString());            // 不同实例化的logButton类在被单击后都会向logList中添加被点击button的name记录
        }
    }
}
