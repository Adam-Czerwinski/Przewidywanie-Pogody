using System;
using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserControls
{
    public partial class AboutUserControl : UserControl, IAboutUserControl
    {

        public string Path_ { set { richTextBox1.SelectedRtf = value; } }

        public event Action Load_;

        public AboutUserControl()
        {
            InitializeComponent();
        }

        private void AboutUserControl_Load(object sender, System.EventArgs e)
        {
            Load_?.Invoke();
            richTextBox1.Enabled = false;
        }
    }
}
