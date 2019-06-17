using System.Windows.Forms;

namespace WeatherForecast.UserControls
{
    public class ForecastData
    {
        private Label[,,] weatherDataLabels;
        public string this[int indexer1, int indexer2, int indexer3] { set { weatherDataLabels[indexer1, indexer2, indexer3].Text = value; } }


        public ForecastData(Label[,,] data)
        {
            weatherDataLabels = data;
        }

    }
}
