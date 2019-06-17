using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserPresenters
{
    class HistoryPresenter
    {
        private IHistoryUserControl _historyUserControl;
        private Model _model;

        public HistoryPresenter(IHistoryUserControl historyUserControl, Model model)
        {
            _historyUserControl = historyUserControl;
            _model = model;
        }
    }
}
