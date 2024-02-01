using System.Security.Cryptography;
using System.Text;
using BSC.Core.Common.Contracts;
using BSC.Core.Common.Security;
using BSC.Core.Common.Settings;

namespace BSC.Web.Infrastructure.Services
{
    public class CryptProvider : ICryptProvider
    {
        private string _OriginalKey;
        private string _OriginalSalt;

        public CryptProvider(SecuritySetting securitySetting)
        {
            _OriginalKey = securitySetting.Key;
            _OriginalSalt = securitySetting.Salt;
        }

        public string Decrypt(string str)
        {
            AesCrypt crypt = new AesCrypt(_OriginalKey, _OriginalSalt);

            return crypt.Decrypt(str);
        }

        public string Decrypt(string str, string key, string salt)
        {
            //The private properties of _Crypt where not refreshing, needed to create new instace
            AesCrypt enc = new AesCrypt(key, salt);

            return enc.Decrypt(str);
        }

        public string DecryptWithEncode(string str)
        {
            AesCrypt crypt = new AesCrypt(_OriginalKey, _OriginalSalt);

            return crypt.Decrypt(System.Web.HttpUtility.UrlDecode(str));
        }

        public T DecryptWithEncode<T>(string str)
        {
            AesCrypt crypt = new AesCrypt(_OriginalKey, _OriginalSalt);

            return (T)Convert.ChangeType(
                System.Web.HttpUtility.UrlDecode(crypt.Decrypt(str.Replace(",", "/").Replace(".", "+")
                    .Replace("=", "=="))), typeof(T));
        }

        public string Encrypt(string str)
        {
            AesCrypt crypt = new AesCrypt(_OriginalKey, _OriginalSalt);

            return crypt.Encrypt(str);
        }

        public string Encrypt(string str, string key, string salt)
        {
            AesCrypt enc = new AesCrypt(key, salt);

            return enc.Encrypt(str);
        }

        public string EncryptWithEncode(string str)
        {
            AesCrypt crypt = new AesCrypt(_OriginalKey, _OriginalSalt);

            return System.Web.HttpUtility.UrlDecode(crypt.Encrypt(str).Replace("/", ",").Replace("+", "%2E")
                .Replace("==", "="));
        }

        public bool MembershipProviderCheckPassword(string password, string salt, string hashedPassword)
        {
            return this.MembershipEncodePassword(password, salt, "SHA1") == hashedPassword;
        }

        public string MembershipEncodePassword(string pass, string salt, string hashName)
        {
            byte[] bIn = Encoding.Unicode.GetBytes(pass);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bRet = null;

            HashAlgorithm hm = HashAlgorithm.Create(hashName);
            if (hm is KeyedHashAlgorithm)
            {
                KeyedHashAlgorithm kha = (KeyedHashAlgorithm)hm;
                if (kha.Key.Length == bSalt.Length)
                {
                    kha.Key = bSalt;
                }
                else if (kha.Key.Length < bSalt.Length)
                {
                    byte[] bKey = new byte[kha.Key.Length];
                    Buffer.BlockCopy(bSalt, 0, bKey, 0, bKey.Length);
                    kha.Key = bKey;
                }
                else
                {
                    byte[] bKey = new byte[kha.Key.Length];
                    for (int iter = 0; iter < bKey.Length;)
                    {
                        int len = Math.Min(bSalt.Length, bKey.Length - iter);
                        Buffer.BlockCopy(bSalt, 0, bKey, iter, len);
                        iter += len;
                    }

                    kha.Key = bKey;
                }

                bRet = kha.ComputeHash(bIn);
            }
            else
            {
                byte[] bAll = new byte[bSalt.Length + bIn.Length];
                Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
                Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
                bRet = hm.ComputeHash(bAll);
            }

            return Convert.ToBase64String(bRet);
        }

        // Generate a random string with a given size    
        public string MembershipGeneratePasswordSalt(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            var randomString = builder.ToString();

            return this.Encrypt(randomString);
        }
    }
}