using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Duty
{
    public class ChainofResponsibility
    {
        public static void DoThis()
        {
            PurchaseRequest request1 = new PurchaseRequest(500.00, "Goods A");
            PurchaseRequest request2 = new PurchaseRequest(15000.00, "Goods B");
            PurchaseRequest request3 = new PurchaseRequest(230000.00, "Goods C");

            Approver manager = new Manager("Tom");
            Approver vp = new VicePresident("Jack");
            Approver coo = new President("Tony");

            manager.NextApprover = vp;
            vp.NextApprover = coo;

            manager.ProcessRequest(request1);
            manager.ProcessRequest(request2);
            manager.ProcessRequest(request3);
            Console.ReadLine();
        }
    }

    /// <summary>
    /// 采购请求
    /// </summary>
    public class PurchaseRequest
    {
        // 金额
        public double Amount { get; set; }
        // 产品名字
        public string ProductName { get; set; }
        public PurchaseRequest(double amount, string productName)
        {
            this.Amount = amount;
            this.ProductName = productName;
        }
    }

    /// <summary>
    /// 抽象审批人
    /// </summary>
    public abstract class Approver
    {
        public Approver NextApprover { get; set; }
        public string Name { get; set; }
        public Approver(string name)
        {
            this.Name = name;
        }
        public abstract void ProcessRequest(PurchaseRequest request);
    }

    /// <summary>
    /// 经理类实例
    /// </summary>
    public class Manager : Approver
    {
        public Manager(string name)
            : base(name)
        {

        }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 10000.00)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    /// <summary>
    /// 副总类实例
    /// </summary>
    public class VicePresident : Approver
    {
        public VicePresident(string name)
            : base(name)
        {

        }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 25000.00)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    /// <summary>
    /// 总经理类实例
    /// </summary>
    public class President : Approver
    {
        public President(string name)
            : base(name)
        {

        }

        public override void ProcessRequest(PurchaseRequest request)
        {
            if (request.Amount < 100000.00)
            {
                Console.WriteLine("{0}-{1} approved the request of purshing {2}", this, Name, request.ProductName);
            }
            else
            {
                Console.WriteLine("请求需要组织一个会议讨论");
            }
        }
    }
}
