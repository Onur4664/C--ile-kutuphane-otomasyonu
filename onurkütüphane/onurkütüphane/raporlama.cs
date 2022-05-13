using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace onurkütüphane
{
    public partial class raporlama : Form
    {
        public raporlama()
        {
            InitializeComponent();
        }

        private void raporlama_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'kutuphaneleryeniDataSet1.emanet' table. You can move, or remove it, as needed.
            this.emanetTableAdapter.Fill(this.kutuphaneleryeniDataSet1.emanet);

            this.reportViewer1.RefreshReport();
        }
    }
}
