using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using DotaHeroRecommender.Model;

namespace DotaHeroRecommender.Helper
{
    public class DotaHeroContext : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Counters> Counters { get; set; }
        public DbSet<CounterPick> SingleCounter { get; set; }

        public void RemoveHero(string name)
        {
            var heroes = Heroes.Where(p => p.Name == name);

            foreach (var hero in heroes)
            {
                Heroes.Remove(hero);
            }
            SaveChanges();
        }

        public void AddHero(Hero hero)
        {
            try
            {
                Heroes.SingleOrDefault(p => p.Name == hero.Name);
            }
            catch
            {
                Hero h = new Hero { Name = hero.Name };

                if(hero.Counters != null)
                {
                    h.Counters = hero.Counters;
                }
                else
                {
                    h.Counters = new Counters();
                }
            }

            Heroes.Add(hero);
            SaveChanges();
        }

        public Hero GetHeroByName(string name)
        {
            return Heroes.Where(p => p.Name == name).SingleOrDefault();
        }

        public void AddCountersToHero(Hero hero, List<CounterPick> counters)
        {
            if (hero.Counters == null)
            {
                hero.Counters = new Counters();
            }

            if (counters.Count > 0)
            {
                var counter1 = counters[0];
                hero.Counters.Counter1 = AddVotesAndNameToCounterPick(hero.Counters.Counter1, counter1.Hero.Name, counter1.VotesCount);                
            }
            if (counters.Count > 1)
            {
                var counter2 = counters[1];
                hero.Counters.Counter2 = AddVotesAndNameToCounterPick(hero.Counters.Counter2, counter2.Hero.Name, counter2.VotesCount);
            }
            if (counters.Count > 2)
            {
                var counter3 = counters[2];
                hero.Counters.Counter3 = AddVotesAndNameToCounterPick(hero.Counters.Counter3, counter3.Hero.Name, counter3.VotesCount);
            }
            if (counters.Count > 3)
            {
                var counter4 = counters[3];
                hero.Counters.Counter4 = AddVotesAndNameToCounterPick(hero.Counters.Counter4, counter4.Hero.Name, counter4.VotesCount);
            }
            if (counters.Count > 4)
            {
                var counter5 = counters[4];
                hero.Counters.Counter5 = AddVotesAndNameToCounterPick(hero.Counters.Counter5, counter5.Hero.Name, counter5.VotesCount);
            }

            SaveChanges();
        }

        private CounterPick AddVotesAndNameToCounterPick(CounterPick counterPick, string name, int votesCount)
        {
            if (counterPick == null)
            {
                counterPick = new CounterPick
                {
                    Hero = Heroes.SingleOrDefault(r => r.Name == name),
                    VotesCount = votesCount
                };
            }
            else
            {
                counterPick.Hero = Heroes.SingleOrDefault(r => r.Name == name);
                counterPick.VotesCount = votesCount;
            }

            return counterPick;
        }
    }
}
