using System;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserControls
{
    public partial class ForecastUserControl : UserControl, IForecastUserControl
    {
        public string[] ForecastDataIn { get; }

        public string this[int indexer1, int indexer2, int indexer3] { set{ weatherDataLabels[indexer1, indexer2, indexer3].Text = value; } }

        private string[] region = { "N", "E", "S", "W", "C"};
        private string[] windDirection = { "C", "E", "N", "S", "W", "NE", "NW", "SE", "SW", "ENE", "ESE", "NNE", "NNW", "SSE", "SSW", "WNW", "WSW" };
        private Label[,,] weatherDataLabels;

        public event Action ForecastAction;

        public ForecastUserControl()
        {
            InitializeComponent();

            #region Initial GMap settings
            gMap.MapProvider = GMapProviders.GoogleMap;
            gMap.Position = new GMap.NET.PointLatLng(52, 19);
            gMap.MinZoom = 5;
            gMap.MaxZoom = 100;
            gMap.Zoom = 5;
            #endregion

            regionCBox.Items.AddRange(region);
            windDirectionCBox.Items.AddRange(windDirection);

            ForecastDataIn = new string[8];

            weatherDataLabels = new Label[4, 3, 6];
            #region Adding weather data labels to weatherDataLabes
            weatherDataLabels[0, 0, 0] = f1h6TemperatureLabel;
            weatherDataLabels[0, 0, 1] = f1h6HumidityLabel;
            weatherDataLabels[0, 0, 2] = f1h6WindSpeedLabel;
            weatherDataLabels[0, 0, 3] = f1h6WindDirectionLabel;
            weatherDataLabels[0, 0, 4] = f1h6CloudyLabel;
            weatherDataLabels[0, 0, 5] = f1h6VisibilityLabel;
            weatherDataLabels[0, 1, 0] = f1h12TemperatureLabel;
            weatherDataLabels[0, 1, 1] = f1h12HumidityLabel;
            weatherDataLabels[0, 1, 2] = f1h12WindSpeedLabel;
            weatherDataLabels[0, 1, 3] = f1h12WindDirectionLabel;
            weatherDataLabels[0, 1, 4] = f1h12CloudyLabel;
            weatherDataLabels[0, 1, 5] = f1h12VisibilityLabel;
            weatherDataLabels[0, 2, 0] = f1h18TemperatureLabel;
            weatherDataLabels[0, 2, 1] = f1h18HumidityLabel;
            weatherDataLabels[0, 2, 2] = f1h18WindSpeedLabel;
            weatherDataLabels[0, 2, 3] = f1h18WindDirectionLabel;
            weatherDataLabels[0, 2, 4] = f1h18CloudyLabel;
            weatherDataLabels[0, 2, 5] = f1h18VisibilityLabel;
            weatherDataLabels[1, 0, 0] = f2h6TemperatureLabel;
            weatherDataLabels[1, 0, 1] = f2h6HumidityLabel;
            weatherDataLabels[1, 0, 2] = f2h6WindSpeedLabel;
            weatherDataLabels[1, 0, 3] = f2h6WindDirectionLabel;
            weatherDataLabels[1, 0, 4] = f2h6CloudyLabel;
            weatherDataLabels[1, 0, 5] = f2h6VisibilityLabel;
            weatherDataLabels[1, 1, 0] = f2h12TemperatureLabel;
            weatherDataLabels[1, 1, 1] = f2h12HumidityLabel;
            weatherDataLabels[1, 1, 2] = f2h12WindSpeedLabel;
            weatherDataLabels[1, 1, 3] = f2h12WindDirectionLabel;
            weatherDataLabels[1, 1, 4] = f2h12CloudyLabel;
            weatherDataLabels[1, 1, 5] = f2h12VisibilityLabel;
            weatherDataLabels[1, 2, 0] = f2h18TemperatureLabel;
            weatherDataLabels[1, 2, 1] = f2h18HumidityLabel;
            weatherDataLabels[1, 2, 2] = f2h18WindSpeedLabel;
            weatherDataLabels[1, 2, 3] = f2h18WindDirectionLabel;
            weatherDataLabels[1, 2, 4] = f2h18CloudyLabel;
            weatherDataLabels[1, 2, 5] = f2h18VisibilityLabel;
            weatherDataLabels[2, 0, 0] = f3h6TemperatureLabel;
            weatherDataLabels[2, 0, 1] = f3h6HumidityLabel;
            weatherDataLabels[2, 0, 2] = f3h6WindSpeedLabel;
            weatherDataLabels[2, 0, 3] = f3h6WindDirectionLabel;
            weatherDataLabels[2, 0, 4] = f3h6CloudyLabel;
            weatherDataLabels[2, 0, 5] = f3h6VisibilityLabel;
            weatherDataLabels[2, 1, 0] = f3h12TemperatureLabel;
            weatherDataLabels[2, 1, 1] = f3h12HumidityLabel;
            weatherDataLabels[2, 1, 2] = f3h12WindSpeedLabel;
            weatherDataLabels[2, 1, 3] = f3h12WindDirectionLabel;
            weatherDataLabels[2, 1, 4] = f3h12CloudyLabel;
            weatherDataLabels[2, 1, 5] = f3h12VisibilityLabel;
            weatherDataLabels[2, 2, 0] = f3h18TemperatureLabel;
            weatherDataLabels[2, 2, 1] = f3h18HumidityLabel;
            weatherDataLabels[2, 2, 2] = f3h18WindSpeedLabel;
            weatherDataLabels[2, 2, 3] = f3h18WindDirectionLabel;
            weatherDataLabels[2, 2, 4] = f3h18CloudyLabel;
            weatherDataLabels[2, 2, 5] = f3h18VisibilityLabel;
            weatherDataLabels[3, 0, 0] = f4h6TemperatureLabel;
            weatherDataLabels[3, 0, 1] = f4h6HumidityLabel;
            weatherDataLabels[3, 0, 2] = f4h6WindSpeedLabel;
            weatherDataLabels[3, 0, 3] = f4h6WindDirectionLabel;
            weatherDataLabels[3, 0, 4] = f4h6CloudyLabel;
            weatherDataLabels[3, 0, 5] = f4h6VisibilityLabel;
            weatherDataLabels[3, 1, 0] = f4h12TemperatureLabel;
            weatherDataLabels[3, 1, 1] = f4h12HumidityLabel;
            weatherDataLabels[3, 1, 2] = f4h12WindSpeedLabel;
            weatherDataLabels[3, 1, 3] = f4h12WindDirectionLabel;
            weatherDataLabels[3, 1, 4] = f4h12CloudyLabel;
            weatherDataLabels[3, 1, 5] = f4h12VisibilityLabel;
            weatherDataLabels[3, 2, 0] = f4h18TemperatureLabel;
            weatherDataLabels[3, 2, 1] = f4h18HumidityLabel;
            weatherDataLabels[3, 2, 2] = f4h18WindSpeedLabel;
            weatherDataLabels[3, 2, 3] = f4h18WindDirectionLabel;
            weatherDataLabels[3, 2, 4] = f4h18CloudyLabel;
            weatherDataLabels[3, 2, 5] = f4h18VisibilityLabel;
            #endregion

            this.BringToFront();
        }

        private void forecastInButton_Click(object sender, EventArgs e)
        {
            bool isValid = true;

            #region Data validation
            if (cityTextBox.Text == "")
            {
                isValid = false;
            }
            if (regionCBox.Text == "")
            {
                isValid = false;
            }
            if (windDirectionCBox.Text == "")
            {
                isValid = false;
            }
            #endregion

            if (isValid)
            {
                #region Attributing input data to properties
                ForecastDataIn[0] = cityTextBox.Text;
                ForecastDataIn[1] = regionCBox.Text;
                ForecastDataIn[2] = temperatureNUpDown.Value.ToString();
                ForecastDataIn[3] = humidityBar.Value.ToString();
                ForecastDataIn[4] = windDirectionCBox.Text;
                ForecastDataIn[5] = windSpeedNUpDown.Value.ToString();
                ForecastDataIn[6] = cloudyBar.Value.ToString();
                ForecastDataIn[7] = visibilityBar.Value.ToString();
                #endregion

                if (ForecastAction != null)
                    ForecastAction.Invoke();

                #region Map attribut
                gMap.Zoom = 6;
                switch (ForecastDataIn[1])
                {
                    case "N":
                        gMap.Position = new GMap.NET.PointLatLng(53.5, 19);
                        break;
                    case "E":
                        gMap.Position = new GMap.NET.PointLatLng(52, 21);
                        break;
                    case "S":
                        gMap.Position = new GMap.NET.PointLatLng(50.5, 19);
                        break;
                    case "W":
                        gMap.Position = new GMap.NET.PointLatLng(52, 17);
                        break;
                    default:
                        gMap.Position = new GMap.NET.PointLatLng(52, 19);
                        break;
                        #endregion
                }
            }

        }

    }
}
