using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;    // PropertyInfo
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Net;

namespace DotaHeroRecommender.Helper
{
    class HeroesPageReader
    {
        WebClient webClient;
        HtmlDocument document;
        string pageString = "https://mobacounter.com/dota";
        string heroesGridXPath = "//div[@class='clearfix']//a[contains(@href,'dota/')]/@href";
        public string dataContent { get; private set; }

        List<string> heroesUrls;

        public HeroesPageReader()
        {
            webClient = new WebClient();
            webClient.Encoding = Encoding.UTF8;
            document = new HtmlDocument();
            heroesUrls = new List<string>();
        }

        public void GetHeroesUrlList()
        {
            string source = webClient.DownloadString(pageString);
            document.LoadHtml(source);

            var grid = document.DocumentNode.SelectNodes(heroesGridXPath);

            foreach(var a in grid)
            {
                heroesUrls.Add(a.Attributes[0].Value);
            }
        }
    }
}
