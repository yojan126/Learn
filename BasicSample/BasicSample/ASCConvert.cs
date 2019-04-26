using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicSample
{
    class ASCConvert
    {
    }

    public abstract class ConvertASC
    {
        public abstract string Result(string str);
    }

    public class charToASIC : ConvertASC
    {
        public override string Result(string str)
        {
            //return ASCIIEncoding.GetEncoding()


            var (_, a, _) = test1();

            return str;

        }

        public static (string, int, double) test1()
        {
            return ("a", 1, 0);
        }
    }
}
