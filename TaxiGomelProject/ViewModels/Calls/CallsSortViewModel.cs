using System.ComponentModel.DataAnnotations;

namespace TaxiGomelProject.ViewModels.Calls
{
    public enum SortState
    {
        CallTimeAsc,
        CallTimeDesc,
        CarAsc, 
        CarDesc, 
        DispatcherAsc,
        DispatcherDesc,
        RateAsc,
        RateDesc,
    }
    public class CallsSortViewModel
    {
        /*public SortState CallTimeSort { get; }
        public SortState RegistrationNumberSort { get; }
        public SortState CarModelSort { get; }
        public SortState ReleaseYearSort { get; }
        public SortState MilleageSort { get; }
        public SortState LastTISort { get; }
        public SortState Current { get; }
        public CallsSortViewModel(SortState sortOrder)
        {
            CallTimeSort = sortOrder == SortState.CallTimeAsc ? SortState.CallTimeDesc : SortState.CallTimeAsc;
            RegistrationNumberSort = sortOrder == SortState.RegistrationNumberAsc ? SortState.RegistrationNumberDesc : SortState.RegistrationNumberAsc;
            CarModelSort = sortOrder == SortState.CarModelAsc ? SortState.CarModelDesc : SortState.CarModelAsc;
            ReleaseYearSort = sortOrder == SortState.ReleaseYearAsc ? SortState.ReleaseYearDesc : SortState.ReleaseYearAsc;
            MilleageSort = sortOrder == SortState.MilleageAsc ? SortState.MilleageDesc : SortState.MilleageAsc;
            LastTISort = sortOrder == SortState.LastTIAsc ? SortState.LastTIDesc : SortState.LastTIAsc;
            Current = sortOrder;
        }*/
        public SortState CallTimeSort { get; }
        public SortState CarSort { get; }
        public SortState RateSort { get; }
        public SortState DispatcherSort { get; }

        public SortState Current { get; }
        public CallsSortViewModel(SortState sortOrder)
        {
            CallTimeSort = sortOrder == SortState.CallTimeAsc ? SortState.CallTimeDesc : SortState.CallTimeAsc;
            CarSort = sortOrder == SortState.CarAsc ? SortState.CarDesc : SortState.CarAsc;
            RateSort = sortOrder == SortState.RateAsc ? SortState.RateDesc : SortState.RateAsc;
            DispatcherSort = sortOrder == SortState.DispatcherAsc ? SortState.DispatcherDesc : SortState.DispatcherAsc;
            Current = sortOrder;
        }
    }
}
