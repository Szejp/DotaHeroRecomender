using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DotaHeroRecommender.Model
{
    public class Counters
    {
        [Key]
        public int Id { get; set; }
        public virtual CounterPick Counter1 { get; set; }
        public virtual CounterPick Counter2 { get; set; }
        public virtual CounterPick Counter3 { get; set; }
        public virtual CounterPick Counter4 { get; set; }
        public virtual CounterPick Counter5 { get; set; }
    }
}
