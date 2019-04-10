using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***********************************中介者模式*****************************************/
namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            // 非中介者模式
            // NoMediator.DoThis();

            // 中介者模式
            MediatorReal.DoThis();
        }
    }
}
