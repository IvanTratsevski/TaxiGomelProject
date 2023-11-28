using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;
namespace TaxiGomelProject.Models
{
    public class EmployeesViewModel
    {
        public IEnumerable<Employee> Employees { get; }
        public PageViewModel PageViewModel { get; }
        public EmployeesViewModel(IEnumerable<Employee> employees, PageViewModel viewModel)
        {
            Employees = employees;
            PageViewModel = viewModel;
        }
    }
}
