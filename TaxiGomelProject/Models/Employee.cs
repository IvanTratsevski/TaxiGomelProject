using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Calls = new HashSet<Call>();
            CarDrivers = new HashSet<CarDriver>();
            CarMechanics = new HashSet<CarMechanic>();
        }

        public int EmployeeId { get; set; }
        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя не указано")]
        [StringLength(20, MinimumLength = 2)]
        public string? FirstName { get; set; }
        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия не указана")]
        [StringLength(20, MinimumLength = 2)]
        public string? LastName { get; set; }
        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст не указан")]
        [Range(18, 100)]
        public int? Age { get; set; }
        [Display(Name = "Должность")]
        [Required(ErrorMessage = "Должность не указана")]
        public int? PositionId { get; set; }
        [Display(Name = "Стаж")]
        [Required(ErrorMessage = "Стаж не указана")]
        [Range(0, 100)]
        public int? Experience { get; set; }

        public virtual Position? Position { get; set; }
        public virtual ICollection<Call> Calls { get; set; }
        public virtual ICollection<CarDriver> CarDrivers { get; set; }
        public virtual ICollection<CarMechanic> CarMechanics { get; set; }
    }
}
