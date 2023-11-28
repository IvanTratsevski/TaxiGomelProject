using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;
namespace TaxiGomelProject.Models
{
    public class CarModelsViewModel
    {
        public IEnumerable<CarModel> CarModels { get; }
        public PageViewModel PageViewModel { get; }
        public CarModelsViewModel(IEnumerable<CarModel> carModels, PageViewModel viewModel)
        {
            CarModels = carModels;
            PageViewModel = viewModel;
        }
    }
}
