using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        public static string BuildString(this char[] source)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < source.Count(); i++)
            {
                sb.Append(source[i]);
            }

            return sb.ToString();
        }
        public static List<string> ExtractHashtags(this string source)
        {
            List<string> result = new List<string>();

            Regex regex = new Regex(@"(?<=#)\w+");
            MatchCollection matches = regex.Matches(source);

            foreach (Match m in matches)
            {
                result.Add(m.Value);
            }

            return result;
        }
    }

}
