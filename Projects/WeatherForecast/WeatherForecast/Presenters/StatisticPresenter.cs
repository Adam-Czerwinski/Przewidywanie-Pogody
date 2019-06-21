using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserPresenters
{
    class StatisticPresenter
    {
        private IStatisticUserControl _statisticUserControl;
        private Model _model;

        public StatisticPresenter(IStatisticUserControl statisticUserControl, Model model)
        {
            _statisticUserControl = statisticUserControl;
            _model = model;
        }
    }
}
