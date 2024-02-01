using System.Security.Cryptography;
using System.Text;

namespace BSC.Core.Common.Security
{
    public class AesCrypt
    {
        #region Private Fields

        private byte[] _Key;
        private byte[] _Iv;

        #endregion Private Fields

        #region Read-Only Properties

        public string KeyStr
        {
            get { return Convert.ToBase64String(_Key); }
        }

        public string IvStr
        {
            get { return Convert.ToBase64String(_Iv); }
        }

        #endregion Read-Only Properties

        #region Constructors

        public AesCrypt(string pass, string keySalt)
            : this(pass, Encoding.ASCII.GetBytes(keySalt))
        {
        }

        public AesCrypt(string pass, byte[] keySalt)
        {
            if (pass == null || pass.Length <= 0)
                throw new ArgumentNullException("pass");
            if (keySalt == null || keySalt.Length <= 0)
                throw new ArgumentNullException("keySalt");

            // Derive a Key and an IV from the Password and create an algorithm
            //PasswordDeriveBytes pdb = new PasswordDeriveBytes( pass, keySalt ); // Weak - based on PBKDF1
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(pass, keySalt); // Strong - based on PBKDF2

            _Key = pdb.GetBytes(32);
            _Iv = pdb.GetBytes(16);
        }

        #endregion Constructor

        #region Methods

        public string Encrypt(string str)
        {
            // Check arguments.
            if (str == null || str.Length <= 0)
                throw new ArgumentNullException("str");

            RijndaelManaged aesAlg = null;

            // Declare the stream used to encrypt to an in memory
            // array of bytes.
            MemoryStream msEncrypt = null;

            try
            {
                // Tip: When the object is created, Key and IV properties are initialized to random values.
                //      In this class we want fixed Key and IV values. We use a PasswordDeriveBytes objetc for that. 
                // Tip: Default Cipher Mode is: CipherMode.CBC
                aesAlg = new RijndaelManaged();
                aesAlg.Key = _Key;
                aesAlg.IV = _Iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                msEncrypt = new MemoryStream();

                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(str);
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        public string Decrypt(string str)
        {
            byte[] cipherText = Convert.FromBase64String(str);

            // Check arguments.
            if (str == null || str.Length <= 0)
                throw new ArgumentNullException("str");

            // Declare the RijndaelManaged object
            // used to encrypt the data.
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            try
            {
                // Tip: When the object is created, Key and IV properties are initialized to random values.
                //      In this class we want fixed Key and IV values. We use a PasswordDeriveBytes objetc for that. 
                aesAlg = new RijndaelManaged();
                aesAlg.Key = _Key;
                aesAlg.IV = _Iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            finally
            {
                // Clear the RijndaelManaged object.
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }

        public void GenerateNewValues(string pass, string keySalt)
        {
            GenerateNewValues(pass, Encoding.ASCII.GetBytes(keySalt));
        }

        public void GenerateNewValues(string pass, byte[] keySalt)
        {
            if (pass == null || pass.Length <= 0)
                throw new ArgumentNullException("pass");
            if (keySalt == null || keySalt.Length <= 0)
                throw new ArgumentNullException("keySalt");

            // Derive a Key and an IV from the Password and create an algorithm
            //PasswordDeriveBytes pdb = new PasswordDeriveBytes( pass, keySalt ); // Weak - based on PBKDF1
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(pass, keySalt); // Strong - based on PBKDF2

            _Key = pdb.GetBytes(32);
            _Iv = pdb.GetBytes(16);
        }


        public static string GetPasswordDerivedBytes(string passPhrase, string saltValue, int size)
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(passPhrase, Encoding.ASCII.GetBytes(saltValue));

            return Convert.ToBase64String(pdb.GetBytes(size));
        }

        #endregion Methods
    }
}