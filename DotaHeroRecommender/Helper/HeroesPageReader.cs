using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;    // PropertyInfo
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using System.Net;
using DotaHeroRecommender.Model;

namespace DotaHeroRecommender.Helper
{
    class HeroesPageReader
    {
        WebClient _webClient;
        HtmlDocument _document;
        DotaHeroContext _dotaContext;
        List<string> _heroesUrls;

        string _pageString = "https://mobacounter.com/dota";
        string _heroesGridXPath = "//div[@class='clearfix']//a[contains(@href,'dota/')]/@href";
        string _countersXPath = "//*[text()[contains(.,'weak against')]]/..//ul//li//h3/a";
        string _votesCountXPath = "../.././/button[@class='btn btn-xs btn-yellow total-votes']";

        public HeroesPageReader()
        {
            _webClient = new WebClient();
            _webClient.Encoding = Encoding.UTF8;
            _document = new HtmlDocument();
            _heroesUrls = new List<string>();
            _dotaContext = new DotaHeroContext();
        }

        public void GetHeroesUrlList()
        {
            string source = _webClient.DownloadString(_pageString);
            _document.LoadHtml(source);

            var heroesGrid = _document.DocumentNode.SelectNodes(_heroesGridXPath);

            foreach(var a in heroesGrid)
            {
                var heroPath = a.Attributes[0].Value.Replace("/dota", "");
                heroPath = heroPath.Replace(" ", "-");
                _heroesUrls.Add(heroPath);
            }
        }

        public List<CounterPick> GetHeroCountersByPath(string path)
        {
            string source = _webClient.DownloadString(_pageString + path);
            _document.LoadHtml(source);
            return GenerateHeroCountersList();
        }

        public List<CounterPick> GetHeroCounterNamesByNames(string name)
        {
            try
            {
                string source = _webClient.DownloadString(_pageString + "/" + name);
                _document.LoadHtml(source);
            }
            catch
            {
                return null;
            }
            return GenerateHeroCountersList();
        }

        private List<CounterPick> GenerateHeroCountersList()
        {
            var heroCountersNodes = _document.DocumentNode.SelectNodes(_countersXPath);

            if (heroCountersNodes == null) return null;

            List<CounterPick> heroCounters = new List<CounterPick>();
            foreach (var node in heroCountersNodes)
            {
                var heroName = node.InnerText.ToLower().Replace(" ", "-");
                var votesCount = node.SelectNodes(_votesCountXPath).SingleOrDefault().InnerText;
                var counter = new CounterPick();

                counter.Hero = _dotaContext.GetHeroByName(heroName);
                counter.VotesCount = Int32.Parse(votesCount);
                heroCounters.Add(counter);
            }
            return heroCounters;
        }

        public void GetCounters()
        {
            foreach(var p in _heroesUrls)
            {
                GetHeroCountersByPath(p);
            }
        }

        public List<string> GetHeroNames()
        {
            if (_heroesUrls == null)
            {
                GetHeroesUrlList();
            }

            return _heroesUrls.Select(p => p.Replace("/", "")).ToList();
        }
    }
}
