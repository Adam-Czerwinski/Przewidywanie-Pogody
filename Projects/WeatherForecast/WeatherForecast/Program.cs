using System;
using System.Windows.Forms;

namespace WeatherForecast
{
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Model model = new Model();
            IWeatherForecast view = new WeatherForecastForm();
            WeatherForecastPresenter presenter = new WeatherForecastPresenter(view, model);
            Application.Run((Form)view);
        }
    }
}
