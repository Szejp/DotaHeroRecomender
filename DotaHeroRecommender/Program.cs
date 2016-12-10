using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotaHeroRecommender.Model;
using System.Data.Entity.Validation;

namespace DotaHeroRecommender
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new DotaHeroContext())
            {
                // Create and save a new Blog 
                Console.Write("Enter a name for a new Hero: ");
                var name = Console.ReadLine();


                try
                {
                    var hero = new Hero { Name = name };
                    db.Heroes.Add(hero);
                    db.SaveChanges();

                }
                catch (DbEntityValidationException excp)
                {
                    var exception = excp;
                }

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
