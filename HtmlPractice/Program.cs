using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace HtmlPractice
{
    public class PageData
    {
        private string sitepath;
        private string email;

        public PageData(string email, string sitepath)
        {
            this.email = email;
            this.sitepath = sitepath;
        }

        public string Get(string value)
        {
            if (value.ToLower() == "email")
            {
                return this.email;
            }
            else if (value.ToLower() == "sitepath")
            {
                return this.sitepath;
            }

            return "";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            HtmlWeb web = new HtmlWeb();
            string siteName = @"https://www.tripadvisor.co.uk";

            HtmlDocument htmlDoc = web.Load(siteName + @"/Restaurants-g186317-Kingston_upon_Hull_East_Riding_of_Yorkshire_England.html");
            HtmlDocument subPathSite;

            Regex emailMatch = new Regex(@"[A-z0-9._%+-]+@[A-z]+.[a-z]+");
            MatchCollection matches;

            List<PageData> pageDataList = new List<PageData>();


            var nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class='ui_shelf_item_detail']");
            List<string> hrefTags = new List<string>();

            foreach (HtmlNode link in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = link.Attributes["href"];
                hrefTags.Add(att.Value);
            }

            hrefTags = hrefTags.Distinct().ToList();

            foreach(string path in hrefTags)
            {
                if(path.Length > 10)
                {
                    if (path.Substring(0, 2) == "/R")
                    {
                        if (!path.Contains("#REVIEWS") && path.Contains("Review"))
                        {
                            subPathSite = web.Load(siteName + path);

                            matches = emailMatch.Matches(subPathSite.DocumentNode.OuterHtml);

                            foreach (Match emailAddress in matches)
                            {
                                if (!emailAddress.ToString().Contains("sentry"))
                                {
                                    pageDataList.Add(new PageData(emailAddress.ToString(), path));
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            foreach(PageData pageData in pageDataList)
            {
                Console.WriteLine($"Email: {pageData.Get("email")}");
                Console.WriteLine($"Page: { siteName + pageData.Get("sitepath")}");
            }

            Console.ReadKey();
        }
    }
}
