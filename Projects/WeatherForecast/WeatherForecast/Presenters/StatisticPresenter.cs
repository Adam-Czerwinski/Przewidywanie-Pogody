using WeatherForecast.UserControls.UserControlInterfaces;
using BusinessObject;

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

            _statisticUserControl.LoadYearly_ += SetYearly;
            _statisticUserControl.LoadMonthly_ += SetMonthly;
            _statisticUserControl.LoadDaily_ += SetDaily;
        }

        private void SetDaily()
        {
            string[][][] data = _model.GetStatistic(StatisticType.Daily, _statisticUserControl.Year_, _statisticUserControl.Month_);
            Set(data);
        }
        private void SetMonthly()
        {
            string[][][] data = _model.GetStatistic(StatisticType.Monthly, _statisticUserControl.Year_);
            Set(data);
        }
        private void SetYearly()
        {
            string[][][] data = _model.GetStatistic(StatisticType.Yearly);
            Set(data);
        }

        /// <summary>
        /// Przekazuje dane z modelu do obiektu pomocniczego klasy StatisticUserControl
        /// </summary>
        /// <param name="data"></param>
        private void Set(string[][][] data)
        {
            if (data != null)
                for (int i = 0; i < 3; i++)
                {
                    if (data[i] != null)
                        for (int j = 0; j < data[0][0].Length; j++)

                            _statisticUserControl.StatisticData[i] = new string[] { data[i][0][j], data[i][1][j] };
                }
        }
    }
}
