using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.Models
{
    public partial class Call
    {
        public int CallId { get; set; }
        [Display(Name = "Время вызова")]
        [Required(ErrorMessage = "Время вызова не указано")]
        public DateTime? CallTime { get; set; }
        [Display(Name = "Телефон клиента")]
        [Required(ErrorMessage = "Телефон клиента не указан")]
        [StringLength(11, MinimumLength = 7)]
        public string Telephone { get; set; }
        [Display(Name = "Адрес отправления")]
        [Required(ErrorMessage = "Адрес отправления не указан")]
        [StringLength(25, MinimumLength = 7)]
        public string? StartPosition { get; set; }
        [Display(Name = "Адрес доставки")]
        [Required(ErrorMessage = "Адрес доставки не указан")]
        [StringLength(25, MinimumLength = 7)]
        public string? EndPosition { get; set; }
        [Display(Name = "Тариф")]
        [Required(ErrorMessage = "Тариф не указан")]
        public int? RateId { get; set; }
        [Display(Name = "Автомобиль")]
        [Required(ErrorMessage = "Автомобиль не указан")]
        [StringLength(25, MinimumLength = 7)]
        public int? CarId { get; set; }
        [Display(Name = "Диспетчер")]
        [Required(ErrorMessage = "Диспетчер не указан")]
        [StringLength(25, MinimumLength = 7)]
        public int? DispatcherId { get; set; }

        public virtual Car? Car { get; set; }
        public virtual Employee? Dispatcher { get; set; }
        public virtual Rate? Rate { get; set; }
    }
}
