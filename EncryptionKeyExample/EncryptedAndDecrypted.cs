using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionKeyExample
{
    public class EncryptedAndDecrypted
    {
        private byte[] TransForm(byte[] textBytes, ICryptoTransform transform)
        {
            using (var buf = new MemoryStream())
            {
                using (var stream =
                    new CryptoStream(
                        buf, transform, CryptoStreamMode.Write))
                {
                    stream.Write(textBytes, 0, textBytes.Length);
                    stream.FlushFinalBlock();
                    return buf.ToArray();
                }
            }
        }
        public void EncrytedAndDectyted()
        {
            using (RijndaelManaged provider = new RijndaelManaged())
            {
                // Converting 128 bits to bytes
                byte[] initVector = new byte[provider.BlockSize / 8];
                // Converting 256 bits to bytes
                byte[] key = new byte[provider.KeySize / 8];

                using (var rngProvider = new RNGCryptoServiceProvider())
                {
                    rngProvider.GetBytes(initVector);
                    rngProvider.GetBytes(key);
                }

                // Encrypted Code
                string password = "secret123";
                byte[] clearBytes = Encoding.UTF8.GetBytes(password);

                byte[] foggyBytes = TransForm(
                    clearBytes, provider.CreateEncryptor(key, initVector));

                string encryptedData = Convert.ToBase64String(foggyBytes);
                Console.WriteLine(string.Format("Encrypted Password:{0}", encryptedData));


                // Decrypted code

                byte[] decrytpedData = Convert.FromBase64String(encryptedData);

                var decryptedPassword = Encoding.UTF8.GetString(
                    TransForm(decrytpedData,
                    provider.CreateDecryptor(key, initVector)));
                Console.WriteLine("Decrypted Password:{0}", decryptedPassword);

            }

        }

       

    }
}
