using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiteCore.SEOAnalyzer.Domain.Models;
using SiteCore.SEOAnalyzer.Domain.Services;
using System.Collections.Generic;
using SiteCore.SEOAnalyzer.Domain.Services.Command;

namespace SiteCore.SEOAnalyzer.Tests
{
    [TestClass]
    public class AnalysisTest
    {
        [TestInitialize]
        public void SetUp()
        {
        }

        [TestMethod]
        public void FilterStopWords_StopWordsRemoved()
        {
            string pageContent = "I am preparing the test";
            WebPage page = PageFactory.PageFromText(pageContent);
            string filteredText = PageUtility.FilterStopWords(page);

            var filteredPage = PageFactory.PageFromText(filteredText);
            Assert.IsFalse(PageUtility.ContainStopWords(filteredPage));
            Console.WriteLine(String.Format("Filtered Text: {0}", filteredPage.PageSource));
        }

        [TestMethod]
        public void CalculateWordsOccurences_ReturnWordsCount()
        {
            string pageContent =
                "This course will guide you to a watershed moment in your learning. If you’ve struggled to learn C#, Visual Studio, and how to translate ideas into working applications, then I personally guarantee you’ll break through upon completing this course.";
            WebPage page = PageFactory.PageFromText(pageContent);
            
            var command = new CountWordsCommand(page);
            command.Execute();

            Dictionary<string, int> wordsCount = page.Result.WordsOccurences;
            Assert.IsTrue(wordsCount.ContainsKey("course"));
            int countSimilarWords = wordsCount["course"];
            Assert.AreEqual(2, countSimilarWords);
        }

        [TestMethod]
        public void TripHtmlTags_ReturnNoHtml()
        {
            string pageContent =
                "<p>This <b>course</b> will guide you to a watershed moment in your learning.<img src='http://www.google.com' alt='my photo' /> If you’ve struggled to learn C#, Visual Studio, and how to translate ideas into working applications, then I personally guarantee you’ll break through upon completing this course.</p>";
            WebPage page = PageFactory.PageFromText(pageContent);
            string noHtmlPage = PageUtility.RemoveHtmlTags(page);

            Assert.IsFalse(noHtmlPage.Contains("<p>"));
            Assert.IsFalse(noHtmlPage.Contains("<b>"));
            Assert.IsFalse(noHtmlPage.Contains("<img src"));
            Console.WriteLine(String.Format("No Html text: {0}", noHtmlPage));
        }

        [TestMethod]
        public void CalculateWordsInMetaTag_ReturnWordsCount()
        {
            string pageContent =
                "This course will guide you to a watershed moment in your learning. If you’ve struggled to learn C#, Visual Studio, and how to translate ideas into working applications, then I personally guarantee you’ll break through upon completing this course.";
            WebPage page = PageFactory.PageFromText(pageContent);
            
            List<string> wordsInMetaTags = new List<string>() 
            { 
                "course", 
                "learning",
                "virtual classroom"
            };

            var command = new CountMetaTagCommand(page, wordsInMetaTags);
            command.Execute();

            Dictionary<string, int> wordsCount = page.Result.MetaTagOccurences;

            Assert.IsTrue(wordsCount.ContainsKey("course"));
            int countSimilarWords = wordsCount["course"];
            Assert.AreEqual(2, countSimilarWords);
        }

        [TestMethod]
        public void GetExternalLinks_ReturnLinks()
        {
            string pageContent =
                "This course will guide you to a watershed moment in your learning.<a href=\"http://www.google.com\">Search this page</a> If you’ve struggled to learn C#, Visual Studio, and how to translate ideas into working applications, then I personally guarantee you’ll break through upon completing this course.";
            WebPage page = PageFactory.PageFromText(pageContent);

            var command = new GetExternalLinkCommand(page);
            command.Execute();

            List<string> linksFound = page.Result.ExternalLinks;

            Assert.IsTrue(linksFound.Count > 0);
            Assert.IsTrue(linksFound.Contains("http://www.google.com"));
        }
    }
}
