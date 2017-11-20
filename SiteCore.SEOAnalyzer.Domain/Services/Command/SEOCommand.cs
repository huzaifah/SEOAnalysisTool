using SiteCore.SEOAnalyzer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteCore.SEOAnalyzer.Services.Command
{
    public abstract class SEOCommand
    {
        protected WebPage page;
        public SEOCommand(WebPage page)
        {
            this.page = page;
        }

        public abstract void Execute();
    }
}
