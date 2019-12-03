using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace BooksBorrow.Common.Utils
{
    public class StringEncrypter : IStringEncrypter
    {
        private static string AesKey = "6777A54A6D65447394AF5B0404827AA6";
        private static string IV = "AD254DC0B00093D5";

        public string OneWayEncrypt(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                return String.Empty;
            }

            byte[] data = Encoding.UTF8.GetBytes(text);

            var sha256 = new SHA256Managed();
            byte[] encryptData = sha256.ComputeHash(data);

            return Convert.ToBase64String(encryptData);
        }

        public string TwoWayEncrypt(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                return String.Empty;
            }

            Byte[] toEncryptArray = Encoding.UTF8.GetBytes(text);

            var rijndael = GetAesSymmetricAlgorithm();

            ICryptoTransform cTransform = rijndael.CreateEncryptor();

            Byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray);
        }

        public string TwoWayDecrypt(string text)
        {
            if (String.IsNullOrWhiteSpace(text))
            {
                return String.Empty;
            }

            Byte[] toDecryptArray = Convert.FromBase64String(text);

            var rijndael = GetAesSymmetricAlgorithm();

            ICryptoTransform cTransform = rijndael.CreateDecryptor();

            Byte[] resultArray = cTransform.TransformFinalBlock(toDecryptArray, 0, toDecryptArray.Length);

            return Encoding.UTF8.GetString(resultArray);
        }

        private static RijndaelManaged GetAesSymmetricAlgorithm()
        {
            var result = new RijndaelManaged();

            result.Key = Encoding.UTF8.GetBytes(AesKey);
            result.IV = Encoding.UTF8.GetBytes(IV);

            return result;
        }
    }
}
