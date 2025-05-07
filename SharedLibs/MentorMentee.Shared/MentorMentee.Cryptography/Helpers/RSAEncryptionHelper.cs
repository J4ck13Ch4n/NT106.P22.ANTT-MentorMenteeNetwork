using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MentorMentee.Cryptography.Helpers
{
    public class RSAEncryptionHelper
    {
        public static byte[] EncryptAESKey(byte[] aesKey, RSAParameters rsaKeyInfo)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(rsaKeyInfo);
                return rsa.Encrypt(aesKey, RSAEncryptionPadding.OaepSHA256);
            }
        }

        public static byte[] DecryptAESKey(byte[] encryptedAesKey, RSAParameters rsaKeyInfo)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(rsaKeyInfo);
                return rsa.Decrypt(encryptedAesKey, RSAEncryptionPadding.OaepSHA256);
            }
        }
    }
}
