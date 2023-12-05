using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;

namespace TaxiGomelProject.ViewModels.Rates
{
    public class RatesViewModel
    {
        public IEnumerable<Rate> Rates { get; }
        public PageViewModel PageViewModel { get; }
        public RatesFilterViewModel RatesFilterViewModel { get; }
        public RatesSortViewModel RatesSortViewModel { get; }
        public RatesViewModel(IEnumerable<Rate> rates, PageViewModel viewModel, RatesFilterViewModel ratesFilter, RatesSortViewModel ratesSort)
        {
            Rates = rates;
            PageViewModel = viewModel;
            RatesFilterViewModel = ratesFilter;
            RatesSortViewModel = ratesSort;
        }
    }
}
