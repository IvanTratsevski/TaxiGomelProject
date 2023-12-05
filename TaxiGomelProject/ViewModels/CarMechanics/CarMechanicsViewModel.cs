using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;
namespace TaxiGomelProject.ViewModels.CarMechanics
{
    public class CarMechanicsViewModel
    {
        public IEnumerable<CarMechanic> CarMechanics { get; }
        public PageViewModel PageViewModel { get; }
        public CarMechanicsViewModel(IEnumerable<CarMechanic> carMechanics, PageViewModel viewModel)
        {
            CarMechanics = carMechanics;
            PageViewModel = viewModel;
        }
    }
}
