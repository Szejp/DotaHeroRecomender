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
    }
}
