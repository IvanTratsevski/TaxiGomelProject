using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.Models
{
    public partial class Car
    {
        public Car()
        {
            Calls = new HashSet<Call>();
            CarDrivers = new HashSet<CarDriver>();
            CarMechanics = new HashSet<CarMechanic>();
        }

        public int CarId { get; set; }
        [Display(Name = "Регистрационный номер")]
        [Required(ErrorMessage = "Регистрационный номер не указан")]
        [StringLength(11, MinimumLength = 7)]
        public string? RegistrationNumber { get; set; }
        [Display(Name = "Марка")]
        [Required(ErrorMessage = "Марка автомобиля не указан")]
        public int? CarModelId { get; set; }
        [Display(Name = "Номер корпуса")]
        [Required(ErrorMessage = "Номер корпуса не указан")]
        [StringLength(11, MinimumLength = 7)]
        public string? CarcaseNumber { get; set; }
        [Display(Name = "Номер двигателя")]
        [Required(ErrorMessage = "Номер двигателя не указан")]
        [StringLength(11, MinimumLength = 7)]
        public string? EngineNumber { get; set; }
        [Display(Name = "Дата производства")]
        [Required(ErrorMessage = "Дата производства не указана")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseYear { get; set; }
        [Display(Name = "Пробег")]
        [Required(ErrorMessage = "Пробег не указан")]
        [Range(1, 500000)]
        public int? Mileage { get; set; }
        [Display(Name = "Последний тех. осмотр")]
        [Required(ErrorMessage = "Дата тех. осмотра не указана")]
        [DataType(DataType.Date)]
        public DateTime? LastTi { get; set; }
        [Display(Name = "Особые отметки")]
        public string? SpecialMarks { get; set; }

        public virtual CarModel? CarModel { get; set; }
        public virtual ICollection<Call> Calls { get; set; }
        public virtual ICollection<CarDriver> CarDrivers { get; set; }
        public virtual ICollection<CarMechanic> CarMechanics { get; set; }
    }
}
