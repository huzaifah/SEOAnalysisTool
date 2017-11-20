using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteCore.SEOAnalyzer.Web.Models
{
    public class SEOResultViewModel
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public IEnumerable<WordCount> WordCount { get; set; }
        public IEnumerable<WordCount> MetaTagsCount { get; set; }
        public IEnumerable<Link> ExternalLinks { get; set; }
        public int Page { get; set; }
        public int Total { get; set; }
        public int Records { get; set; }
    }

    public class WordCount
    {
        public string Word { get; set; }
        public int Total { get; set; }
    }

    public class Link
    {
        public string Address { get; set; }
    }
}