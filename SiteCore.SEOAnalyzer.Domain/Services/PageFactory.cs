using SiteCore.SEOAnalyzer.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SiteCore.SEOAnalyzer.Domain.Services
{
    public static class PageFactory
    {
        public static WebPage PageFromText(string pageContent)
        {
            return new WebPage
            {
                PageSource = pageContent
            };
        }

        public static WebPage PageFromUrl(string url)
        {
            WebClient client = new WebClient();
            string pageContent = String.Empty;

            try
            {
                pageContent = client.DownloadString(url);                
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to access the given address", ex);
            }
                        
            return new WebPage 
            { 
                PageSource = pageContent, 
                Link = url 
            };
        }
    }
}
