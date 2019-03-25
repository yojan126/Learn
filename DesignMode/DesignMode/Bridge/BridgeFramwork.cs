using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/***********************桥接模式（三层架构原始模型）**************************/
namespace Bridge
{
    public class BridgeFramwork
    {
    }


    public class Client
    {
        static void Main()
        {
            BusinessObject customers = new CustomersBusinessObject("shanghai");
            customers.DataAccess = new CustomersDataAccess();

            customers.Add("Jame");
            customers.GetAll();
            customers.Delete("Tom");
            customers.GetAll();
            customers.Update("Harry");
            customers.Get(2);
        }
    }

    /// <summary>
    /// BLL层
    /// </summary>
    public class BusinessObject
    {
        /// <summary>
        /// 字段
        /// </summary>
        private DataAccess dataAccess;
        private string city;

        public BusinessObject(string city)
        {
            this.city = city;
        }

        /// <summary>
        /// 属性
        /// </summary>
        public DataAccess DataAccess
        {
            get { return dataAccess; }
            set { dataAccess = value; }
        }

        /// <summary>
        /// 方法
        /// </summary>
        /// <param name="name"></param>
        public virtual void Add(string name)
        {
            DataAccess.AddRecord(name);
        }

        public virtual void Delete(string name)
        {
            DataAccess.DeleteRecord(name);
        }

        public virtual void Update(string name)
        {
            DataAccess.UpdateRecord(name);
        }

        public virtual void Get(int index)
        {
            dataAccess.GetRecord(index);
        }

        public virtual void GetAll()
        {
            DataAccess.ShowAllRecords();
        }
    }

    public class CustomersBusinessObject : BusinessObject
    {
        public CustomersBusinessObject(string city)
            : base(city) { }

        /// <summary>
        /// 重写方法
        /// </summary>
        public override void GetAll()
        {
            base.GetAll();
        }
    }

    /// <summary>
    /// 相当于三层架构中的数据访问层（DAL）
    /// </summary>
    public abstract class DataAccess
    {
        // 对记录的增删改查操作
        public abstract void AddRecord(string name);
        public abstract void DeleteRecord(string name);
        public abstract void UpdateRecord(string name);
        public abstract string GetRecord(int index);
        public abstract void ShowAllRecords();
    }

    public class CustomersDataAccess : DataAccess
    {
        /// <summary>
        /// 字段
        /// </summary>
        private List<string> customers = new List<string>();

        public CustomersDataAccess()
        {
            // 实际业务中从数据库中读取数据再填充列表
            customers.Add("Tom");
            customers.Add("Jack");
            customers.Add("Harry");
        }

        /// <summary>
        /// 重写方法
        /// </summary>
        /// <param name="name"></param>
        public override void AddRecord(string name)
        {
            customers.Add(name);
        }

        public override void DeleteRecord(string name)
        {
            customers.Remove(name);
        }

        public override void UpdateRecord(string name)
        {
            customers[0] = name;
        }

        public override string GetRecord(int index)
        {
            return customers[index];
        }

        public override void ShowAllRecords()
        {
            List<string> lst = new List<string>();
            foreach (string str in customers)
            {
                lst.Add(str);
            }
        }
    }
}
