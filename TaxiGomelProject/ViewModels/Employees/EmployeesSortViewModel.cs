using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.ViewModels.Employees
{
    public enum SortState
    {
        FirstNameAsc,
        FirstNameDesc,
        LastNameAsc,
        LastNameDesc,
        AgeAsc,
        AgeDesc,
        PositionAsc,
        PositionDesc,
        ExperienceAsc,
        ExperienceDesc,
    }
    public class EmployeesSortViewModel
    {
        public SortState FirstNameSort { get; }
        public SortState LastNameSort { get; }
        public SortState AgeSort { get; }
        public SortState PositionSort { get; }
        public SortState ExperienceSort { get; }
        public SortState Current { get; }
        public EmployeesSortViewModel(SortState sortOrder)
        {
            FirstNameSort = sortOrder == SortState.FirstNameAsc ? SortState.FirstNameDesc : SortState.FirstNameAsc;
            LastNameSort = sortOrder == SortState.LastNameAsc ? SortState.LastNameDesc : SortState.LastNameAsc;
            AgeSort = sortOrder == SortState.AgeAsc ? SortState.AgeDesc : SortState.AgeAsc;
            PositionSort = sortOrder == SortState.PositionAsc ? SortState.PositionDesc : SortState.PositionAsc;
            ExperienceSort = sortOrder == SortState.ExperienceAsc ? SortState.ExperienceDesc : SortState.ExperienceAsc;
            Current = sortOrder;
        }
    }
}
