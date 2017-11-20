using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteCore.SEOAnalyzer.Web.Models
{
    public class AnalyzeViewModel
    {
        public string PageUrl { get; set; }

        [AllowHtml]
        public string PageContent { get; set; }
        public string[] Options { get; set; }
    }
}