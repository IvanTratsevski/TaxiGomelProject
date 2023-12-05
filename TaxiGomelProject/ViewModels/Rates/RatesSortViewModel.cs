using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.ViewModels.Rates
{
    public enum SortState
    {
        RateNameAsc,
        RateNameDesc,
        PriceAsc,
        PriceDesc,
    }
    public class RatesSortViewModel
    {
        public SortState RateNameSort { get; }
        public SortState PriceSort { get; }
        public SortState Current { get; }
        public RatesSortViewModel(SortState sortOrder)
        {
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            RateNameSort = sortOrder == SortState.RateNameAsc ? SortState.RateNameDesc : SortState.RateNameAsc;
            Current = sortOrder;
        }
    }
}
