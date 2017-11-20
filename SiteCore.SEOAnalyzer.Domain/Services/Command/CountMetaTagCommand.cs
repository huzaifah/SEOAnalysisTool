using SiteCore.SEOAnalyzer.Domain.Models;
using SiteCore.SEOAnalyzer.Services.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCore.SEOAnalyzer.Domain.Services.Command
{
    public class CountMetaTagCommand : SEOCommand
    {
        private List<string> metaTags;

        public CountMetaTagCommand(WebPage page, List<string> metaTags) : base(page)
        {
            this.metaTags = metaTags;
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

            foreach (var metaTag in metaTags)
            {
                wordsCount.Add(metaTag, 0);
            }

            StringBuilder builder = new StringBuilder();

            foreach (string currentWord in words)
            {
                string lowerWord = currentWord.ToLower();

                if (wordsCount.ContainsKey(lowerWord))
                {
                    wordsCount[lowerWord]++;
                }
            }

            page.Result.MetaTagOccurences = wordsCount;
        }
    }
}
