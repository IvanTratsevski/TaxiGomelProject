using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.ViewModels.CarModels
{
    public enum SortState
    {
        CarModelNameAsc,
        CarModelNameDesc,
        PriceAsc,
        PriceDesc,
        SpecificationAsc,
        SpecificationDesc,
    }
    public class CarModelsSortViewModel
    {
        public SortState CarModelNameSort { get; }
        public SortState PriceSort { get; }
        public SortState SpecificationSort { get; }
        public SortState Current { get; }
        public CarModelsSortViewModel(SortState sortOrder)
        {
            PriceSort = sortOrder == SortState.PriceAsc ? SortState.PriceDesc : SortState.PriceAsc;
            CarModelNameSort = sortOrder == SortState.CarModelNameAsc ? SortState.CarModelNameDesc : SortState.CarModelNameAsc;
            SpecificationSort = sortOrder == SortState.SpecificationAsc ? SortState.SpecificationDesc : SortState.SpecificationAsc;
            Current = sortOrder;
        }
    }
}
