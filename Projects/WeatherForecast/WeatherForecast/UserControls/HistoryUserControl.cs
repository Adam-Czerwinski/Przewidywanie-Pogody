using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserControls
{
    public partial class HistoryUserControl : UserControl, IHistoryUserControl
    {
        public HistoryUserControl()
        {
            InitializeComponent();
        }
    }
}
