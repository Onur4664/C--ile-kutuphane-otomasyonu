using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace onurkütüphane
{
    public partial class personel : Form
    {

        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = kutuphaneleryeni.mdb");
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        Boolean yenikayitmi = true;
        OleDbDataReader oku;
        int tutindex;
        void verileri_cek()
        {
            ds.Clear();
            string seckomutu = "select * from personel";
            OleDbDataAdapter da = new OleDbDataAdapter(seckomutu, conn);
            da.Fill(ds, "personel");
        }
        public personel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;

            OleDbCommand cmd2 = new OleDbCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select personeltc from personel where personeltc='" + ptc.Text + "'";
            if (conn.State == ConnectionState.Closed) conn.Open();
            oku = cmd2.ExecuteReader();
            if (oku.Read())
            {

                cmd.CommandText = "update personel set personelad=@personelad,personelsoy=@personelsoy,personeltc=@personeltc,personeltel=@personeltel,personelgörev=@personelgörev where personelıd=@personelıd";
                cmd.Parameters.AddWithValue("@personelad", pad.Text);
                cmd.Parameters.AddWithValue("@personelsoy", psoy.Text);
                cmd.Parameters.AddWithValue("@personeltc", ptc.Text);
                cmd.Parameters.AddWithValue("@personeltel", ptel.Text);
                cmd.Parameters.AddWithValue("@personelgörev", pgörev.Text);
                cmd.Parameters.AddWithValue("@personelıd", perıd.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("İşleminiz Tamalanmıştır");
                verileri_cek();
                conn.Close();
                bs.Position = tutindex;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = true;
            if (conn.State == ConnectionState.Closed) conn.Open();
            DialogResult c = MessageBox.Show("silmek istediğinize emin misiniz?", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (c == DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from personel where personeltc=@personeltc";
                cmd.Parameters.AddWithValue("@personeltc", ptc.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("kaydınız silindi!!!!");
                verileri_cek();
                conn.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            anamenü f1 = new anamenü();
            this.Hide();
            f1.Show();
        }

        private void personel_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            verileri_cek();
            bs.DataSource = ds.Tables["personel"];
            dataGridView1.DataSource = bs;
            perıd.DataBindings.Add("Text", bs, "personelıd");
            pad.DataBindings.Add("Text", bs, "personelad");
            psoy.DataBindings.Add("Text", bs, "personelsoy");
            ptc.DataBindings.Add("Text", bs, "personeltc");
            ptel.DataBindings.Add("Text", bs, "personeltel");
            pgörev.DataBindings.Add("Text", bs, "personelgörev");
            yenikayitmi = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {

            OleDbCommand cmd2 = new OleDbCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select * from personel where personeltc='" + ptc.Text + "'";
            if (conn.State == ConnectionState.Closed) conn.Open();
            oku = cmd2.ExecuteReader();
            if (oku.Read())
            {
                DialogResult c = MessageBox.Show("Lütfen Farklı Bilgiler Girin");
                if (c == DialogResult.OK)
                {
                    pad.Clear();
                    psoy.Clear();
                    ptc.Clear();
                    ptel.Clear();
                    pgörev.Clear();
                }
            }
            else
            {

                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into personel (personelad,personelsoy,personeltc,personeltel,personelgörev) Values(@personelad,@personelsoy,@personeltc,@personeltel,@personelgörev)";
                    cmd.Parameters.AddWithValue("@personelad", pad.Text);
                    cmd.Parameters.AddWithValue("@personelsoy", psoy.Text);
                    cmd.Parameters.AddWithValue("@personeltc", ptc.Text);
                    cmd.Parameters.AddWithValue("@personeltel", ptel.Text);
                    cmd.Parameters.AddWithValue("@personelgörev", pgörev.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yeni Personel Eklendi");
                    verileri_cek();
                }
            }
        }

        private void pad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void psoy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void ptc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ptel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void pgörev_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
        }
    }
}