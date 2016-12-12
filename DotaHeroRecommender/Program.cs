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

            var hero = _dotaContext.GetHeroByName("anti-mage");

            var counters = new List<CounterPick>();
            counters.Add(
                new CounterPick
                {
                    Hero = hero,
                    VotesCount = 3333
                });
            counters.Add(new CounterPick
            {
                Hero = hero,
                VotesCount = 1111
            });

            counters.Add(new CounterPick
            {
                Hero = hero,
                VotesCount = 22222
            });
            counters.Add(new CounterPick
            {
                Hero = hero,
                VotesCount = 4444
            });
            counters.Add(new CounterPick
            {
                Hero = hero,
                VotesCount = 5555
            });

            _dotaContext.AddCountersToHero(hero, counters);

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
                Console.Write(hero.Counters.Counter1.Hero.Name + " ");
            if (hero.Counters.Counter2 != null)
                Console.Write(hero.Counters.Counter2.Hero.Name + " ");
            if (hero.Counters.Counter3 != null)
                Console.Write(hero.Counters.Counter3.Hero.Name + " ");
            if (hero.Counters.Counter4 != null)
                Console.Write(hero.Counters.Counter4.Hero.Name + " ");
            if (hero.Counters.Counter5 != null)
                Console.Write(hero.Counters.Counter5.Hero.Name + " ");

            Console.WriteLine("\n");
        }

        private static void GetHeroCounters()
        {
            while (true)
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
        }
    }
}
