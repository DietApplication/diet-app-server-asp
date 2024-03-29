﻿using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Mealtake
    {
        public Mealtake()
        {
            Individualrecipes = new HashSet<Individualrecipe>();
        }

        public int Idmealtake { get; set; }
        public int Idday { get; set; }
        public string Time { get; set; }
        public bool? Isfollowed { get; set; }
        public decimal Proportion { get; set; }

        public virtual Day IddayNavigation { get; set; }
        public virtual ICollection<Individualrecipe> Individualrecipes { get; set; }
    }
}
