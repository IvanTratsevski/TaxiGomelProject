using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.ViewModels.Cars
{
    public enum SortState
    {
        CarModelAsc,
        CarModelDesc,
        ReleaseYearAsc,
        ReleaseYearDesc,
        MilleageAsc,
        MilleageDesc,
        LastTIAsc,
        LastTIDesc
    }
    public class CarsSortViewModel
    {
        public SortState CarModelSort { get; }
        public SortState ReleaseYearSort { get; }
        public SortState MilleageSort { get; }
        public SortState LastTISort { get; }
        public SortState Current { get; }
        public CarsSortViewModel(SortState sortOrder)
        {
            CarModelSort = sortOrder == SortState.CarModelAsc ? SortState.CarModelDesc : SortState.CarModelAsc;
            ReleaseYearSort = sortOrder == SortState.ReleaseYearAsc ? SortState.ReleaseYearDesc : SortState.ReleaseYearAsc;
            MilleageSort = sortOrder == SortState.MilleageAsc ? SortState.MilleageDesc : SortState.MilleageAsc;
            LastTISort = sortOrder == SortState.LastTIAsc ? SortState.LastTIDesc : SortState.LastTIAsc;
            Current = sortOrder;
        }
    }
}
