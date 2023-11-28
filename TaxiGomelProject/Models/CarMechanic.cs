using System;
using System.Collections.Generic;

namespace TaxiGomelProject.Models
{
    public partial class CarMechanic
    {
        public int CarMechanicId { get; set; }
        public int? CarId { get; set; }
        public int? MechanicId { get; set; }

        public virtual Car? Car { get; set; }
        public virtual Employee? Mechanic { get; set; }
    }
}
