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
            this.menuPanel = new System.Windows.Forms.Panel();
            this.mimimizeButton = new System.Windows.Forms.Button();
            this.exitButton = new System.Windows.Forms.Button();
            this.aboutButton = new System.Windows.Forms.Button();
            this.neuralNetButton = new System.Windows.Forms.Button();
            this.statisticButton = new System.Windows.Forms.Button();
            this.historyButton = new System.Windows.Forms.Button();
            this.forecastButton = new System.Windows.Forms.Button();
            this.aboutUserControl = new WeatherForecast.UserControls.AboutUserControl();
            this.neuralNetUserControl = new WeatherForecast.UserControls.NeuralNetUserControl();
            this.statisticUserControl = new WeatherForecast.UserControls.StatisticUserControl();
            this.historyUserControl = new WeatherForecast.UserControls.HistoryUserControl();
            this.forecastUserControl = new WeatherForecast.UserControls.ForecastUserControl();
            this.menuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.Controls.Add(this.mimimizeButton);
            this.menuPanel.Controls.Add(this.exitButton);
            this.menuPanel.Controls.Add(this.aboutButton);
            this.menuPanel.Controls.Add(this.neuralNetButton);
            this.menuPanel.Controls.Add(this.statisticButton);
            this.menuPanel.Controls.Add(this.historyButton);
            this.menuPanel.Controls.Add(this.forecastButton);
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Margin = new System.Windows.Forms.Padding(0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Size = new System.Drawing.Size(1200, 50);
            this.menuPanel.TabIndex = 0;
            // 
            // mimimizeButton
            // 
            this.mimimizeButton.Location = new System.Drawing.Point(1100, 5);
            this.mimimizeButton.Margin = new System.Windows.Forms.Padding(0);
            this.mimimizeButton.Name = "mimimizeButton";
            this.mimimizeButton.Size = new System.Drawing.Size(40, 40);
            this.mimimizeButton.TabIndex = 6;
            this.mimimizeButton.Text = "_";
            this.mimimizeButton.UseVisualStyleBackColor = true;
            this.mimimizeButton.Click += new System.EventHandler(this.mimimizeButton_Click);
            // 
            // exitButton
            // 
            this.exitButton.BackColor = System.Drawing.Color.Firebrick;
            this.exitButton.Location = new System.Drawing.Point(1150, 5);
            this.exitButton.Margin = new System.Windows.Forms.Padding(0);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(40, 40);
            this.exitButton.TabIndex = 5;
            this.exitButton.Text = "X";
            this.exitButton.UseVisualStyleBackColor = false;
            this.exitButton.Click += new System.EventHandler(this.exitButton_Click);
            // 
            // aboutButton
            // 
            this.aboutButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.aboutButton.Location = new System.Drawing.Point(850, 5);
            this.aboutButton.Margin = new System.Windows.Forms.Padding(0);
            this.aboutButton.Name = "aboutButton";
            this.aboutButton.Size = new System.Drawing.Size(200, 40);
            this.aboutButton.TabIndex = 4;
            this.aboutButton.Text = "O aplikacji";
            this.aboutButton.UseVisualStyleBackColor = true;
            // 
            // neuralNetButton
            // 
            this.neuralNetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.neuralNetButton.Location = new System.Drawing.Point(640, 5);
            this.neuralNetButton.Margin = new System.Windows.Forms.Padding(0);
            this.neuralNetButton.Name = "neuralNetButton";
            this.neuralNetButton.Size = new System.Drawing.Size(200, 40);
            this.neuralNetButton.TabIndex = 3;
            this.neuralNetButton.Text = "Sieć neuronowa";
            this.neuralNetButton.UseVisualStyleBackColor = true;
            // 
            // statisticButton
            // 
            this.statisticButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.statisticButton.Location = new System.Drawing.Point(430, 5);
            this.statisticButton.Margin = new System.Windows.Forms.Padding(0);
            this.statisticButton.Name = "statisticButton";
            this.statisticButton.Size = new System.Drawing.Size(200, 40);
            this.statisticButton.TabIndex = 2;
            this.statisticButton.Text = "Statystyki ";
            this.statisticButton.UseVisualStyleBackColor = true;
            // 
            // historyButton
            // 
            this.historyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.historyButton.Location = new System.Drawing.Point(220, 5);
            this.historyButton.Margin = new System.Windows.Forms.Padding(0);
            this.historyButton.Name = "historyButton";
            this.historyButton.Size = new System.Drawing.Size(200, 40);
            this.historyButton.TabIndex = 1;
            this.historyButton.Text = "Historia ";
            this.historyButton.UseVisualStyleBackColor = true;
            // 
            // forecastButton
            // 
            this.forecastButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.forecastButton.Location = new System.Drawing.Point(10, 5);
            this.forecastButton.Margin = new System.Windows.Forms.Padding(0);
            this.forecastButton.Name = "forecastButton";
            this.forecastButton.Size = new System.Drawing.Size(200, 40);
            this.forecastButton.TabIndex = 0;
            this.forecastButton.Text = "Prognoza";
            this.forecastButton.UseVisualStyleBackColor = true;
            this.forecastButton.Click += new System.EventHandler(this.forecastButton_Click);
            // 
            // aboutUserControl
            // 
            this.aboutUserControl.Location = new System.Drawing.Point(0, 50);
            this.aboutUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.aboutUserControl.MaximumSize = new System.Drawing.Size(1200, 650);
            this.aboutUserControl.MinimumSize = new System.Drawing.Size(1200, 650);
            this.aboutUserControl.Name = "aboutUserControl";
            this.aboutUserControl.Size = new System.Drawing.Size(1200, 650);
            this.aboutUserControl.TabIndex = 1;
            // 
            // neuralNetUserControl
            // 
            this.neuralNetUserControl.Location = new System.Drawing.Point(0, 50);
            this.neuralNetUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.neuralNetUserControl.MaximumSize = new System.Drawing.Size(1200, 650);
            this.neuralNetUserControl.MinimumSize = new System.Drawing.Size(1200, 650);
            this.neuralNetUserControl.Name = "neuralNetUserControl";
            this.neuralNetUserControl.Size = new System.Drawing.Size(1200, 650);
            this.neuralNetUserControl.TabIndex = 2;
            // 
            // statisticUserControl
            // 
            this.statisticUserControl.Location = new System.Drawing.Point(0, 50);
            this.statisticUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.statisticUserControl.MaximumSize = new System.Drawing.Size(1200, 650);
            this.statisticUserControl.MinimumSize = new System.Drawing.Size(1200, 650);
            this.statisticUserControl.Name = "statisticUserControl";
            this.statisticUserControl.Size = new System.Drawing.Size(1200, 650);
            this.statisticUserControl.TabIndex = 3;
            // 
            // historyUserControl
            // 
            this.historyUserControl.Location = new System.Drawing.Point(0, 50);
            this.historyUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.historyUserControl.MaximumSize = new System.Drawing.Size(1200, 650);
            this.historyUserControl.MinimumSize = new System.Drawing.Size(1200, 650);
            this.historyUserControl.Name = "historyUserControl";
            this.historyUserControl.Size = new System.Drawing.Size(1200, 650);
            this.historyUserControl.TabIndex = 4;
            // 
            // forecastUserControl
            // 
            this.forecastUserControl.City = null;
            this.forecastUserControl.Cloudy = 0;
            this.forecastUserControl.ForecastData = null;
            this.forecastUserControl.Humidity = 0;
            this.forecastUserControl.Location = new System.Drawing.Point(0, 50);
            this.forecastUserControl.Margin = new System.Windows.Forms.Padding(0);
            this.forecastUserControl.MaximumSize = new System.Drawing.Size(1200, 650);
            this.forecastUserControl.MinimumSize = new System.Drawing.Size(1200, 650);
            this.forecastUserControl.Name = "forecastUserControl";
            this.forecastUserControl.RegionPL = null;
            this.forecastUserControl.Size = new System.Drawing.Size(1200, 650);
            this.forecastUserControl.TabIndex = 5;
            this.forecastUserControl.Temperature = 0D;
            this.forecastUserControl.Visibility = 0;
            this.forecastUserControl.WindDirection = null;
            this.forecastUserControl.WindSpeed = 0;
            // 
            // WeatherForecastForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.forecastUserControl);
            this.Controls.Add(this.historyUserControl);
            this.Controls.Add(this.statisticUserControl);
            this.Controls.Add(this.neuralNetUserControl);
            this.Controls.Add(this.aboutUserControl);
            this.Controls.Add(this.menuPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(1200, 700);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "WeatherForecastForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Weather Forecast";
            this.TopMost = true;
            this.menuPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel menuPanel;
        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.Button aboutButton;
        private System.Windows.Forms.Button neuralNetButton;
        private System.Windows.Forms.Button statisticButton;
        private System.Windows.Forms.Button historyButton;
        private System.Windows.Forms.Button forecastButton;
        private System.Windows.Forms.Button mimimizeButton;
        private AboutUserControl aboutUserControl;
        private NeuralNetUserControl neuralNetUserControl;
        private StatisticUserControl statisticUserControl;
        private HistoryUserControl historyUserControl;
        private ForecastUserControl forecastUserControl;
    }
}

