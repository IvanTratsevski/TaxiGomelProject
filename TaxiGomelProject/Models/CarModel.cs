using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.Models
{
    public partial class CarModel
    {
        public CarModel()
        {
            Cars = new HashSet<Car>();
        }

        public int CarModelId { get; set; }
        [Display(Name = "Марка")]
        [Required(ErrorMessage = "Марка не указана")]
        [StringLength(20, MinimumLength = 1)]
        public string? ModelName { get; set; }
        [Display(Name = "Технические характеристики")]
        [Required(ErrorMessage = "Характеристики не указаны")]
        [StringLength(100, MinimumLength = 10)]
        public string? TechStats { get; set; }
        [Display(Name = "Цена")]
        [Required(ErrorMessage = "Цена не указана")]
        [Range(1,100000)]
        public decimal? Price { get; set; }
        [Display(Name = "Спецификация")]
        [Required(ErrorMessage = "Спецификация не указана")]
        [StringLength(20, MinimumLength = 1)]
        public string? Specifications { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
