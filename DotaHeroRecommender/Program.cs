using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotaHeroRecommender.Model;
using System.Data.Entity.Validation;
using DotaHeroRecommender.Helper;
using System.Data.Entity.Core;


namespace DotaHeroRecommender
{
    static class Program
    {
        static DotaHeroContext _dotaContext;

        static void Main(string[] args)
        {
            _dotaContext = new DotaHeroContext();
            //HeroesPageReader reader = new HeroesPageReader();
            //reader.GetHeroesUrlList();

            //HeroManager.AddHeroes();
            //HeroManager.AddCountersFromWebside();
            while(true)  GetHeroesCounters();
            
            //_dotaContext.AddCountersToHero(hero, counters);
            //CheckDb();           
        }

        private static void CheckDb()
        {
            var query = _dotaContext.Heroes.OrderBy(p => p.Name).ToArray();

            Console.WriteLine("All heroes in the database:");
            foreach (var item in query)
            {
                //Console.WriteLine(item.Name);
                Console.WriteLine(item.Name + " ");
                WriteCountersForHero(item);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static void WriteCountersForHero(Hero hero)
        {
            if (hero.Counters == null) return;
            if (hero.Counters.Counter1 != null)
                WriteHeroNameAndVotesCount(hero.Counters.Counter1);
            if (hero.Counters.Counter2 != null)
                WriteHeroNameAndVotesCount(hero.Counters.Counter2);
            if (hero.Counters.Counter3 != null)
                WriteHeroNameAndVotesCount(hero.Counters.Counter3);
            if (hero.Counters.Counter4 != null)
                WriteHeroNameAndVotesCount(hero.Counters.Counter4);
            if (hero.Counters.Counter5 != null)
                WriteHeroNameAndVotesCount(hero.Counters.Counter5);

            Console.WriteLine("\n");
        }
        
        private static void WriteHeroNameAndVotesCount(CounterPick counterPick)
        {
            Console.Write(counterPick.Hero.Name + " (" + counterPick.VotesCount + ")" + " ");
        }

        private static void GetHeroCounters()
        {
                Console.Write("enter hero name: ");
                string heroName = Console.ReadLine().Replace(" ", "-");
                using (var db = new DotaHeroContext())
                {
                    try
                    {
                        var hero = db.Heroes.SingleOrDefault(p => p.Name == heroName);
                        WriteCountersForHero(hero);
                    }
                    catch
                    {
                        Console.Write("There is no hero with that name.");
                    }
                }
                Console.ReadKey();
                Console.Clear();          
        }

        private static void GetHeroesCounters()
        {
            Console.Write("enter heroes names: ");
            var heroesNames = Console.ReadLine().Split(' ');

            var heroNames= HeroManager.GerHeroesCounterPicks(heroesNames);

            foreach (string n in heroNames)
            {
                Console.Write(n + " ");
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}
