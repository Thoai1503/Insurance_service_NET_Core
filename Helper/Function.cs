using System.Security.Cryptography;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Insurance_agency.Helper
{
    public class Function
    {
        public static string MD5Hash(string text)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder hashsb = new StringBuilder();
                foreach (byte b in hash)
                {
                    hashsb.Append(b.ToString("X2"));
                }
                return hashsb.ToString();
            }
        }
        public static int limitnum(int num, int length)
        {
            int result = num;
            for (int i = 0; i < length; i++)
            {
                result += result * 10;
            }

            return result;
        }

        public static string GenerateCode()
        {
            var random = new Random();
            int a = 9;
            int max = 999999;
            int min = 1;
            string b = random.Next(min, max).ToString();


            return b;
        }
        public static string getMonth(DateTime date)
        {
            var a = string.Empty;
            switch (date.Month)
            {
                case 1: a = "Jan"; break;
                case 2: a = "Feb"; break;
                case 3: a = "Mar"; break;
                case 4: a = "Apr"; break;
                case 5: a = "May"; break;
                case 6: a = "Jun"; break;
                case 7: a = "Jul"; break;
                case 8: a = "Aug"; break;
                case 9: a = "Sep"; break;
                case 10: a = "Oct"; break;
                case 11: a = "Nov"; break;
                case 12: a = "Dec"; break;
            }
            return a;
        }
    }
}