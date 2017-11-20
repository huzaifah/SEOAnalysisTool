using SiteCore.SEOAnalyzer.Domain.Models;
using SiteCore.SEOAnalyzer.Services.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCore.SEOAnalyzer.Domain.Services.Command
{
    public class CountWordsCommand : SEOCommand
    {
        public CountWordsCommand(WebPage page) : base(page) {
        }

        public override void Execute()
        {
            WebPage stripPage = new WebPage { PageSource = page.PageSource, Link = page.Link };

            stripPage.PageSource = PageUtility.RemoveJavaScriptsCSS(stripPage);
            stripPage.PageSource = PageUtility.RemoveHtmlTags(stripPage);
            stripPage.PageSource = PageUtility.RemoveLineBreaks(stripPage);

            var words = stripPage.PageSource.Split(Constants.Delimiters,
                StringSplitOptions.RemoveEmptyEntries);

            var wordsCount = new Dictionary<string, int>();

            StringBuilder builder = new StringBuilder();

            foreach (string currentWord in words)
            {
                string lowerWord = currentWord.ToLower();

                if (wordsCount.ContainsKey(lowerWord))
                {
                    wordsCount[lowerWord]++;
                }
                else
                {
                    wordsCount.Add(lowerWord, 1);
                }
            }

            page.Result.WordsOccurences = wordsCount;
        }
    }
}
