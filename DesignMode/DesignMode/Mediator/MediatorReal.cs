using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class MediatorReal
    {
        public static void DoThis()
        {
            AbstractCardPartnerClass A = new PartnerAClass();
            AbstractCardPartnerClass B = new PartnerBClass();
            // 初始
            A.MoneyCount = 20;
            B.MoneyCount = 20;

            AbstractMediator mediator = new MediatorPartner(A, B);

            // A赢了
            A.ChangeCount(5, mediator);
            Console.WriteLine("A 现在的钱是{0}", A.MoneyCount);
            Console.WriteLine("B 现在的钱是{0}", B.MoneyCount);
            Console.ReadLine();

            // B赢了
            B.ChangeCount(10, mediator);
            Console.WriteLine("A 现在的钱是{0}", A.MoneyCount);
            Console.WriteLine("B 现在的钱是{0}", B.MoneyCount);
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 抽象牌友类
    /// </summary>
    public abstract class AbstractCardPartnerClass
    {
        public int MoneyCount { get; set; }

        public AbstractCardPartnerClass()
        {
            MoneyCount = 0;
        }

        public abstract void ChangeCount(int Count, AbstractMediator Mediator);
    }

    /// <summary>
    /// 牌友A类
    /// </summary>
    public class PartnerAClass : AbstractCardPartnerClass
    {
        /// <summary>
        /// 依赖于抽象中介者对象
        /// </summary>
        /// <param name="Count"></param>
        /// <param name="Mediator"></param>
        public override void ChangeCount(int Count, AbstractMediator Mediator)
        {
            Mediator.AWin(Count);
        }
    }

    /// <summary>
    /// 牌友B类
    /// </summary>
    public class PartnerBClass : AbstractCardPartnerClass
    {
        /// <summary>
        /// 依赖于抽象中介者对象
        /// </summary>
        /// <param name="Count"></param>
        /// <param name="Mediator"></param>
        public override void ChangeCount(int Count, AbstractMediator Mediator)
        {
            Mediator.BWin(Count);
        }
    }

    /// <summary>
    /// 抽象中介者类
    /// </summary>
    public abstract class AbstractMediator
    {
        protected AbstractCardPartnerClass A;
        protected AbstractCardPartnerClass B;
        public AbstractMediator(AbstractCardPartnerClass a, AbstractCardPartnerClass b)
        {
            A = a;
            B = b;
        }

        public abstract void AWin(int Count);
        public abstract void BWin(int Count);
    }

    /// <summary>
    /// 具体中介者类
    /// </summary>
    public class MediatorPartner : AbstractMediator
    {
        public MediatorPartner(AbstractCardPartnerClass a, AbstractCardPartnerClass b)
            : base(a, b)
        {
        }

        public override void AWin(int Count)
        {
            A.MoneyCount += Count;
            B.MoneyCount -= Count;
        }

        public override void BWin(int Count)
        {
            B.MoneyCount += Count;
            A.MoneyCount -= Count;
        }
    }
}
