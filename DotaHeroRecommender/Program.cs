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
