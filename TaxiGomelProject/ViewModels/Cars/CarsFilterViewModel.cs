using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using TaxiGomelProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaxiGomelProject.ViewModels.Cars 
{
    public class CarsFilterViewModel
    {
        public CarsFilterViewModel(List<CarModel> carModels, int carModelId, DateTime releaseYear, int mileage, DateTime lastTI,  string registartionNumber)
        {
            carModels.Insert(0, new CarModel
            {
                CarModelId = 0,
                ModelName = "Все"
            });
            CarModels = new SelectList(carModels, "CarModelId", "ModelName", carModelId);
            SelectedCarModel = carModelId;
            SelectedReleaseYear = releaseYear;
            SelectedLastTI = lastTI;
            SelectedMileage = mileage;
            SelectedRegistrationNumber = registartionNumber;

        }

        public SelectList CarModels { get; }
        public int SelectedCarModel { get; }
        public int SelectedMileage { get; }
        public string SelectedRegistrationNumber { get; }

        public DateTime SelectedReleaseYear { get; }
        public DateTime SelectedLastTI { get; }
    }
}
