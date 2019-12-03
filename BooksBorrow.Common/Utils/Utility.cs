using BooksBorrow.Common.Constants;
using BooksBorrow.Common.Extensions;
using BooksBorrow.InjectFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace BooksBorrow.Common.Utils
{
    public static class Utility
    {
        public static Guid GetNewId()
        {
            return Guid.NewGuid();
        }

        public static DateTime GetCurrentDateTime()
        {
            return DateTime.UtcNow;
        }

        public static string EncryptPassword(string password)
        {
            return GetStringEncrypter().OneWayEncrypt(password);
        }

        public static string EncryptString(string source)
        {
            return GetStringEncrypter().TwoWayEncrypt(source);
        }

        public static string DecryptString(string source)
        {
            return GetStringEncrypter().TwoWayDecrypt(source);
        }

        private static IStringEncrypter GetStringEncrypter()
        {
            return InjectContainer.GetInstance<IStringEncrypter>();
        }

        public static string GetRandomString(int length, bool useNumber, bool useLowerCase, bool useUpperCase, bool useSpecificSymbol, string custom = "")
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;

            if (useNumber) { str += "0123456789"; }
            if (useLowerCase) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpperCase) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpecificSymbol) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }

            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }

            return s;
        }

        public static string GetRandomNumberString(int length)
        {
            var number = GetRandomString(length, true, false, false, false);

            return number;
        }

        public static string GetRandomAlphaString(int length)
        {
            return GetRandomString(length, false, true, true, false);
        }

        public static bool RowVersionEqual(byte[] obj1, byte[] obj2)
        {
            if (obj1 == null && obj2 == null)
            {
                throw new ArgumentNullException();
            }

            if (obj1 == null || obj2 == null)
            {
                return false;
            }

            return BitConverter.ToInt64(obj1, 0) == BitConverter.ToInt64(obj2, 0);
        }

        public static string BuildErrorMessage(Exception ex)
        {
            var errorMessage = new StringBuilder();

            errorMessage.AppendLine(ex.Message);
            errorMessage.AppendLine(ex.StackTrace);
            errorMessage.AppendLine(ex.Source);

            return errorMessage.ToString();
        }

        public static bool IsValidUserName(string userName)
        {
            return userName != null && userName.All(c => Char.IsLetterOrDigit(c) || c == '_');
        }

        public static bool IsValidPassword(string password)
        {
            return password != null && password.Length >= 8 &&
                password.Length <= 16 &&
                password.Any(c => Char.IsLetterOrDigit(c)) &&
                password.Any(c => Char.IsSymbol(c) || Char.IsPunctuation(c));
        }

        public static bool IsEmailAddressValid(string emailAddress)
        {
            var attribute = new EmailAddressAttribute();

            return attribute.IsValid(emailAddress);
        }

        public static string GetExceptionContent(System.Exception exception, int depth = 0)
        {
            System.Exception logException = exception;

            string indentString = GetIndentString(depth);

            var contentBuilder = new StringBuilder();

            string exceptionTitle = depth > 0 ? "Inner Exception" : "Exception";

            contentBuilder.AppendLine(String.Format("{0}{1}: {2}", indentString, exceptionTitle, logException.Message));
            contentBuilder.AppendLine(String.Format("{0}Source:", indentString));
            contentBuilder.AppendLine(String.Format("{0}{1}", indentString, logException.Source));
            contentBuilder.AppendLine(String.Format("{0}StackTrace:", indentString));
            contentBuilder.AppendLine(String.Format("{0}{1}", indentString, logException.StackTrace));

            if (logException.InnerException != null)
            {
                contentBuilder.AppendLine(String.Format("{0}Inner Exception:", indentString));
                contentBuilder.AppendLine(String.Format("{0}{1}", indentString, GetExceptionContent(logException.InnerException, depth + 1)));
            }

            return contentBuilder.ToString();
        }

        private static string GetIndentString(int depth)
        {
            if (depth <= 0)
            {
                return String.Empty;
            }

            string[] result = new string[depth];

            for (int i = 0; i < depth; i++)
            {
                result[i] = "\t";
            }

            return String.Join(String.Empty, result);
        }

        public static string GetCurrencyString(decimal? value)
        {
            return value.HasValue ? value.ToCurrencyString() : SystemDefaultSettings.EmptyStringShow;
        }
    }
}
