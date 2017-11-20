using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiteCore.SEOAnalyzer.Domain.Services;
using SiteCore.SEOAnalyzer.Domain.Models;
using System.Collections.Generic;

namespace SiteCore.SEOAnalyzer.Tests
{
    [TestClass]
    public class SEOTest
    {
        [TestMethod]
        public void WebAddress_AnalyzeCountWords_GetResult()
        {
            WebPage page = PageFactory.PageFromUrl("https://www.w3schools.com/");
            var options = new List<Constants.AnalysisOption>();
            options.Add(Constants.AnalysisOption.CountWords);

            SEOService seoService = new SEOService(page, options);
            var result = seoService.Analyze();

            Assert.IsTrue(result.WordsOccurences["tutorials"] == 6);
        }

        [TestMethod]
        public void WebAddress_AnalyzeMetaTags_GetResult()
        {
            WebPage page = PageFactory.PageFromUrl("https://www.w3schools.com/");
            var options = new List<Constants.AnalysisOption>();
            options.Add(Constants.AnalysisOption.CountMetaTags);

            SEOService seoService = new SEOService(page, options);
            var result = seoService.Analyze();

            Assert.IsTrue(result.MetaTagOccurences["tutorials"] == 6);
        }

        [TestMethod]
        public void WebAddress_GetExternalLinks_GetResult()
        {
            WebPage page = PageFactory.PageFromUrl("https://www.asp.net/learn");
            var options = new List<Constants.AnalysisOption>();
            options.Add(Constants.AnalysisOption.GetExternalLinks);

            SEOService seoService = new SEOService(page, options);
            var result = seoService.Analyze();

            Assert.IsTrue(result.TotalExternalLink == 57);
            Assert.IsTrue(result.ExternalLinks.Contains("https://www.asp.net/"));
        }
    }
}
