using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast
{
    interface IView
    {
        IForecastUserControl ForecastUserControl { get; }
        IHistoryUserControl HistoryUserControl { get; }
        IStatisticUserControl StatisticUserControl { get; }
        INeuralNetUserControl NeuralNetUserControl { get; }
        IAboutUserControl AboutUserControl { get; }
    }
}
