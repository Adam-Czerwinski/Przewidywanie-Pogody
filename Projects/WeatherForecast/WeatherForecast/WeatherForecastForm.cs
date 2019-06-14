using System;
using System.Windows.Forms;
using WeatherForecast.UserControls;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast
{
    public partial class WeatherForecastForm : Form , IView
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
            HistoryUserControl = historyUserControl;
            StatisticUserControl = statisticUserControl;
            NeuralNetUserControl = neuralNetUserControl;
            AboutUserControl = aboutUserControl;
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            Form.ActiveForm.Close();
        }

        private void mimimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void forecastButton_Click(object sender, EventArgs e)
        {

        }
    }
}
