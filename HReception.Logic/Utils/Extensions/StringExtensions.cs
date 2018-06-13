using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace HReception.Logic.Utils.Extensions
{
    public static class StringExtensions
    {
        private const string Seperator = ";";
        private static readonly string[] VNeSigns =
        {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
        };
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        public static string Separate(this IEnumerable<string> source)
        {
            return string.Join(Seperator, source);
        }

        public static string[] GetFromSeparated(this string separated)
        {
            return separated.IsNullOrEmpty() ? new string[0] : separated.Split(Seperator.ToCharArray());
        }
        public static string ToNoneSign(this string text)
        {
            if (text == null)
                return null;
            for (var i = 1; i < VNeSigns.Length; i++)
                for (var j = 0; j < VNeSigns[i].Length; j++)
                    text = text.Replace(VNeSigns[i][j], VNeSigns[0][i - 1]);

            return text;
        }

        public static bool IsValidPhoneNumber(this string number)
        {
            return !number.IsNullOrEmpty() && (number.Length == 10 || number.Length == 11) && Regex.Match(number, @"^-*[0-9,\.?\-?\(?\)?\ ]+$").Success;
        }

        public static bool IsValidEmail(this string address)
        {
            try
            {
                var mail = new MailAddress(address);
                return true;
            }
            catch 
            {
                return false;
            }
        }
    }
}
