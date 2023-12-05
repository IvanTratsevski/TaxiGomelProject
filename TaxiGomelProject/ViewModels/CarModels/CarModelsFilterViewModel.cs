using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using TaxiGomelProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaxiGomelProject.ViewModels.CarModels
{
    public class CarModelsFilterViewModel
    {
        public CarModelsFilterViewModel(decimal price, string rateName)
        {
            SelectedCarModelName = rateName;
            SelectedPrice = price;

        }
        public decimal SelectedPrice { get; }
        public string SelectedCarModelName { get; }
    }
}
