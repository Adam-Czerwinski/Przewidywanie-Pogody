using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserControls
{
    public partial class HistoryUserControl : UserControl, IHistoryUserControl
    {
        public HistoryUserControl()
        {
            InitializeComponent();

            this.BringToFront();
        }
    }
}
