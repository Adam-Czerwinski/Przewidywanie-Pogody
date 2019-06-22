using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;
using System;

namespace WeatherForecast.UserControls
{
    public partial class HistoryUserControl : UserControl, IHistoryUserControl
    {

        public string[][] forecastDataIn
        {
            set
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < value.Length; i++)
                {
                    string[] row = new string[10];
                    for (int j = 0; j < 10; j++)
                    {
                        row[j] = value[i][j];
                    }
                    dataGridView1.Rows.Add(row);
                }
            }
        }

        public event Action Load;
       

        public HistoryUserControl()
        {
            InitializeComponent();
            
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            Load?.Invoke();
        }
    }
}
