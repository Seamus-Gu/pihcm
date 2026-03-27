using System.Security.Cryptography;
using System.Text;

namespace Framework.Security
{
    public class CryptoUtil
    {
        public static string BCHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool BCValify(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public static string MD5(string input)
        {
            string salt = "seed_567890Omnbvc_Salt";
            string str = input + salt;
            using (MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(str);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public static string RSAEncrypt(string plainText, string publicKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    // 导入公钥
                    rsa.ImportFromPem(publicKey);

                    // 将明文转换为字节数组
                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);

                    // 加密
                    byte[] encryptedBytes = rsa.Encrypt(plainBytes, false);

                    // 将加密后的字节数组转换为 Base64 字符串
                    return Convert.ToBase64String(encryptedBytes);
                }
                catch (CryptographicException e)
                {
                    throw new Exception($"加密错误{e.Message}");
                }
            }
        }

        public static string RSADecrypt(string cipherText, string privateKey)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    // 导入私钥
                    rsa.ImportFromPem(privateKey);

                    // 将 Base64 编码的密文转换为字节数组
                    byte[] cipherBytes = Convert.FromBase64String(cipherText);

                    // 解密
                    byte[] plainBytes = rsa.Decrypt(cipherBytes, false);

                    // 将字节数组转换为字符串
                    return Encoding.UTF8.GetString(plainBytes);
                }
                catch (CryptographicException e)
                {
                    throw new Exception($"解密错误{e.Message}");
                }
            }
        }
    }
}