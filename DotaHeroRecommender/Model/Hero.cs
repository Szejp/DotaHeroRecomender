using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace DotaHeroRecommender.Model
{
    public class Hero
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual CounterPicks Counters { get; set; }
    }
}
