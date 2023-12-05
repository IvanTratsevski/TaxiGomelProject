using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;

namespace TaxiGomelProject.ViewModels.CarModels
{
    public class CarModelsViewModel
    {
        public IEnumerable<CarModel> CarModels { get; }
        public PageViewModel PageViewModel { get; }
        public CarModelsFilterViewModel CarModelsFilterViewModel { get; }
        public CarModelsSortViewModel CarModelsSortViewModel { get; }
        public CarModelsViewModel(IEnumerable<CarModel> carModels, PageViewModel viewModel, CarModelsFilterViewModel carModelsFilterViewModel, CarModelsSortViewModel carModelsSortViewModel)
        {
            CarModels = carModels;
            PageViewModel = viewModel;
            CarModelsFilterViewModel = carModelsFilterViewModel;
            CarModelsSortViewModel = carModelsSortViewModel;
        }
    }
}
