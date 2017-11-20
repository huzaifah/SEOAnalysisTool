using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCore.SEOAnalyzer.Domain.Models
{
    public class SEOResult
    {
        public Dictionary<string, int> WordsOccurences { get; set; }
        public Dictionary<string, int> MetaTagOccurences { get; set; }
        public List<string> ExternalLinks { get; set; }

        public SEOResult()
        {
            WordsOccurences = new Dictionary<string, int>();
            MetaTagOccurences = new Dictionary<string, int>();
            ExternalLinks = new List<string>();
        }

        public int TotalExternalLink
        {
            get 
            {
                return ExternalLinks.Count;
            }            
        }
    }
}
