using System.Windows.Forms.DataVisualization.Charting;

namespace WeatherForecast.UserControls
{
    public class StatisticData
    {
        private Chart _chart;
        public string[] this[int indexer] {
            set
            {
                _chart.Series[indexer].Points.AddXY(value[0], double.Parse(value[1]));
            }
        }

        public void Clear()
        {
            foreach (var s in _chart.Series)
                s.Points.Clear();
        }

        public StatisticData(Chart chart)
        {
            _chart = chart;
        }
    }
}
