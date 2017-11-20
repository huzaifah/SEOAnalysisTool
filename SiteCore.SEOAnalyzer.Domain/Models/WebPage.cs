using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCore.SEOAnalyzer.Domain.Models
{
    public class WebPage
    {
        public string Link { get; set; }
        public string PageSource { get; set; }
        public SEOResult Result { get; set; }

        public WebPage()
        {
            Result = new SEOResult();
        }
    }
}
