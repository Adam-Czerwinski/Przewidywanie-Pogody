using System;
using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;
using System.Windows.Forms.DataVisualization.Charting;

namespace WeatherForecast.UserControls
{
    public partial class StatisticUserControl : UserControl, IStatisticUserControl
    {
        /// <summary>
        /// Zwraca obiekt ładujący dane do wykresu 
        /// </summary>
        public StatisticData StatisticData { get; }
        public string Year_ { get { return yearUpDown.Value.ToString(); } }
        public string Month_ { get { return monthUpDown.Value.ToString(); } }

        public event Action LoadYearly_;
        public event Action LoadMonthly_;
        public event Action LoadDaily_;
        public StatisticUserControl()
        {
            InitializeComponent();

            StatisticData = new StatisticData(chart1);

            #region Ładowanie rodzaji wykresu
            chartTypeComboBox.Items.AddRange(new object[]
            {
                SeriesChartType.Bubble, SeriesChartType.Column, SeriesChartType.Line
            });

            chartTypeComboBox.SelectedIndex = 2;
            #endregion

            #region Ładowanie rodzaji statystyk
            statisticTypeComboBox.Items.AddRange(new object[]
            {
                "Roczny", "Miesięczny", "Dzienny"
            });

            statisticTypeComboBox.SelectedIndex = 0;
            #endregion

        }

        private void StatisticUserControl_Load(object sender, EventArgs e)
        {
            LoadYearly_?.Invoke();
        }

        private void chartTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
                chart1.Series[i].ChartType = (SeriesChartType)chartTypeComboBox.SelectedItem;
        }

        private void statisticTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            StatisticData.Clear();

            if(statisticTypeComboBox.SelectedItem == statisticTypeComboBox.Items[0])
            {
                yearUpDown.Enabled = false;
                monthUpDown.Enabled = false;
                LoadYearly_?.Invoke();
            }
            if (statisticTypeComboBox.SelectedItem == statisticTypeComboBox.Items[1])
            {
                yearUpDown.Enabled = true;
                monthUpDown.Enabled = false;
                LoadMonthly_?.Invoke();
            }
            if (statisticTypeComboBox.SelectedItem == statisticTypeComboBox.Items[2])
            {
                yearUpDown.Enabled = true;
                monthUpDown.Enabled = true;
                LoadDaily_?.Invoke();
            }
        }

        private void yearUpDown_ValueChanged(object sender, EventArgs e)
        {
            statisticTypeComboBox_SelectedIndexChanged(sender, e);
        }

        private void monthUpDown_ValueChanged(object sender, EventArgs e)
        {
            statisticTypeComboBox_SelectedIndexChanged(sender, e);
        }
    }
}
