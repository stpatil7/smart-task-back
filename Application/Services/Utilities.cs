using System.Security.Cryptography;
using System.Text;

namespace Libraries.Services
{
    public class Utilities
    {
        public static string Encrypt(string plainText, string? key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            using var aes = Aes.Create();
            var keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            aes.Key = keyBytes;
            aes.IV = new byte[16];

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream();
            using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
            using var sw = new StreamWriter(cs);
            sw.Write(plainText);
            sw.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string cipherText, string key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            using var aes = Aes.Create();
            var keyBytes = Encoding.UTF8.GetBytes(key.PadRight(32).Substring(0, 32));
            aes.Key = keyBytes;
            aes.IV = new byte[16];

            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }

        public static string GenerateOtp()
        {

            using var rng = RandomNumberGenerator.Create();

            byte[] bytes = new byte[4];

            rng.GetBytes(bytes);

            int value = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF;

            int otp = (value % 900000) + 100000;

            return otp.ToString();

        }

    }
}
