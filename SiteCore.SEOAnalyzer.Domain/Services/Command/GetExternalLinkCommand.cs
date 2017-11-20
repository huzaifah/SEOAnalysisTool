using SiteCore.SEOAnalyzer.Domain.Models;
using SiteCore.SEOAnalyzer.Services.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiteCore.SEOAnalyzer.Domain.Services.Command
{
    public class GetExternalLinkCommand : SEOCommand
    {
        public GetExternalLinkCommand(WebPage page) : base(page)
        {

        }

        public override void Execute()
        {
            Match m;
            string HRefPattern = "href\\s*=\\s*(?:[\"'](?<1>[^\"']*)[\"']|(?<1>\\S+))";
            List<string> linkList = new List<string>();

            try
            {
                m = Regex.Match(page.PageSource, HRefPattern,
                                RegexOptions.IgnoreCase | RegexOptions.Compiled,
                                TimeSpan.FromSeconds(1));

                while (m.Success)
                {
                    if (!linkList.Contains(m.Groups[1].Value))
                    {
                        if (m.Groups[1].Value.Contains("http") && !String.IsNullOrEmpty(page.Link) && !m.Groups[1].Value.Contains(page.Link))
                            linkList.Add(m.Groups[1].Value);

                        if (String.IsNullOrEmpty(page.Link))
                            linkList.Add(m.Groups[1].Value);
                    }

                    m = m.NextMatch();
                }
            }
            catch (RegexMatchTimeoutException)
            {
                Console.WriteLine("The matching operation timed out.");
            }

            page.Result.ExternalLinks = linkList;
        }
    }
}
