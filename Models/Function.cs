using System.Security.Cryptography;
using System.Text;

namespace Insurance_agency.Models
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

        public static string GenerateCode(int length = 6)
        {
            var random = new Random();
            int a = 9;
            int max = limitnum(9, length);
            int min = limitnum(1, length);
            random.Next(min, max);


            return random.ToString();
        }
    }
}
