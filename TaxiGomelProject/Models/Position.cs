using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.Models
{
    public partial class Position
    {
        public Position()
        {
            Employees = new HashSet<Employee>();
        }

        public int PositionId { get; set; }
        [Display(Name = "Должность")]
        [Required(ErrorMessage = "Должность не указана")]
        [StringLength(30, MinimumLength = 7)]
        public string? PositionName { get; set; }
        [Display(Name = "Заработная плата")]
        [Required(ErrorMessage = "Заработная плата не указана")]
        [Range(1, 100000)]
        public decimal? Salary { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
