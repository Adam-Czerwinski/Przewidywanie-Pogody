using System.IO;
using System.Text;
using System.Windows.Forms;
using WeatherForecast.UserControls.UserControlInterfaces;

namespace WeatherForecast.UserControls
{
    public partial class NeuralNetUserControl : UserControl, INeuralNetUserControl
    {

        private static readonly Encoding LocalEncoding = Encoding.UTF8;
        public NeuralNetUserControl()
        {
            InitializeComponent();
        }

        private void NeuralNetUserControl_Load(object sender, System.EventArgs e)
        {
            byte[] da = Properties.Resources.Dokumentacja_SSI_3;

            string tempName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName() + ".pdf");
            File.WriteAllBytes(tempName, da);
            axAcroPDF1.src = tempName;
            axAcroPDF1.setZoom(100);
            axAcroPDF1.setShowToolbar(false);
            
        }
    }
}
