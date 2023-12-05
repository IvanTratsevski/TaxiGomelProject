using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;

namespace TaxiGomelProject.ViewModels.Cars
{
    public class CarsViewModel
    {
        public IEnumerable<Car> Cars { get; }
        public PageViewModel PageViewModel { get; }
        public CarsFilterViewModel CarsFilterViewModel { get; }
        public CarsSortViewModel CarsSortViewModel { get; }
        public CarsViewModel(IEnumerable<Car> cars, PageViewModel viewModel, CarsFilterViewModel filterViewModel, CarsSortViewModel sortViewModel)
        {
            Cars = cars;
            PageViewModel = viewModel;
            CarsFilterViewModel = filterViewModel;
            CarsSortViewModel = sortViewModel;
        }
    }
}
