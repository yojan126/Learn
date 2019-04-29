using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TranCore
{
    public class Common
    {

        public bool GetMes(ref Dictionary<string, string> Dic_Info)
        {
            try
            {
                JsonPost jp = new JsonPost();
                if (!jp.TranCN2EN(ref Dic_Info))
                {
                    throw new Exception(Dic_Info.Values.ToArray()[0].ToString());
                }

                return true;
            }
            catch (Exception ex)
            {
                Dic_Info.Clear();
                Dic_Info.Add("ERROR", ex.ToString());
                return false;
            }
        }
    }
}
