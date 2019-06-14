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
            IView view = new WeatherForecastForm();
            Presenter presenter = new Presenter(view, model);
            Application.Run((Form)view);
        }
    }
}
