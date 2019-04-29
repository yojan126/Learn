using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TranCore
{
    internal class JsonPost
    {
        private const string str_API = @"http://openapi.youdao.com/api?q={0}&from={1}&to={2}&appKey={3}&salt={4}&sign={5}";
        private const string appkey = "3816d13a73df196b";
        private const string key = "gmCCdf8VGH45lcwAauRTMWcoFpvOaNwE";
        private const string lanEn = "EN";
        private const string lanCn = "zh-CHS";

        public bool TranCN2EN(ref Dictionary<string, string> Dic_Info)
        {
            try
            {
                string str_Mes = "";
                foreach (KeyValuePair<string, string> kvp in Dic_Info)
                {
                    if (!string.IsNullOrEmpty(kvp.Key))
                    {
                        str_Mes = str_Mes + "'" + kvp.Key + "'" + "|";
                    }
                }
                str_Mes = str_Mes.Substring(0, str_Mes.Length - 1);

                Random tmpRan = new Random();
                string salt = tmpRan.Next(0, 9).ToString();
                string sign = CreateMD5Hash(appkey + str_Mes + salt + key);


                string str_TranUrl = string.Format(str_API, str_Mes, lanEn, lanCn, appkey, salt, sign);
                string str_JsonResult = GETInterface(str_TranUrl);

                int tmpStart = str_JsonResult.IndexOf("[") + 2;
                int tmpOver = str_JsonResult.IndexOf("]") - 1;
                string[] str_Arry = str_JsonResult.Substring(tmpStart, tmpOver - tmpStart).Split(new char[1] { '|' });
                string[] dicKeys = Dic_Info.Keys.ToArray();
                for (int i = 0; i < str_Arry.Length; i++)
                {
                    Dic_Info[dicKeys[i]] = str_Arry[i].ToString();
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


        CookieContainer cookie = new CookieContainer();

        private string GETInterface(string Url)
        {
            string str_Result = "";
            try
            {
                string serviceAddress = Url;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                str_Result = retString;
                return str_Result;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }

        }

        private string ToJson(object obj)
        {
            string result = "";
            try
            {
                result = JsonConvert.SerializeObject(obj);
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }


            return result;
        }

        private DataTable FromJson(string Json)
        {
            try
            {
                DataTable dt_Json = new DataTable();
                if (Json.Substring(0, 1) != "[")
                {
                    Json = "[" + Json + "]";
                }
                dt_Json = JsonConvert.DeserializeObject<DataTable>(Json);

                return dt_Json;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string CreateMD5Hash(string input)
        {
            // Use input string to calculate MD5 hash
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hashBytes = md5.ComputeHash(inputBytes);

            // Convert the byte array to hexadecimal string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                sb.Append(hashBytes[i].ToString("X2"));
                // To force the hex string to lower-case letters instead of
                // upper-case, use he following line instead:
                // sb.Append(hashBytes[i].ToString("x2")); 
            }
            return sb.ToString();
        }
    }
}
