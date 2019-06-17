using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserControls
{
    public partial class AboutUserControl : UserControl, IAboutUserControl
    {
        public AboutUserControl()
        {
            InitializeComponent();

            this.BringToFront();
        }
    }
}
