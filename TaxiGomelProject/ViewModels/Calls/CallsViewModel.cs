using TaxiGomelProject.ViewModels.Calls;
using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;
using TaxiGomelProject.ViewModels.Calls;

namespace TaxiGomelProject.ViewModels.Calls
{
    public class CallsViewModel
    {
        public IEnumerable<Call> Calls { get; }
        public PageViewModel PageViewModel { get; }
        public CallsFilterViewModel CallsFilterViewModel { get; }
        public CallsSortViewModel CallsSortViewModel { get; }
        public CallsViewModel(IEnumerable<Call> calls, PageViewModel viewModel, CallsFilterViewModel callsFilterViewModel, CallsSortViewModel callsSortViewModel)
        {
            Calls = calls;
            PageViewModel = viewModel;
            CallsFilterViewModel = callsFilterViewModel;
            CallsSortViewModel = callsSortViewModel;
        }
    }
}
