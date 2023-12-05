using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using TaxiGomelProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaxiGomelProject.ViewModels.Calls
{
    public class CallsFilterViewModel
    {
        
        public CallsFilterViewModel(List<Car> cars, List<Employee> employees, List<Rate> rates, int carId, int employeeId, int rateId, DateTime callTime, string telephone, string startPosition, string endPosition)
        {
            cars.Insert(0, new Car
            {
                CarId = 0,
                RegistrationNumber = "Все"
            });
            Cars = new SelectList(cars, "CarId", "RegistrationNumber", carId);
            employees.Insert(0, new Employee
            {
                EmployeeId = 0,
                FirstName = "Все"
            });
            Employees = new SelectList(employees, "EmployeeId", "FirstName", employeeId);
            rates.Insert(0, new Rate
            {
                RateId = 0,
                RateDescription = "Все"
            });
            Rates = new SelectList(rates, "RateId", "RateDescription", rateId);
            SelectedCar = carId;
            SelectedRate = rateId;
            SelectedEmployee = employeeId;
            SelectedStartPosition = startPosition;
            SelectedTelephone = telephone;
            SelectedEndPosition = endPosition;
            SelectedCallTime = callTime;

        }

        public SelectList Cars { get; }
        public SelectList Rates { get; }
        public SelectList Employees { get; }
        public int SelectedCar { get; }
        public int SelectedRate { get; }
        public int SelectedEmployee { get; }
        public string SelectedStartPosition { get; }
        public string SelectedEndPosition { get; }
        public string SelectedTelephone { get; }

        public DateTime SelectedCallTime { get; }
    }
}
