using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace System
{
    public static class StringExtensions
    {
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
