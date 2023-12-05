using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using TaxiGomelProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaxiGomelProject.ViewModels.Positions
{
    public class PositionsFilterViewModel
    {
        public PositionsFilterViewModel(decimal salary, string positionName)
        {
            SelectedPositionName = positionName;
            SelectedSalary = salary;

        }
        public decimal SelectedSalary { get; }
        public string SelectedPositionName { get; }
    }
}
