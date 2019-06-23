namespace WeatherForecast.UserControls
{
    partial class HistoryUserControl
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ColumnID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnHour = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnTemperature = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnHumidity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWindSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnWindDirection = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnCloudy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColumnVisibility = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColumnID,
            this.ColumnCity,
            this.ColumnDate,
            this.ColumnHour,
            this.ColumnTemperature,
            this.ColumnHumidity,
            this.ColumnWindSpeed,
            this.ColumnWindDirection,
            this.ColumnCloudy,
            this.ColumnVisibility});
            this.dataGridView1.Location = new System.Drawing.Point(3, 54);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1144, 587);
            this.dataGridView1.TabIndex = 3;
            // 
            // ColumnID
            // 
            this.ColumnID.HeaderText = "ID";
            this.ColumnID.Name = "ColumnID";
            this.ColumnID.Width = 50;
            // 
            // ColumnCity
            // 
            this.ColumnCity.HeaderText = "Miasto";
            this.ColumnCity.Name = "ColumnCity";
            // 
            // ColumnDate
            // 
            this.ColumnDate.HeaderText = "Data";
            this.ColumnDate.Name = "ColumnDate";
            // 
            // ColumnHour
            // 
            this.ColumnHour.HeaderText = "Godzina";
            this.ColumnHour.Name = "ColumnHour";
            // 
            // ColumnTemperature
            // 
            this.ColumnTemperature.HeaderText = "Temperatura";
            this.ColumnTemperature.Name = "ColumnTemperature";
            // 
            // ColumnHumidity
            // 
            this.ColumnHumidity.HeaderText = "Wilgotność";
            this.ColumnHumidity.Name = "ColumnHumidity";
            // 
            // ColumnWindSpeed
            // 
            this.ColumnWindSpeed.HeaderText = "Kierunek wiatru";
            this.ColumnWindSpeed.Name = "ColumnWindSpeed";
            this.ColumnWindSpeed.Width = 140;
            // 
            // ColumnWindDirection
            // 
            this.ColumnWindDirection.HeaderText = "Prędkość wiatru";
            this.ColumnWindDirection.Name = "ColumnWindDirection";
            this.ColumnWindDirection.Width = 140;
            // 
            // ColumnCloudy
            // 
            this.ColumnCloudy.HeaderText = "Zachmurzenie";
            this.ColumnCloudy.Name = "ColumnCloudy";
            this.ColumnCloudy.Width = 120;
            // 
            // ColumnVisibility
            // 
            this.ColumnVisibility.HeaderText = "Widzialność";
            this.ColumnVisibility.Name = "ColumnVisibility";
            this.ColumnVisibility.Width = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(506, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 42);
            this.label1.TabIndex = 2;
            this.label1.Text = "Historia";
            // 
            // HistoryUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(1150, 620);
            this.MinimumSize = new System.Drawing.Size(1150, 620);
            this.Name = "HistoryUserControl";
            this.Size = new System.Drawing.Size(1150, 620);
            this.Load += new System.EventHandler(this.HistoryUserControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnHour;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnTemperature;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnHumidity;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWindSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnWindDirection;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnCloudy;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColumnVisibility;
    }
}
