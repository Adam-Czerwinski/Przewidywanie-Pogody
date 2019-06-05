using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherForecast.UserControls;

namespace WeatherForecast
{
    public partial class WeatherForecastForm : Form , IView
    {
        public ForecastUserControl ForecastUserControl { get; }
        public HistoryUserControl HistoryUserControl { get; }
        public StatisticUserControl StatisticUserControl { get; }
        public NeuralNetUserControl NeuralNetUserControl { get; }
        public AboutUserControl AboutUserControl { get; }

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
    }
}
