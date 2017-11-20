using SiteCore.SEOAnalyzer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiteCore.SEOAnalyzer.Domain.Services
{
    public static class PageUtility
    {
        public static bool ContainStopWords(WebPage page)
        {
            var words = page.PageSource.Split(Constants.Delimiters,
                StringSplitOptions.RemoveEmptyEntries);

            foreach (string currentWord in words)
            {
                string lowerWord = currentWord.ToLower();

                if (Constants.StopWords.ContainsKey(lowerWord))
                {
                    return true;
                }
            }

            return false;
        }

        public static string FilterStopWords(WebPage page)
        {
            var words = page.PageSource.Split(Constants.Delimiters,
                StringSplitOptions.RemoveEmptyEntries);

            var found = new Dictionary<string, bool>();

            StringBuilder builder = new StringBuilder();

            foreach (string currentWord in words)
            {
                string lowerWord = currentWord.ToLower();

                if (!Constants.StopWords.ContainsKey(lowerWord) &&
                    !found.ContainsKey(lowerWord))
                {
                    builder.Append(currentWord).Append(' ');
                    found.Add(lowerWord, true);
                }
            }

            return builder.ToString().Trim();
        }

        public static string RemoveLineBreaks(WebPage page)
        {
            string result = Regex.Replace(page.PageSource, @"\r\n\t?|\n", String.Empty);
            return result;
        }

        public static string RemoveJavaScriptsCSS(WebPage page)
        {
            var regex = new Regex("(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)",
                RegexOptions.Singleline | RegexOptions.IgnoreCase
            );

            string output = regex.Replace(page.PageSource, "");
            return output;
        }

        public static string RemoveHtmlTags(WebPage page)
        {
            string noHtmlText = StripTagsCharArray(page.PageSource);
            return noHtmlText;
        }

        private static string StripTagsCharArray(string source)
        {
            char[] array = new char[source.Length];
            int arrayIndex = 0;
            bool inside = false;

            for (int i = 0; i < source.Length; i++)
            {
                char let = source[i];
                if (let == '<')
                {
                    inside = true;
                    continue;
                }
                if (let == '>')
                {
                    inside = false;
                    continue;
                }
                if (!inside)
                {
                    array[arrayIndex] = let;
                    arrayIndex++;
                }
            }
            return new string(array, 0, arrayIndex);
        }

        public static Dictionary<string, string> GetMetaTags(string page)
        {
            Regex metaTag = new Regex("<meta name=\"(.+?)\" content=\"(.+?)\">");
            Dictionary<string, string> metaInformation = new Dictionary<string, string>();

            foreach (Match m in metaTag.Matches(page))
            {
                if (!m.Groups[1].Value.Equals("viewport", StringComparison.OrdinalIgnoreCase))
                    metaInformation.Add(m.Groups[1].Value, m.Groups[2].Value);
            }

            return metaInformation;
        }
    }
}
