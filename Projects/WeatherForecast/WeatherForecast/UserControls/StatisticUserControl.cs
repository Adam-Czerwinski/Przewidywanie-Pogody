using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserControls
{
    public partial class StatisticUserControl : UserControl, IStatisticUserControl
    {
        public StatisticUserControl()
        {
            InitializeComponent();
        }
    }
}
