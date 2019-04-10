using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    public class NoMediator
    {
        /// <summary>
        /// A B两人打牌
        /// </summary>
        public static void DoThis()
        {
            AbstractCardPartner A = new PartnerA();
            A.MoneyCount = 20;
            AbstractCardPartner B = new PartnerB();
            B.MoneyCount = 20;

            // A赢了B的钱就减少
            A.ChangeCount(5, B);
            Console.WriteLine("A 现在的钱是：{0}", A.MoneyCount); // 应该是25
            Console.WriteLine("B 现在的钱是：{0}", B.MoneyCount); // 应该是15
            Console.ReadLine();

            // B赢了A的钱也减少
            B.ChangeCount(10, A);
            Console.WriteLine("A 现在的钱是：{0}", A.MoneyCount); // 应该是15
            Console.WriteLine("B 现在的钱是：{0}", B.MoneyCount); // 应该是25
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 抽象牌友类
    /// </summary>
    public abstract class AbstractCardPartner
    {
        public int MoneyCount { get; set; }

        public AbstractCardPartner()
        {
            MoneyCount = 0;
        }

        public abstract void ChangeCount(int Count, AbstractCardPartner other);
    }

    /// <summary>
    /// 牌友A类
    /// </summary>
    public class PartnerA : AbstractCardPartner
    {
        public override void ChangeCount(int Count, AbstractCardPartner other)
        {
            this.MoneyCount += Count;
            other.MoneyCount -= Count;
        }
    }

    /// <summary>
    /// 牌友B类
    /// </summary>
    public class PartnerB : AbstractCardPartner
    {
        public override void ChangeCount(int Count, AbstractCardPartner other)
        {
            this.MoneyCount += Count;
            other.MoneyCount -= Count;
        }
    }


}
