using SiteCore.SEOAnalyzer.Domain.Models;
using SiteCore.SEOAnalyzer.Domain.Services;
using SiteCore.SEOAnalyzer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteCore.SEOAnalyzer.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["SubTitle"] = "Welcome to SEO Analysis Tool";
            ViewData["Message"] = "It is an application for you to perform a simple SEO analysis by providing the page content or URL.";

            return View();
        }

        [HttpPost]
        public ActionResult Analyze(AnalyzeViewModel model)
        {
            SEOResultViewModel resultViewModel = new SEOResultViewModel();

            if (!ModelState.IsValid)
            {
                resultViewModel.IsSuccess = false;
                resultViewModel.Error = "Invalid input";

                return Json(resultViewModel);
            }

            if (String.IsNullOrEmpty(model.PageContent) && String.IsNullOrEmpty(model.PageUrl))
            {
                resultViewModel.IsSuccess = false;
                resultViewModel.Error = "Page URL or page content is required";

                return Json(resultViewModel);
            }

            if (model.Options == null)
            {
                resultViewModel.IsSuccess = false;
                resultViewModel.Error = "No analysis option is selected";

                return Json(resultViewModel);
            }

            try
            {
                WebPage page;

                if (!String.IsNullOrEmpty(model.PageUrl))
                    page = PageFactory.PageFromUrl(model.PageUrl);
                else
                    page = PageFactory.PageFromText(model.PageContent);
                                
                var options = new List<Constants.AnalysisOption>();

                foreach (var o in model.Options)
                {
                    switch (o)
                    {
                        case "countExternalLink":
                            options.Add(Constants.AnalysisOption.GetExternalLinks);
                            break;
                        case "countWords":
                            options.Add(Constants.AnalysisOption.CountWords);
                            break;
                        case "countMetaTag":
                            options.Add(Constants.AnalysisOption.CountMetaTags);
                            break;                        
                        default:
                            break;
                    }
                }

                SEOService seoService = new SEOService(page, options);
                var result = seoService.Analyze();

                resultViewModel.IsSuccess = true;
                List<WordCount> wordCounts = new List<WordCount>();
                foreach (var word in result.WordsOccurences)
                {
                    wordCounts.Add(new WordCount { Word = word.Key, Total = word.Value });
                }

                resultViewModel.WordCount = wordCounts;

                List<WordCount> metaTagsCounts = new List<WordCount>();
                foreach (var word in result.MetaTagOccurences)
                {
                    metaTagsCounts.Add(new WordCount { Word = word.Key, Total = word.Value });
                }

                resultViewModel.MetaTagsCount = metaTagsCounts;

                resultViewModel.ExternalLinks = result.ExternalLinks.ConvertAll(e => new Link { Address = e });
            }
            catch (Exception ex)
            {
                resultViewModel.IsSuccess = false;
                resultViewModel.Error = ex.Message;

                return Json(resultViewModel);
            }


            return Json(resultViewModel);
        }
    }
}