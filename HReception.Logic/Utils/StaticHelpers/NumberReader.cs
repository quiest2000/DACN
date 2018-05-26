using System;

namespace HReception.Logic.Utils.StaticHelpers
{
    public class NumberReader
    {
        private static readonly string[] Digits = { " không", " một", " hai", " ba", " bốn", " năm", " sáu", " bảy", " tám", " chín" };
        private static readonly string[] Units = { "", " nghìn", " triệu", " tỷ", " nghìn tỷ", " triệu tỷ" };
        private static string ReadGroup(int threeDigits)
        {
            var result = "";
            var tram = threeDigits / 100;
            var chuc = (threeDigits % 100) / 10;
            var donvi = threeDigits % 10;
            if ((tram == 0) && (chuc == 0) && (donvi == 0)) return "";
            if (tram != 0)
            {
                result += Digits[tram] + " trăm";
                if ((chuc == 0) && (donvi != 0)) result += " linh";
            }
            if ((chuc != 0) && (chuc != 1))
            {
                result += Digits[chuc] + " mươi";
                if ((chuc == 0) && (donvi != 0)) result += " linh";
            }
            if (chuc == 1) result += " mười";
            switch (donvi)
            {
                case 1:
                    if ((chuc != 0) && (chuc != 1))
                    {
                        result += " mốt";
                    }
                    else
                    {
                        result += Digits[donvi];
                    }
                    break;
                case 5:
                    if (chuc == 0)
                    {
                        result += Digits[donvi];
                    }
                    else
                    {
                        result += " lăm";
                    }
                    break;
                default:
                    if (donvi != 0)
                    {
                        result += Digits[donvi];
                    }
                    break;
            }
            return result;
        }
        /// <summary>
        /// In VNese
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static string Read(long input, string tail = "đồng")
        {
            tail = (tail ?? string.Empty).Trim();
            if (input < 0) return "Số tiền âm!";
            if (input == 0) return $"Không {tail}!";
            if (input >= long.MaxValue)
            {
                return $"{input.ToString("N0")} {tail}";
            }
            var so = input > 0 ? input : -input;
            int step, index;

            var ketQua = "";
            var positions = new int[6];

            positions[5] = (int)(so / 1000000000000000);
            so = so - long.Parse(positions[5].ToString()) * 1000000000000000;
            positions[4] = (int)(so / 1000000000000);
            so = so - long.Parse(positions[4].ToString()) * +1000000000000;
            positions[3] = (int)(so / 1000000000);
            so = so - long.Parse(positions[3].ToString()) * 1000000000;
            positions[2] = (int)(so / 1000000);
            positions[1] = (int)((so % 1000000) / 1000);
            positions[0] = (int)(so % 1000);
            if (positions[5] > 0)
            {
                step = 5;
            }
            else if (positions[4] > 0)
            {
                step = 4;
            }
            else if (positions[3] > 0)
            {
                step = 3;
            }
            else if (positions[2] > 0)
            {
                step = 2;
            }
            else if (positions[1] > 0)
            {
                step = 1;
            }
            else
            {
                step = 0;
            }
            for (index = step; index >= 0; index--)
            {
                var tmp = ReadGroup(positions[index]);
                ketQua += tmp;
                if (positions[index] != 0) ketQua += Units[index];
                if ((index > 0) && (!string.IsNullOrEmpty(tmp))) ketQua += ",";
            }
            if (ketQua.Substring(ketQua.Length - 1, 1) == ",") ketQua = ketQua.Substring(0, ketQua.Length - 1);
            ketQua = $"{ketQua.Trim()} {tail}";
            return ketQua.Substring(0, 1).ToUpper() + ketQua.Substring(1);
        }
        /// <summary>
        /// In VNese
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static string Read(double input, string tail = "đồng")
        {
            return Read(Convert.ToInt64(input), tail);
        }
        /// <summary>
        /// In VNese
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static string Read(int input, string tail = "đồng")
        {
            return Read(Convert.ToInt64(input), tail);
        }
        /// <summary>
        /// In VNese
        /// </summary>
        /// <param name="input"></param>
        /// <param name="tail"></param>
        /// <returns></returns>
        public static string Read(short input, string tail = "đồng")
        {
            return Read(Convert.ToInt64(input), tail);
        }
    }
}
