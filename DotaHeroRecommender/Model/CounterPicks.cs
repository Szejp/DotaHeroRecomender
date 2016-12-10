using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DotaHeroRecommender.Model
{
    public class CounterPicks
    {
        [Key]
        public int Id { get; set; }
        public virtual Hero Counter1 { get; set; }
        public virtual Hero Counter2 { get; set; }
        public virtual Hero Counter3 { get; set; }
        public virtual Hero Counter4 { get; set; }
        public virtual Hero Counter5 { get; set; }
    }
}
