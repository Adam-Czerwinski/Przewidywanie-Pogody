namespace WeatherForecast.UserControls
{
    partial class StatisticUserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series10 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series11 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series12 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.titleLabel = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTypeLabel = new System.Windows.Forms.Label();
            this.chartTypeComboBox = new System.Windows.Forms.ComboBox();
            this.statisticTypeLabel = new System.Windows.Forms.Label();
            this.statisticTypeComboBox = new System.Windows.Forms.ComboBox();
            this.yearLabel = new System.Windows.Forms.Label();
            this.monthLabel = new System.Windows.Forms.Label();
            this.yearUpDown = new System.Windows.Forms.NumericUpDown();
            this.monthUpDown = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.yearUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(499, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(178, 42);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "Statystyki";
            // 
            // chart1
            // 
            chartArea4.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea4);
            legend4.Name = "Temperatura";
            legend4.Title = "Temperatura";
            this.chart1.Legends.Add(legend4);
            this.chart1.Location = new System.Drawing.Point(3, 187);
            this.chart1.Name = "chart1";
            series10.ChartArea = "ChartArea1";
            series10.Legend = "Temperatura";
            series10.Name = "Najmniejsza tmperatura";
            series10.YValuesPerPoint = 6;
            series11.ChartArea = "ChartArea1";
            series11.Legend = "Temperatura";
            series11.Name = "Średnia temperatura";
            series12.ChartArea = "ChartArea1";
            series12.Legend = "Temperatura";
            series12.Name = "Największa temperatura";
            this.chart1.Series.Add(series10);
            this.chart1.Series.Add(series11);
            this.chart1.Series.Add(series12);
            this.chart1.Size = new System.Drawing.Size(1142, 436);
            this.chart1.TabIndex = 4;
            this.chart1.Text = "Temperatura";
            title4.Name = "Title1";
            title4.Text = "Temperatura";
            this.chart1.Titles.Add(title4);
            // 
            // chartTypeLabel
            // 
            this.chartTypeLabel.AutoSize = true;
            this.chartTypeLabel.Location = new System.Drawing.Point(29, 137);
            this.chartTypeLabel.Name = "chartTypeLabel";
            this.chartTypeLabel.Size = new System.Drawing.Size(67, 13);
            this.chartTypeLabel.TabIndex = 5;
            this.chartTypeLabel.Text = "Typ wykresu";
            // 
            // chartTypeComboBox
            // 
            this.chartTypeComboBox.FormattingEnabled = true;
            this.chartTypeComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chartTypeComboBox.Location = new System.Drawing.Point(138, 129);
            this.chartTypeComboBox.Name = "chartTypeComboBox";
            this.chartTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.chartTypeComboBox.TabIndex = 6;
            this.chartTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.chartTypeComboBox_SelectedIndexChanged);
            // 
            // statisticTypeLabel
            // 
            this.statisticTypeLabel.AutoSize = true;
            this.statisticTypeLabel.Location = new System.Drawing.Point(300, 132);
            this.statisticTypeLabel.Name = "statisticTypeLabel";
            this.statisticTypeLabel.Size = new System.Drawing.Size(86, 13);
            this.statisticTypeLabel.TabIndex = 7;
            this.statisticTypeLabel.Text = "Rodzaj statystyki";
            // 
            // statisticTypeComboBox
            // 
            this.statisticTypeComboBox.FormattingEnabled = true;
            this.statisticTypeComboBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.statisticTypeComboBox.Location = new System.Drawing.Point(442, 129);
            this.statisticTypeComboBox.Name = "statisticTypeComboBox";
            this.statisticTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.statisticTypeComboBox.TabIndex = 8;
            this.statisticTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.statisticTypeComboBox_SelectedIndexChanged);
            // 
            // yearLabel
            // 
            this.yearLabel.AutoSize = true;
            this.yearLabel.Location = new System.Drawing.Point(622, 132);
            this.yearLabel.Name = "yearLabel";
            this.yearLabel.Size = new System.Drawing.Size(27, 13);
            this.yearLabel.TabIndex = 9;
            this.yearLabel.Text = "Rok";
            // 
            // monthLabel
            // 
            this.monthLabel.AutoSize = true;
            this.monthLabel.Location = new System.Drawing.Point(898, 132);
            this.monthLabel.Name = "monthLabel";
            this.monthLabel.Size = new System.Drawing.Size(43, 13);
            this.monthLabel.TabIndex = 11;
            this.monthLabel.Text = "Miesiąc";
            // 
            // yearUpDown
            // 
            this.yearUpDown.Location = new System.Drawing.Point(717, 130);
            this.yearUpDown.Maximum = new decimal(new int[] {
            2019,
            0,
            0,
            0});
            this.yearUpDown.Minimum = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.yearUpDown.Name = "yearUpDown";
            this.yearUpDown.Size = new System.Drawing.Size(120, 20);
            this.yearUpDown.TabIndex = 12;
            this.yearUpDown.Value = new decimal(new int[] {
            2015,
            0,
            0,
            0});
            this.yearUpDown.ValueChanged += new System.EventHandler(this.yearUpDown_ValueChanged);
            // 
            // monthUpDown
            // 
            this.monthUpDown.Location = new System.Drawing.Point(991, 130);
            this.monthUpDown.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.monthUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.monthUpDown.Name = "monthUpDown";
            this.monthUpDown.Size = new System.Drawing.Size(120, 20);
            this.monthUpDown.TabIndex = 13;
            this.monthUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.monthUpDown.ValueChanged += new System.EventHandler(this.monthUpDown_ValueChanged);
            // 
            // StatisticUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.monthUpDown);
            this.Controls.Add(this.yearUpDown);
            this.Controls.Add(this.monthLabel);
            this.Controls.Add(this.yearLabel);
            this.Controls.Add(this.statisticTypeComboBox);
            this.Controls.Add(this.statisticTypeLabel);
            this.Controls.Add(this.chartTypeComboBox);
            this.Controls.Add(this.chartTypeLabel);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.titleLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(1200, 650);
            this.MinimumSize = new System.Drawing.Size(1200, 650);
            this.Name = "StatisticUserControl";
            this.Size = new System.Drawing.Size(1200, 650);
            this.Load += new System.EventHandler(this.StatisticUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.yearUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.monthUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label chartTypeLabel;
        private System.Windows.Forms.ComboBox chartTypeComboBox;
        private System.Windows.Forms.Label statisticTypeLabel;
        private System.Windows.Forms.ComboBox statisticTypeComboBox;
        private System.Windows.Forms.Label yearLabel;
        private System.Windows.Forms.Label monthLabel;
        private System.Windows.Forms.NumericUpDown yearUpDown;
        private System.Windows.Forms.NumericUpDown monthUpDown;
    }
}
