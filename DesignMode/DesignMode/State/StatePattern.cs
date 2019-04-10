using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State
{
    public class StatePattern
    {
        public static void DoThis()
        {
            // 开一个新账户
            Account account = new Account("John");

            // 进行交易
            // 存钱
            account.Deposit(200.00);
            account.Deposit(500.00);
            account.Deposit(20000.00);
            Console.ReadLine();

            // 付利息
            account.PayInterest();
            Console.ReadLine();

            // 取钱
            account.Withdraw(20000.00);
            account.Withdraw(10000.00);

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 账户类
    /// </summary>
    public class Account
    {
        public State State { get; set; }
        public string Owner { get; set; }
        public Account(string owner)
        {
            this.Owner = owner;
            this.State = new SilverState(0.00, this);
        }

        public double Balance { get { return State.Balance; } }     // 余额

        public void Deposit(double amount)
        {
            State.Deposit(amount);
            Console.WriteLine("存款金额为 {0:C}", amount);
            Console.WriteLine("账户余额为 {0:C}", this.Balance);
            Console.WriteLine("账户状态为 {0}", this.State.GetType().Name);
            Console.WriteLine();
        }

        public void Withdraw(double amount)
        {
            State.Withdraw(amount);
            Console.WriteLine("取款金额为 {0:C}", amount);
            Console.WriteLine("账户余额为 {0:C}", this.Balance);
            Console.WriteLine("账户状态为 {0}", this.State.GetType().Name);
            Console.WriteLine();
        }

        public void PayInterest()
        {
            State.PayInterest();
            Console.WriteLine("Interest Paid");
            Console.WriteLine("账户余额为 {0:C}", this.Balance);
            Console.WriteLine("账户状态为 {0}", this.State.GetType().Name);
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 抽象状态类
    /// </summary>
    public abstract class State
    {
        public Account Account { get; set; }        // 账户
        public double Balance { get; set; }         // 余额
        public double Interest { get; set; }        // 利率
        public double LowerLimit { get; set; }      // 下限
        public double UpperLimit { get; set; }      // 上限

        public abstract void Deposit(double amount);    // 存钱
        public abstract void Withdraw(double amount);   // 取钱
        public abstract void PayInterest();             // 获得利息
    }

    /// <summary>
    /// red state 意味着account透支了
    /// </summary>
    public class RedState : State
    {
        public RedState(State state)
        {
            // Initialize
            this.Balance = state.Balance;
            this.Account = state.Account;
            Interest = 0.00;
            LowerLimit = -100.00;
            UpperLimit = 0.00;
        }

        public override void Deposit(double amount)
        {
            Balance = +amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            Console.WriteLine("没有钱可取了");
        }

        public override void PayInterest()
        {
            // 没有利息
        }
        
        private void StateChangeCheck()
        {
            if (Balance > UpperLimit)
            {
                Account.State = new SilverState(this);
            }
            else if (Balance > 1000.00)
            {
                Account.State = new GoldState(this);
            }
        }
    }

    /// <summary>
    /// 普通账户
    /// </summary>
    public class SilverState : State
    {
        public SilverState(State state)
            : this(state.Balance, state.Account)
        {

        }

        public SilverState(double balance, Account account)
        {
            this.Balance = balance;
            this.Account = account;
            Interest = 0.00;
            LowerLimit = 0.00;
            UpperLimit = 1000.00;
        }

        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        public override void Withdraw(double amount)
        {
            Balance -= amount;
            StateChangeCheck();
        }

        public override void PayInterest()
        {
            Balance += Interest * Balance;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (Balance < LowerLimit)
            {
                Account.State = new RedState(this);
            }
            if (Balance > UpperLimit)
            {
                Account.State = new GoldState(this);
            }
        }
    }

    /// <summary>
    /// 高级账户
    /// </summary>
    public class GoldState : State
    {
        public GoldState(State state)
        {
            this.Balance = state.Balance;
            this.Account = state.Account;
            Interest = 0.05;
            LowerLimit = 1000.00;
            UpperLimit = 1000000.00;
        }

        /// <summary>
        /// 存钱
        /// </summary>
        /// <param name="amount"></param>
        public override void Deposit(double amount)
        {
            Balance += amount;
            StateChangeCheck();
        }

        /// <summary>
        /// 取钱
        /// </summary>
        /// <param name="amount"></param>
        public override void Withdraw(double amount)
        {
            Balance -= amount;
            StateChangeCheck();
        }

        /// <summary>
        /// 利息
        /// </summary>
        public override void PayInterest()
        {
            Balance += Interest * Balance;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (Balance < 0.00)
            {
                Account.State = new RedState(this);
            }
            else if (Balance < LowerLimit)
            {
                Account.State = new SilverState(this);
            }
        }
    }
}
