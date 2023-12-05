using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using TaxiGomelProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaxiGomelProject.ViewModels.Rates
{
    public class RatesFilterViewModel
    {
        public RatesFilterViewModel(decimal price, string rateName)
        {
            SelectedRateName = rateName;
            SelectedPrice = price;

        }
        public decimal SelectedPrice { get; }
        public string SelectedRateName { get; }
    }
}
