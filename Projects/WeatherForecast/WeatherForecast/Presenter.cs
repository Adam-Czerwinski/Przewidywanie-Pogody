using WeatherForecast.UserPresenters;

namespace WeatherForecast
{
    class Presenter
    {
        private IView _view;
        private Model _model;
        private ForecastPresenter _forecastPresenter;
        private HistoryPresenter _historyPresenter;
        private StatisticPresenter _statisticPresenter;
        private NeuralNetPresenter _neuralNetPresenter;
        private AboutPresenter _aboutPresenter;

        public Presenter(IView view, Model model)
        {
            _view = view;
            _model = model;

            _forecastPresenter = new ForecastPresenter(_view.ForecastUserControl, _model);
            _historyPresenter = new HistoryPresenter(_view.HistoryUserControl, _model);
            _statisticPresenter = new StatisticPresenter(_view.StatisticUserControl, _model);
            _neuralNetPresenter = new NeuralNetPresenter(_view.NeuralNetUserControl, _model);
            _aboutPresenter = new AboutPresenter(_view.AboutUserControl, _model);
        }

    }
}
