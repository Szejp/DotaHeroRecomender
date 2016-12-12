using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotaHeroRecommender.Model;

namespace DotaHeroRecommender.Helper
{
    public static class HeroManager
    {
        static DotaHeroContext _dotaContext = new DotaHeroContext();
        public static void AddHeroes()
        {
            HeroesPageReader reader = new HeroesPageReader();
            reader.GetHeroesUrlList();
            var names = reader.GetHeroNames();

            foreach (var n in names)
            {
                Hero hero = new Hero { Name = n };
                _dotaContext.AddHero(hero);
            }
        }
        public static void AddCountersFromWebside()
        {
            HeroesPageReader reader = new HeroesPageReader();
            reader.GetHeroesUrlList();

            var heroes = _dotaContext.Heroes.ToArray();

            foreach (var h in heroes)
            {
                var counters = reader.GetHeroCounterNamesByNames(h.Name);
                if (counters == null) continue;
                var hero = _dotaContext.Heroes.Single(p => p.Name == h.Name);

                _dotaContext.AddCountersToHero(hero, counters);
            }
        }

        public static List<CounterPick> GetHeroCounterPicks(string heroName)
        {
            var hero = _dotaContext.GetHeroByName(heroName);
            var counters = hero.Counters;
            var counterPicks = new List<CounterPick>();

            counterPicks.Add(counters.Counter1);
            counterPicks.Add(counters.Counter2);
            counterPicks.Add(counters.Counter3);
            counterPicks.Add(counters.Counter4);
            counterPicks.Add(counters.Counter5);

            return counterPicks;
        }
    }
}
