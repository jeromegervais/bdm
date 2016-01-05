using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDM.Common.Model
{
    public enum Order
    {
        [Display("Dernières")]
        Last,

        [Display("Meilleures")]
        TopWeek,

        [Display("Meilleures")]
        TopMonth,

        [Display("Meilleures")]
        TopYear,

        [Display("Aléatoires")]
        Random
    }
}
