namespace onurkütüphane
{
    partial class raporlama
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.kutuphaneleryeniDataSet1 = new onurkütüphane.kutuphaneleryeniDataSet1();
            this.emanetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.emanetTableAdapter = new onurkütüphane.kutuphaneleryeniDataSet1TableAdapters.emanetTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.kutuphaneleryeniDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emanetBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.emanetBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "onurkütüphane.Report1.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(699, 448);
            this.reportViewer1.TabIndex = 0;
            // 
            // kutuphaneleryeniDataSet1
            // 
            this.kutuphaneleryeniDataSet1.DataSetName = "kutuphaneleryeniDataSet1";
            this.kutuphaneleryeniDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // emanetBindingSource
            // 
            this.emanetBindingSource.DataMember = "emanet";
            this.emanetBindingSource.DataSource = this.kutuphaneleryeniDataSet1;
            // 
            // emanetTableAdapter
            // 
            this.emanetTableAdapter.ClearBeforeFill = true;
            // 
            // raporlama
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(699, 448);
            this.Controls.Add(this.reportViewer1);
            this.Name = "raporlama";
            this.Text = "raporlama";
            this.Load += new System.EventHandler(this.raporlama_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kutuphaneleryeniDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emanetBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource emanetBindingSource;
        private kutuphaneleryeniDataSet1 kutuphaneleryeniDataSet1;
        private kutuphaneleryeniDataSet1TableAdapters.emanetTableAdapter emanetTableAdapter;
    }
}