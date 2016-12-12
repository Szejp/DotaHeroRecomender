using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DotaHeroRecommender.Model
{
    public class CounterPick
    {
        [Key]
        public int Id { get; set; }
        public virtual Hero Hero { get; set; }
        public int VotesCount { get; set; }
    }
}
