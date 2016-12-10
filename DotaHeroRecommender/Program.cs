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
    class Program
    {
        static void Main(string[] args)
        {
            //HeroesPageReader reader = new HeroesPageReader();
            //reader.GetHeroesUrlList();

            //AddCounters();
            //RemoveHero("void");
            //CheckDb();
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

        private static void RemoveHero(string name)
        {
            using (var db = new DotaHeroContext())
            {
                var heroes = db.Heroes.Where(p => p.Name == name);

                foreach (var hero in heroes)
                {
                    db.Heroes.Remove(hero);
                }
                db.SaveChanges();
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
                var heroes = db.Heroes.ToArray();

                foreach (var h in heroes)
                {
                    var counters = reader.GetHeroCounterNamesByNames(h.Name);

                    if (counters == null) continue;

                    var hero = db.Heroes.Single(p => p.Name == h.Name);

                    hero.Counters = new CounterPicks();

                    if (counters.Count > 0)
                    {
                        var counter1Name = counters[0];
                        hero.Counters.Counter1 = db.Heroes.SingleOrDefault(r => r.Name == counter1Name);
                    }
                    if (counters.Count > 1)
                    {
                        var counter2Name = counters[1];
                        hero.Counters.Counter2 = db.Heroes.SingleOrDefault(r => r.Name == counter2Name);
                    }
                    if (counters.Count > 2)
                    {
                        var counter3Name = counters[2];
                        hero.Counters.Counter3 = db.Heroes.SingleOrDefault(r => r.Name == counter3Name);
                    }
                    if (counters.Count > 3)
                    {
                        var counter4Name = counters[3];
                        hero.Counters.Counter4 = db.Heroes.SingleOrDefault(r => r.Name == counter4Name);
                    }
                    if (counters.Count > 4)
                    {
                        var counter5Name = counters[4];
                        hero.Counters.Counter5 = db.Heroes.SingleOrDefault(r => r.Name == counter5Name);
                    }
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
                            select b.Counters;                

                Console.WriteLine("All heroes in the database:");
                foreach (var item in query)
                {
                    //Console.WriteLine(item.Name);
                    Console.WriteLine(item.Counter1.Name +
                        item.Counter2.Name + item.Counter3.Name
                        + item.Counter4.Name + item.Counter5.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
