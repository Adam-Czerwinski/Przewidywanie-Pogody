namespace WeatherForecast.UserControls
{
    partial class NeuralNetUserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NeuralNetUserControl));
            this.NNTitleLabel = new System.Windows.Forms.Label();
            this.axAcroPDF1 = new AxAcroPDFLib.AxAcroPDF();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).BeginInit();
            this.SuspendLayout();
            // 
            // NNTitleLabel
            // 
            this.NNTitleLabel.AutoSize = true;
            this.NNTitleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F);
            this.NNTitleLabel.Location = new System.Drawing.Point(363, 18);
            this.NNTitleLabel.Name = "NNTitleLabel";
            this.NNTitleLabel.Size = new System.Drawing.Size(384, 42);
            this.NNTitleLabel.TabIndex = 3;
            this.NNTitleLabel.Text = "Opis sieci neuronowej";
            // 
            // axAcroPDF1
            // 
            this.axAcroPDF1.Enabled = true;
            this.axAcroPDF1.Location = new System.Drawing.Point(15, 77);
            this.axAcroPDF1.Name = "axAcroPDF1";
            this.axAcroPDF1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF1.OcxState")));
            this.axAcroPDF1.Size = new System.Drawing.Size(1121, 535);
            this.axAcroPDF1.TabIndex = 4;
            // 
            // NeuralNetUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.axAcroPDF1);
            this.Controls.Add(this.NNTitleLabel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(1200, 650);
            this.MinimumSize = new System.Drawing.Size(1200, 650);
            this.Name = "NeuralNetUserControl";
            this.Size = new System.Drawing.Size(1200, 650);
            this.Load += new System.EventHandler(this.NeuralNetUserControl_Load);
            //((System.ComponentModel.ISupportInitialize)(this.axAcroPDF1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label NNTitleLabel;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF1;
    }
}
