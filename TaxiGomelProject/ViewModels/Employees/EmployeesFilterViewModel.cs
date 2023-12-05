using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using TaxiGomelProject.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaxiGomelProject.ViewModels.Employees
{
    public class EmployeesFilterViewModel
    {
        public EmployeesFilterViewModel(List<Position> positions, int positionId, int age, int experience, string firstName,  string lastName)
        {
            positions.Insert(0, new Position
            {
                PositionId = 0,
                PositionName = "Все"
            });
            Positions = new SelectList(positions, "PositionId", "PositionName", positionId);
            SelectedPosition = positionId;
            SelectedAge = age;
            SelectedExperience = experience;
            SelectedFirstName = firstName;
            SelectedLastName = lastName;
        }

        public SelectList Positions { get; }
        public int SelectedPosition { get; }
        public int SelectedAge { get; }
        public int SelectedExperience { get; }
        public string SelectedFirstName { get; }
        public string SelectedLastName { get; }
    }
}
