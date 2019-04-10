using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State
{
    public class MediatorAndState
    {
        public static void DoThis()
        {
            AbstractCardPartner A = new CardPartenrA();
            AbstractCardPartner B = new CardPartenrB();

            // 初始钱
            A.MoneyCount = 20;
            B.MoneyCount = 20;

            AbstractMediator mediator = new MediatorPartner(new InitState());

            //A,B玩家进入游戏
            mediator.Enter(A);
            mediator.Enter(B);

            // A赢了
            mediator.State = new AWinState(mediator);
            mediator.ChangeCount(5);
            Console.WriteLine("A 现在的钱是：{0}", A.MoneyCount);     // 应该是25
            Console.WriteLine("B 现在的钱是：{0}", B.MoneyCount);     // 应该是15
            Console.ReadLine();

            // B赢了
            mediator.State = new BWinState(mediator);
            mediator.ChangeCount(10);
            Console.WriteLine("A 现在的钱是：{0}", A.MoneyCount);     // 应该是15
            Console.WriteLine("B 现在的钱是：{0}", B.MoneyCount);     // 应该是25
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

        public abstract void ChangeCount(int Count, AbstractMediator mediator);
    }

    /// <summary>
    /// 牌友A类实例
    /// </summary>
    public class CardPartenrA : AbstractCardPartner
    {
        public override void ChangeCount(int Count, AbstractMediator mediator)
        {
            mediator.ChangeCount(Count);
        }
    }

    /// <summary>
    /// 牌友B类实例
    /// </summary>
    public class CardPartenrB : AbstractCardPartner
    {
        public override void ChangeCount(int Count, AbstractMediator mediator)
        {
            mediator.ChangeCount(Count);
        }
    }

    /// <summary>
    /// 抽象状态类
    /// </summary>
    public abstract class StateClass
    {
        protected AbstractMediator mediator;
        public abstract void ChangeCount(int count);
    }

    /// <summary>
    /// A赢状态
    /// </summary>
    public class AWinState : StateClass
    {
        public AWinState(AbstractMediator concreteMediator)
        {
            this.mediator = concreteMediator;
        }

        public override void ChangeCount(int count)
        {
            foreach (AbstractCardPartner cardPartner in mediator.list)
            {
                CardPartenrA a = cardPartner as CardPartenrA;

                if (a != null)
                {
                    a.MoneyCount += count;
                }
                else
                {
                    cardPartner.MoneyCount -= count;
                }
            }
        }
    }

    /// <summary>
    /// B赢状态
    /// </summary>
    public class BWinState : StateClass
    {
        public BWinState(AbstractMediator concreteMediator)
        {
            this.mediator = concreteMediator;
        }

        public override void ChangeCount(int count)
        {
            foreach (AbstractCardPartner cardPartner in mediator.list)
            {
                CardPartenrB b = cardPartner as CardPartenrB;

                if (b != null)
                {
                    b.MoneyCount += count;
                }
                else
                {
                    cardPartner.MoneyCount -= count;
                }
            }
        }
    }

    /// <summary>
    /// 初始化状态类
    /// </summary>
    public class InitState : StateClass
    {
        public InitState()
        {
            Console.WriteLine("游戏才刚刚开始，暂时还没有玩家胜出");
        }

        public override void ChangeCount(int count)
        {
            return;
        }
    }

    /// <summary>
    /// 抽象中介者类
    /// </summary>
    public abstract class AbstractMediator
    {
        public List<AbstractCardPartner> list = new List<AbstractCardPartner>();

        public StateClass State { get; set; }

        public AbstractMediator(StateClass state)
        {
            this.State = state;
        }

        public void Enter(AbstractCardPartner partner)
        {
            list.Add(partner);
        }

        public void Exit(AbstractCardPartner partner)
        {
            list.Remove(partner);
        }

        public void ChangeCount(int Count)
        {
            State.ChangeCount(Count);
        }
    }

    /// <summary>
    /// 具体中介者类
    /// </summary>
    public class MediatorPartner : AbstractMediator
    {
        public MediatorPartner(StateClass initState)
            : base(initState)
        {

        }
    }
}
