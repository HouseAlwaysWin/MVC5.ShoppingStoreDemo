using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EncryptionKeyExample
{
    public class SymmetricKey
    {
        public string GenerateSecretKey()
        {
            var provider = new RNGCryptoServiceProvider();

            byte[] secretKeyBytes = new byte[32];

            provider.GetBytes(secretKeyBytes);

            return Convert.ToBase64String(secretKeyBytes);
        }

        public string GenerateSignature(
            string secretKeyString, string password)
        {
            var secretKeyBytes = Convert.FromBase64String(secretKeyString);

            // Symmertic key signing
            byte[] passwordData =
                Encoding.UTF8.GetBytes(password);

            HMACSHA256 hmac = new HMACSHA256(secretKeyBytes);


            byte[] signatureBytes = hmac.ComputeHash(passwordData);

            string signature = Convert.ToBase64String(signatureBytes);

            return signature;
        }

        public bool SymmerticKeySigningVerification(
            string clientSignatured, string secretKeyString, string clientPassword)
        {

            if (GenerateSignature(secretKeyString, clientPassword)
                .Equals(clientSignatured, StringComparison.Ordinal))
            {
                Console.WriteLine("Log In Successful");
                return true;
            }
            Console.WriteLine("Log In Failed");
            return false;
        }

        public static void SymmerticSystemEmulator()
        {
            SymmetricKey sk = new SymmetricKey();
            var serverSideSecretKey = sk.GenerateSecretKey();
            Console.WriteLine(string.Format("Server send secret Key to Client:{0}"
                , serverSideSecretKey));
            Console.WriteLine(
                "Client encrypted secret key with password" +
                ",Please enter password:");
            var serverSidePassword = Console.ReadLine();
            Console.WriteLine("----------------------------");
            Console.WriteLine("Client send signature back Server...");
            var sendBackSignature = sk.GenerateSignature(serverSideSecretKey
               , serverSidePassword);
            Console.WriteLine("----------------------------");



            Console.WriteLine("This is Your Signature: " + sendBackSignature);
            Console.WriteLine("Please,Enter your password:");
            var password = Console.ReadLine();
            Console.WriteLine("Please,Enter your signature:");
            var clientSignature = Console.ReadLine();



            var serverSideValidate = sk.SymmerticKeySigningVerification(
                clientSignature, serverSideSecretKey, password);
            Console.WriteLine("Validation Status: " + serverSideValidate);
        }
    }
}
