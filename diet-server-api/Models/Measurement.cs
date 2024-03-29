﻿using System;
using System.Collections.Generic;

#nullable disable

namespace diet_server_api.Models
{
    public partial class Measurement
    {
        public int Idmeasurement { get; set; }
        public int Idpatient { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public DateTime Date { get; set; }
        public decimal Hipcircumference { get; set; }
        public decimal Waistcircumference { get; set; }
        public decimal? Bicepscircumference { get; set; }
        public decimal? Chestcircumference { get; set; }
        public decimal? Thighcircumference { get; set; }
        public decimal? Calfcircumference { get; set; }
        public decimal? Waistlowercircumference { get; set; }
        public string Whomeasured { get; set; }

        public virtual Patient IdpatientNavigation { get; set; }
    }
}
