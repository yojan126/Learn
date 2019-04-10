using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    public class Strategy
    {
        public static void DoThis()
        {
            // 个人所得税方式
            InterestOperation operation = new InterestOperation(new PersonalTaxStrategy());
            Console.WriteLine("个人支付的税为：{0}", operation.GetTax(5000.00));

            operation = new InterestOperation(new EnterpriseTaxStrategy());
            Console.WriteLine("企业支付的税为：{0}", operation.GetTax(50000.00));

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 所得税计算策略
    /// </summary>
    public interface ITaxStrategy
    {
        double CalculateTax(double income);
    }

    /// <summary>
    /// 个人所得税计算策略
    /// </summary>
    public class PersonalTaxStrategy : ITaxStrategy
    {
        public double CalculateTax(double income)
        {
            return income * 0.12;
        }
    }

    /// <summary>
    /// 企业所得税计算策略
    /// </summary>
    public class EnterpriseTaxStrategy : ITaxStrategy
    {
        public double CalculateTax(double income)
        {
            return (income - 3500) > 0 ? (income - 3000) * 0.045 : 0.00;
        }
    }

    /// <summary>
    /// 利益操作
    /// </summary>
    public class InterestOperation
    {
        private ITaxStrategy m_strategy;
        public InterestOperation(ITaxStrategy taxStrategy)
        {
            this.m_strategy = taxStrategy;
        }

        public double GetTax(double income)
        {
            return m_strategy.CalculateTax(income);
        }
    }
}
