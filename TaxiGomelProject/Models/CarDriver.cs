using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.Models
{
    public partial class CarDriver
    {
        [Key]
        public int CarDriverId { get; set; }
        [Display(Name = "Регистрационный номер")]
        public int? CarId { get; set; }
        [Display(Name = "Водитель")]
        public int? DriverId { get; set; }

        public virtual Car? Car { get; set; }
        public virtual Employee? Driver { get; set; }
    }
}
