using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;
namespace TaxiGomelProject.ViewModels.CarDrivers
{
    public class CarDriversViewModel
    {
        public IEnumerable<CarDriver> CarDrivers { get; }
        public PageViewModel PageViewModel { get; }
        public CarDriversViewModel(IEnumerable<CarDriver> carDrivers, PageViewModel viewModel)
        {
            CarDrivers = carDrivers;
            PageViewModel = viewModel;
        }
    }
}
