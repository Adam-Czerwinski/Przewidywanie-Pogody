using System;
using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserControls
{
    public partial class NeuralNetUserControl : UserControl, INeuralNetUserControl
    {
        public string Path_ { set { axAcroPDF1.src = value; } }

        public event Action Load_;

        public NeuralNetUserControl()
        {
            InitializeComponent();
        }

        private void NeuralNetUserControl_Load(object sender, System.EventArgs e)
        {

            Load_?.Invoke();
            axAcroPDF1.setZoom(100);
            axAcroPDF1.setShowToolbar(false);
            axAcroPDF1.Focus();

        }

    }
}
