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
        string countersXPath = "//*[text()[contains(.,'weak against')]]/..//ul//li//h3/a";
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

            var heroesGrid = document.DocumentNode.SelectNodes(heroesGridXPath);

            foreach(var a in heroesGrid)
            {
                var heroPath = a.Attributes[0].Value.Replace("/dota", "");
                heroesUrls.Add(heroPath);
            }
        }

        public List<string> GetHeroCounterNamesByPath(string path)
        {
            string source = webClient.DownloadString(pageString + path);
            document.LoadHtml(source);
            return GenerateCountersNameList();
        }

        public List<string> GetHeroCounterNamesByNames(string name)
        {
            string source = webClient.DownloadString(pageString + "/" + name);
            document.LoadHtml(source);
            return GenerateCountersNameList();
        }

        private List<string> GenerateCountersNameList()
        {
            var heroCountersNodes = document.DocumentNode.SelectNodes(countersXPath);
            List<string> heroCountersNames = new List<string>();

            foreach (var node in heroCountersNodes)
            {
                heroCountersNames.Add(node.InnerText);
            }

            return heroCountersNames;
        }

        public void GetCounters()
        {
            foreach(var p in heroesUrls)
            {
                GetHeroCounterNamesByPath(p);
            }
        }

        public List<string> GetHeroNames()
        {
            if (heroesUrls == null)
            {
                GetHeroesUrlList();
            }

            return heroesUrls.Select(p => p.Replace("/", "")).ToList();
        }
    }
}
