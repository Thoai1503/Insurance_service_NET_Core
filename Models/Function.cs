using System.Net.Mail;
using System.Net;
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

        public void sendmail(string email,MailMessage mail)
        {
            string from = "minhphat1612@gmail.com";
            MailMessage message = new MailMessage(from, email);
            message.Subject = mail.Subject;
            message.Body = mail.Body;
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    NetworkCredential NetworkCred = new NetworkCredential(from, "xhhe ikpv aazx osjq");
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }
        }

        public static string GenerateCode()
        {
            var random = new Random();
            int a = 9;
            int max = 999999;
            int min = 1;
            string b= random.Next(min, max).ToString();


            return b;
        }
    }
}
