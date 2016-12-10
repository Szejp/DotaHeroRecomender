using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotaHeroRecommender.Model;
using System.Data.Entity.Validation;
using DotaHeroRecommender.Helper;

namespace DotaHeroRecommender
{
    class Program
    {
        static void Main(string[] args)
        {
            HeroesPageReader reader = new HeroesPageReader();
            reader.GetHeroesUrlList();

            //AddCounters();

            
            
            CheckDb();     
        }

        private static void RemoveHero(string name)
        {
            using (var db = new DotaHeroContext())
            {

            }
        }

        private static void AddHeroes()
        {
            HeroesPageReader reader = new HeroesPageReader();
            reader.GetHeroesUrlList();
            var names = reader.GetHeroNames();

            using (var db = new DotaHeroContext())
            {
                foreach (var n in names)
                {
                    Hero hero = new Hero { Name = n };
                    db.Heroes.Add(hero);
                }

                db.SaveChanges();
            }
        }

        private static void AddCounters()
        {
            HeroesPageReader reader = new HeroesPageReader();
            reader.GetHeroesUrlList();          

            using (var db = new DotaHeroContext())
            {

                var heroes = db.Heroes;

                foreach (var h in heroes)
                {
                    var counters = reader.GetHeroCounterNamesByNames(h.Name);
                    var hero = db.Heroes.SingleOrDefault(p => p.Name == h.Name);
                    hero.Counters.Counter1 = db.Heroes.SingleOrDefault(r => r.Name == counters[0]);
                    hero.Counters.Counter2 = db.Heroes.SingleOrDefault(r => r.Name == counters[1]);
                    hero.Counters.Counter3 = db.Heroes.SingleOrDefault(r => r.Name == counters[2]);
                    hero.Counters.Counter4 = db.Heroes.SingleOrDefault(r => r.Name == counters[3]);
                    hero.Counters.Counter5 = db.Heroes.SingleOrDefault(r => r.Name == counters[4]);
                    db.Heroes.Add(hero);
                }

                db.SaveChanges();
            }
        }


        private static void CheckDb()
        {
            using (var db = new DotaHeroContext())
            {
                // Display all Blogs from the database 
                var query = from b in db.Heroes
                            orderby b.Name
                            select b;

                Console.WriteLine("All heroes in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name + item.Counters.Counter1.Name +
                        item.Counters.Counter2.Name + item.Counters.Counter3.Name
                        + item.Counters.Counter4.Name + item.Counters.Counter5.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
