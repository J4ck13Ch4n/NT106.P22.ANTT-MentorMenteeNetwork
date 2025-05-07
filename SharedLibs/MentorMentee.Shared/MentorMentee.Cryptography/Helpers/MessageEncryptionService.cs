using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MentorMentee.Cryptography.Helpers
{
    public class MessageEncryptionService
    {
        public static (byte[] EncryptedMessage, byte[] EncryptedKey) EncryptMessage(string message, RSAParameters rsaPublicKey)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] encryptedMessage = AESEncryptionHelper.EncryptMessage(message, aes.Key, aes.IV);
                byte[] encryptedKey = RSAEncryptionHelper.EncryptAESKey(aes.Key, rsaPublicKey);
                return (encryptedMessage, encryptedKey);
            }
        }

        public static string DecryptMessage(byte[] encryptedMessage, byte[] encryptedKey, RSAParameters rsaPrivateKey)
        {
            byte[] aesKey = RSAEncryptionHelper.DecryptAESKey(encryptedKey, rsaPrivateKey);
            return AESEncryptionHelper.DecryptMessage(encryptedMessage, aesKey, new byte[16]);
        }
    }
}
