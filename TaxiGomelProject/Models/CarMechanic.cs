using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.Models
{
    public partial class CarMechanic
    {
        public int CarMechanicId { get; set; }
        [Display(Name = "Регистрационный номер")]
        public int? CarId { get; set; }
        [Display(Name = "Механик")]
        public int? MechanicId { get; set; }

        public virtual Car? Car { get; set; }
        public virtual Employee? Mechanic { get; set; }
    }
}
