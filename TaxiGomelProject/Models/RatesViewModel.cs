using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;
namespace TaxiGomelProject.Models
{
    public class RatesViewModel
    {
        public IEnumerable<Rate> Rates { get; }
        public PageViewModel PageViewModel { get; }
        public RatesViewModel(IEnumerable<Rate> rates, PageViewModel viewModel)
        {
            Rates = rates;
            PageViewModel = viewModel;
        }
    }
}
