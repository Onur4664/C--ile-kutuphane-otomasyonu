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
    public partial class anamenü : Form
    {
        public anamenü()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            kitapbilgileri f1 = new kitapbilgileri();
            this.Hide();
            f1.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ögrencibilgileri f1 = new ögrencibilgileri();
            this.Hide();
            f1.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            personel f1 = new personel();
            this.Hide();
            f1.Show();

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            emanetögr f1 = new emanetögr();
            this.Hide();
            f1.Show();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
