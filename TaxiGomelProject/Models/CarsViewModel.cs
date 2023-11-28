using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;
namespace TaxiGomelProject.Models
{
    public class CarsViewModel
    {
        public IEnumerable<Car> Cars { get; }
        public PageViewModel PageViewModel { get; }
        public CarsViewModel(IEnumerable<Car> cars, PageViewModel viewModel)
        {
            Cars = cars;
            PageViewModel = viewModel;
        }
    }
}
