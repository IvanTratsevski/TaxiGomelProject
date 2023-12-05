using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.Models
{
    public partial class Rate
    {
        public Rate()
        {
            Calls = new HashSet<Call>();
        }

        public int RateId { get; set; }
        [Display(Name = "Тариф")]
        [Required(ErrorMessage = "Тариф не указан")]
        [StringLength(20, MinimumLength = 1)]
        public string? RateDescription { get; set; }
        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Стоимость не указана")]
        [Range(1, 100)]
        public decimal? RatePrice { get; set; }

        public virtual ICollection<Call> Calls { get; set; }
    }
}
