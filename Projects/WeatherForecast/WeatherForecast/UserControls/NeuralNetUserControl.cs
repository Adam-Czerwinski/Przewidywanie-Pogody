using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserControls
{
    public partial class NeuralNetUserControl : UserControl, INeuralNetUserControl
    {
        public string Path_ { set { if (value != null) axAcroPDF1.src = value; } }
        
        public NeuralNetUserControl()
        {
            InitializeComponent();
        }

        public event Action Load_;

        private void NeuralNetUserControl_Load(object sender, System.EventArgs e)
        {

            Load_?.Invoke();
            //axAcroPDF1.setZoom(100);
            //axAcroPDF1.setShowToolbar(false);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Load_?.Invoke();
        }
    }
}
