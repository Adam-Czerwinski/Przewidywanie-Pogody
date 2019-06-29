using WeatherForecast.UserControls;
namespace WeatherForecast
{
    partial class WeatherForecastForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.forecastUserControl = new WeatherForecast.UserControls.ForecastUserControl();
            this.historyUserControl1 = new WeatherForecast.UserControls.HistoryUserControl();
            this.statisticUserControl1 = new WeatherForecast.UserControls.StatisticUserControl();
            this.neuralNetUserControl1 = new WeatherForecast.UserControls.NeuralNetUserControl();
            this.aboutUserControl1 = new WeatherForecast.UserControls.AboutUserControl();
            this.tabPage5.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage5
            // 
            this.tabPage5.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage5.Controls.Add(this.aboutUserControl1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1158, 617);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "O aplikacji";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage4.Controls.Add(this.neuralNetUserControl1);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(1158, 617);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Sieć neuronowa";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage3.Controls.Add(this.statisticUserControl1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1158, 617);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Statystyki";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage2.Controls.Add(this.historyUserControl1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1158, 617);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Historia";
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tabPage1.Controls.Add(this.forecastUserControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1158, 617);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Prognoza";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage1);
            this.tabControl.Controls.Add(this.tabPage2);
            this.tabControl.Controls.Add(this.tabPage3);
            this.tabControl.Controls.Add(this.tabPage4);
            this.tabControl.Controls.Add(this.tabPage5);
            this.tabControl.Location = new System.Drawing.Point(9, 9);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1166, 643);
            this.tabControl.TabIndex = 1;
            // 
            // forecastUserControl
            // 
            this.forecastUserControl.BackColor = System.Drawing.SystemColors.ControlLight;
            this.forecastUserControl.Location = new System.Drawing.Point(4, 4);
            this.forecastUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.forecastUserControl.Name = "forecastUserControl";
            this.forecastUserControl.Size = new System.Drawing.Size(1150, 620);
            this.forecastUserControl.TabIndex = 0;
            // 
            // historyUserControl1
            // 
            this.historyUserControl1.Location = new System.Drawing.Point(3, 0);
            this.historyUserControl1.Margin = new System.Windows.Forms.Padding(0);
            this.historyUserControl1.MaximumSize = new System.Drawing.Size(1150, 620);
            this.historyUserControl1.MinimumSize = new System.Drawing.Size(1150, 620);
            this.historyUserControl1.Name = "historyUserControl1";
            this.historyUserControl1.Size = new System.Drawing.Size(1150, 620);
            this.historyUserControl1.TabIndex = 0;
            // 
            // statisticUserControl1
            // 
            this.statisticUserControl1.Location = new System.Drawing.Point(3, 0);
            this.statisticUserControl1.Margin = new System.Windows.Forms.Padding(0);
            this.statisticUserControl1.MaximumSize = new System.Drawing.Size(1200, 650);
            this.statisticUserControl1.MinimumSize = new System.Drawing.Size(1200, 650);
            this.statisticUserControl1.Name = "statisticUserControl1";
            this.statisticUserControl1.Size = new System.Drawing.Size(1200, 650);
            this.statisticUserControl1.TabIndex = 0;
            // 
            // neuralNetUserControl1
            // 
            this.neuralNetUserControl1.Location = new System.Drawing.Point(0, 0);
            this.neuralNetUserControl1.Margin = new System.Windows.Forms.Padding(0);
            this.neuralNetUserControl1.MaximumSize = new System.Drawing.Size(1200, 650);
            this.neuralNetUserControl1.MinimumSize = new System.Drawing.Size(1200, 650);
            this.neuralNetUserControl1.Name = "neuralNetUserControl1";
            this.neuralNetUserControl1.Size = new System.Drawing.Size(1200, 650);
            this.neuralNetUserControl1.TabIndex = 0;
            // 
            // aboutUserControl1
            // 
            this.aboutUserControl1.Location = new System.Drawing.Point(0, 0);
            this.aboutUserControl1.Margin = new System.Windows.Forms.Padding(0);
            this.aboutUserControl1.MaximumSize = new System.Drawing.Size(1200, 650);
            this.aboutUserControl1.MinimumSize = new System.Drawing.Size(1200, 650);
            this.aboutUserControl1.Name = "aboutUserControl1";
            this.aboutUserControl1.Size = new System.Drawing.Size(1200, 650);
            this.aboutUserControl1.TabIndex = 0;
            // 
            // WeatherForecastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.tabControl);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "WeatherForecastForm";
            this.Text = "Weather Forecast";
            this.tabPage5.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl tabControl;
        private ForecastUserControl forecastUserControl;
        private HistoryUserControl historyUserControl1;
        private StatisticUserControl statisticUserControl1;
        private NeuralNetUserControl neuralNetUserControl1;
        private AboutUserControl aboutUserControl1;
    }
}

