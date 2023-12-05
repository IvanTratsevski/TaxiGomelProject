using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;

namespace TaxiGomelProject.ViewModels.Employees
{
    public class EmployeesViewModel
    {
        public IEnumerable<Employee> Employees { get; }
        public PageViewModel PageViewModel { get; }
        public EmployeesFilterViewModel EmployeesFilterViewModel { get; }
        public EmployeesSortViewModel EmployeesSortViewModel { get; }
        public EmployeesViewModel(IEnumerable<Employee> employees, PageViewModel viewModel, EmployeesFilterViewModel employeesFilterViewModel, EmployeesSortViewModel employeesSortViewModel)
        {
            Employees = employees;
            PageViewModel = viewModel;
            EmployeesFilterViewModel = employeesFilterViewModel;
            EmployeesSortViewModel = employeesSortViewModel;
        }
    }
}
