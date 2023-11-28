using System;
using System.Collections.Generic;

namespace TaxiGomelProject.Models
{
    public partial class CarDriver
    {
        public int CarDriverId { get; set; }
        public int? CarId { get; set; }
        public int? DriverId { get; set; }

        public virtual Car? Car { get; set; }
        public virtual Employee? Driver { get; set; }
    }
}
