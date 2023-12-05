using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.ViewModels.Positions
{
    public enum SortState
    {
        PositionNameAsc,
        PositionNameDesc,
        SalaryAsc,
        SalaryDesc,
    }
    public class PositionsSortViewModel
    {
        public SortState PositionNameSort { get; }
        public SortState SalarySort { get; }
        public SortState Current { get; }
        public PositionsSortViewModel(SortState sortOrder)
        {
            SalarySort = sortOrder == SortState.SalaryAsc ? SortState.SalaryDesc : SortState.SalaryAsc;
            PositionNameSort = sortOrder == SortState.PositionNameAsc ? SortState.PositionNameDesc : SortState.PositionNameAsc;
            Current = sortOrder;
        }
    }
}
