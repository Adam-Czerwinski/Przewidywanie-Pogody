using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserControls
{
    public partial class NeuralNetUserControl : UserControl, INeuralNetUserControl
    {
        public NeuralNetUserControl()
        {
            InitializeComponent();
        }
    }
}
