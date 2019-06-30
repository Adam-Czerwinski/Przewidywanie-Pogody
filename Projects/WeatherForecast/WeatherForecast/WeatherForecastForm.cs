using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast
{
    public partial class WeatherForecastForm : Form , IWeatherForecast
    {
        public IForecastUserControl ForecastUserControl { get; }
        public IHistoryUserControl HistoryUserControl { get; }
        public IStatisticUserControl StatisticUserControl { get; }
        public INeuralNetUserControl NeuralNetUserControl { get; }
        public IAboutUserControl AboutUserControl { get; }

        public WeatherForecastForm()
        {
            InitializeComponent();

            ForecastUserControl = forecastUserControl;
            HistoryUserControl = historyUserControl1;
            StatisticUserControl = statisticUserControl1;
            NeuralNetUserControl = neuralNetUserControl1;
            AboutUserControl = aboutUserControl1;
        }

    }
}
