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

            _historyUserControl.Load_ += Load;
        }

        private void Load()
        {
            _historyUserControl.forecastDataIn = _model.LoadData();

        }
    }
}
