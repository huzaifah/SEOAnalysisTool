using SiteCore.SEOAnalyzer.Domain.Models;
using SiteCore.SEOAnalyzer.Domain.Services.Command;
using SiteCore.SEOAnalyzer.Services.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCore.SEOAnalyzer.Domain.Services
{
    public class SEOService
    {
        private WebPage pageToAnalyze;
        private List<Constants.AnalysisOption> options;

        public SEOService(WebPage page, List<Constants.AnalysisOption> options)
        {
            this.pageToAnalyze = page;
            this.options = options;
        }

        public SEOResult Analyze()
        {
            List<SEOCommand> commands = new List<SEOCommand>();

            foreach (var analysis in options)
            {
                switch (analysis)
                {
                    case Constants.AnalysisOption.GetExternalLinks:
                        commands.Add(new GetExternalLinkCommand(pageToAnalyze));
                        break;
                    case Constants.AnalysisOption.CountWords:
                        commands.Add(new CountWordsCommand(pageToAnalyze));
                        break;
                    case Constants.AnalysisOption.CountMetaTags:
                        List<string> metaTags = PageUtility.GetMetaTags(pageToAnalyze.PageSource).Values.Distinct().ToList();
                        List<string> metaTagsWords = new List<string>();

                        foreach (var word in metaTags)
                        {
                            metaTagsWords.AddRange(word.Split(Constants.Delimiters, StringSplitOptions.RemoveEmptyEntries));
                        }

                        commands.Add(new CountMetaTagCommand(pageToAnalyze, metaTagsWords));
                        break;
                    default:
                        break;
                }    
            }

            foreach (var command in commands)
            {
                command.Execute();
            }

            return pageToAnalyze.Result;
        }
    }
}
