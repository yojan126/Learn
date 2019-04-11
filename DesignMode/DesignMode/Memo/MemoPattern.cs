using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memo
{
    public class MemoPattern
    {
        public static void DoThis()
        {
            List<ContactPerson> people = new List<ContactPerson>()
            {
                new ContactPerson(){Name="Tom",MobileNum="11111"},
                new ContactPerson(){Name="Jack",MobileNum="22222"},
                new ContactPerson(){Name="Harry",MobileNum="33333"}
            };

            MobileOwner mobileOwner = new MobileOwner(people);
            mobileOwner.Show();

            // 创建备忘录并保存备忘录对象
            Caretaker caretaker = new Caretaker();
            caretaker.contactMemo = mobileOwner.CreateMemo();

            // 更改发起人联系列表
            Console.WriteLine("----移除最后一个联系人----");
            mobileOwner.ContactPeople.RemoveAt(2);
            mobileOwner.Show();

            // 恢复到初始状态
            Console.WriteLine("----恢复联系人列表----");
            mobileOwner.RestoreMemo(caretaker.contactMemo);
            mobileOwner.Show();

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 联系人
    /// </summary>
    public class ContactPerson
    {
        public string Name { get; set; }
        public string MobileNum { get; set; }
    }

    /// <summary>
    /// 发起人
    /// </summary>
    public class MobileOwner
    {
        public List<ContactPerson> ContactPeople { get; set; }

        public MobileOwner(List<ContactPerson> contactPeople)
        {
            this.ContactPeople = contactPeople;
        }

        public ContactMemo CreateMemo()
        {
            // 这里也应该传递深拷贝，new List方式传递的是浅拷贝，
            // 因为ContactPerson类中都是string类型,所以这里new list方式对ContactPerson对象执行了深拷贝
            // 如果ContactPerson包括非string的引用类型就会有问题，所以这里也应该用序列化传递深拷贝
            return new ContactMemo(new List<ContactPerson>(this.ContactPeople));
        }

        /// <summary>
        /// 将备忘录中的数据备份导入到联系人列表中
        /// </summary>
        /// <param name="memo"></param>
        public void RestoreMemo(ContactMemo memo)
        {
            // 下面这种方式是错误的，因为这样传递的是引用，
            // 则删除一次可以恢复，但恢复之后再删除的话就恢复不了.
            // 所以应该传递contactPersonBack的深拷贝，深拷贝可以使用序列化来完成
            this.ContactPeople = memo.contactPeopleBack;
        }

        public void Show()
        {
            Console.WriteLine("联系人列表中有{0}个人，他们是：", ContactPeople.Count());
            foreach (ContactPerson cp in ContactPeople)
            {
                Console.WriteLine("姓名：{0} 号码为：{1}", cp.Name, cp.MobileNum);
            }
        }
    }

    /// <summary>
    /// 备忘录
    /// </summary>
    public class ContactMemo
    {
        /// <summary>
        /// 保存发起人的内部状态
        /// </summary>
        public List<ContactPerson> contactPeopleBack;

        public ContactMemo(List<ContactPerson> contactPeople)
        {
            contactPeopleBack = contactPeople;
        }
    }

    /// <summary>
    /// 管理角色
    /// </summary>
    public class Caretaker
    {
        public ContactMemo contactMemo { get; set; }
    }
}
