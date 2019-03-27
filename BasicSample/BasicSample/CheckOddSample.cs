using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSample
{
    class CheckOddSample
    {
        public static void CheckOdd()
        {
            Random random = new Random();
            int i;
            while (1 == 1)
            {
                i = random.Next(1, 999);
                if (Convert.ToBoolean(i & 1))
                {
                    Console.WriteLine("{0} 是奇数", i.ToString());
                }
                else
                {
                    Console.WriteLine("{0} 是偶数", i.ToString());
                }

                Console.ReadLine();
            }
        }
    }
}
