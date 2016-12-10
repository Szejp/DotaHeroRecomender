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
        }


        private void CheckDb()
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
                    Console.WriteLine(item.Name);
                }

                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
