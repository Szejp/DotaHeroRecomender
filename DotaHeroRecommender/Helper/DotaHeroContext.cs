using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DotaHeroRecommender.Model
{
    public class DotaHeroContext : DbContext
    {
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<CounterPicks> Counters { get; set; }

        public void RemoveHero(string name)
        {
            var heroes = Heroes.Where(p => p.Name == name);

            foreach (var hero in heroes)
            {
                Heroes.Remove(hero);
            }
            SaveChanges();
        }

        public void CheckDb()
        {
            // Display all Blogs from the database 
            var query = from b in Heroes
                        orderby b.Name
                        select b;

            Console.WriteLine("All heroes in the database:");
            foreach (var item in query)
            {
                //Console.WriteLine(item.Name);
                Console.WriteLine(item.Name);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
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
                    h.Counters = new CounterPicks();
                }
            }

            Heroes.Add(hero);
            SaveChanges();
        }

        public void AddCountersToHero(Hero hero, List<string> counters)
        {
            if (hero.Counters == null)
            {
                hero.Counters = new CounterPicks();
            }

            if (counters.Count > 0)
            {
                var counter1Name = counters[0];
                hero.Counters.Counter1 = Heroes.SingleOrDefault(r => r.Name == counter1Name);
            }
            if (counters.Count > 1)
            {
                var counter2Name = counters[1];
                hero.Counters.Counter2 = Heroes.SingleOrDefault(r => r.Name == counter2Name);
            }
            if (counters.Count > 2)
            {
                var counter3Name = counters[2];
                hero.Counters.Counter3 = Heroes.SingleOrDefault(r => r.Name == counter3Name);
            }
            if (counters.Count > 3)
            {
                var counter4Name = counters[3];
                hero.Counters.Counter4 = Heroes.SingleOrDefault(r => r.Name == counter4Name);
            }
            if (counters.Count > 4)
            {
                var counter5Name = counters[4];
                hero.Counters.Counter5 = Heroes.SingleOrDefault(r => r.Name == counter5Name);
            }

            SaveChanges();
        }
    }
}
