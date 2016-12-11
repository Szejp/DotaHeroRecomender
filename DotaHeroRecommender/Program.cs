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
        static DotaHeroContext dotaContext;

        static void Main(string[] args)
        {
            dotaContext = new DotaHeroContext();
            //HeroesPageReader reader = new HeroesPageReader();
            //reader.GetHeroesUrlList();

            AddCounters();
            //RemoveHero("void");
            //CheckDb();
            
            
        }

        private static void ReadHeroCounters()
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

                        if (hero.Counters.Counter1 != null)
                            Console.Write(hero.Counters.Counter1.Name + " ");
                        if (hero.Counters.Counter2 != null)
                            Console.Write(hero.Counters.Counter2.Name + " ");
                        if (hero.Counters.Counter3 != null)
                            Console.Write(hero.Counters.Counter3.Name + " ");
                        if (hero.Counters.Counter4 != null)
                            Console.Write(hero.Counters.Counter4.Name + " ");
                        if (hero.Counters.Counter5 != null)
                            Console.Write(hero.Counters.Counter5.Name + " ");
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

        private static void AddHeroes()
        {
            HeroesPageReader reader = new HeroesPageReader();
            reader.GetHeroesUrlList();
            var names = reader.GetHeroNames();

            foreach (var n in names)
            {
                Hero hero = new Hero { Name = n };
                dotaContext.AddHero(hero);
            }
        }

        private static void AddCounters()
        {
            HeroesPageReader reader = new HeroesPageReader();
            reader.GetHeroesUrlList();

            var heroes = dotaContext.Heroes.ToArray();

            foreach (var h in heroes)
            {
                var counters = reader.GetHeroCounterNamesByNames(h.Name);
                if (counters == null) continue;
                var hero = dotaContext.Heroes.Single(p => p.Name == h.Name);

                dotaContext.AddCountersToHero(hero, counters);
            }
        }
    }
}
