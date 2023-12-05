using System.Diagnostics.Metrics;
using TaxiGomelProject.Models;
namespace TaxiGomelProject.ViewModels.Positions
{
    public class PositionsViewModel
    {
        public IEnumerable<Position> Positions { get; }
        public PageViewModel PageViewModel { get; }
        public PositionsFilterViewModel PositionsFilterViewModel { get; }
        public PositionsSortViewModel PositionsSortViewModel { get; }
        public PositionsViewModel(IEnumerable<Position> positions, PageViewModel viewModel, PositionsFilterViewModel positionsFilterViewModel, PositionsSortViewModel positionsSortViewModel)
        {
            Positions = positions;
            PageViewModel = viewModel;
            PositionsFilterViewModel = positionsFilterViewModel;
            PositionsSortViewModel = positionsSortViewModel;
        }
    }
}
