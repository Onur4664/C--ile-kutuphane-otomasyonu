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

    public partial class ögrencibilgileri : Form
    {
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = kutuphaneleryeni.mdb");
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        Boolean yenikayitmi = true;
        OleDbDataReader oku;
        int tutindex;
        public ögrencibilgileri()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

            OleDbCommand cmd2 = new OleDbCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select * from ogrenci where ogrentc=" + tbtc.Text + "";
            if (conn.State == ConnectionState.Closed) conn.Open();
            oku = cmd2.ExecuteReader();
            if (oku.Read())
            {
                DialogResult c = MessageBox.Show("Lütfen Farklı Bilgiler Girin");
                if (c == DialogResult.OK)
                {
                    tbıd.DataBindings.Clear();
                    tbıd.Clear();
                    tbtc.DataBindings.Clear();
                    tbtc.Clear();
                    tbad.DataBindings.Clear();
                    tbad.Clear();
                    tbsoy.DataBindings.Clear();
                    tbsoy.Clear();
                    tbtelefon.DataBindings.Clear();
                    tbtelefon.Clear();
                    tbemail.DataBindings.Clear();
                    tbemail.Clear();
                    tbfakülte.DataBindings.Clear();
                    tbfakülte.Clear();
                    tbsınıf.DataBindings.Clear();
                    tbsınıf.Clear();
                    richTextBox1.DataBindings.Clear();
                    richTextBox1.Clear();
                    dataGridView1.ClearSelection();



                }
            }
            else
            {
                if (tbtc.Text == "" & tbad.Text == "" & tbsoy.Text == "" & tbtelefon.Text == "" & tbemail.Text == "" & tbfakülte.Text == "" & tbsınıf.Text == "" & richTextBox1.Text == "")
                {
                    MessageBox.Show("lütfen tüm verileri giriniz");
                }
                else
                {
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "insert into ogrenci(ogrentc,ad,soyad,telefon,email,fakulte,sinif,adres) Values(@ogrentctc,@ad,@soyad,@telefon,@email,@fakulte,@sinif,@adres)";
                    cmd.Parameters.AddWithValue("@ogrentc", tbtc.Text);
                    cmd.Parameters.AddWithValue("@ad", tbad.Text);
                    cmd.Parameters.AddWithValue("@soyad", tbsoy.Text);
                    cmd.Parameters.AddWithValue("@telefon", tbtelefon.Text);
                    cmd.Parameters.AddWithValue("@email", tbemail.Text);
                    cmd.Parameters.AddWithValue("@fakulte", tbfakülte.Text);
                    cmd.Parameters.AddWithValue("@sinif", tbsınıf.Text);
                    cmd.Parameters.AddWithValue("@adres", richTextBox1.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yeni Ögrenci Eklendi");
                    verileri_cek();
                }
            }
        }
        void verileri_cek()
        {
            ds.Clear();
            string seckomutu = "select * from ogrenci";
            OleDbDataAdapter da = new OleDbDataAdapter(seckomutu, conn);
            da.Fill(ds, "ogrenci");

        }
        void griddoldurtc()
        {
            conn.Close();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from ogrenci where ogrentc like '%" + tbarama.Text + "%'", conn);
            ds = new DataSet();
            conn.Open();

            da.Fill(ds, "ogrenci");
            bs.DataSource = ds.Tables["ogrenci"];
            dataGridView1.DataSource = bs;

            conn.Close();
        }
        void griddoldurad()
        {
            conn.Close();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from ogrenci where ad like '%" + tbarama.Text + "%'", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "ogrenci");
            bs.DataSource = ds.Tables["ogrenci"];
            dataGridView1.DataSource = bs;

            conn.Close();
        }

        private void ögrencibilgileri_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            verileri_cek();
            bs.DataSource = ds.Tables["ogrenci"];
            dataGridView1.DataSource = bs;
            tbıd.DataBindings.Add("Text", bs, "Kimlik");
            tbtc.DataBindings.Add("Text", bs, "ogrentc");
            tbad.DataBindings.Add("Text", bs, "ad");
            tbsoy.DataBindings.Add("Text", bs, "soyad");
            tbtelefon.DataBindings.Add("Text", bs, "telefon");
            tbemail.DataBindings.Add("Text", bs, "email");
            tbfakülte.DataBindings.Add("Text", bs, "fakulte");
            tbsınıf.DataBindings.Add("Text", bs, "sinif");
            richTextBox1.DataBindings.Add("Text", bs, "adres");
            yenikayitmi = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;

            OleDbCommand cmd2 = new OleDbCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select ogrentc from ogrenci where ogrentc=" + tbtc.Text + "";
            if (conn.State == ConnectionState.Closed) conn.Open();
            oku = cmd2.ExecuteReader();
            if (oku.Read())
            {

                cmd.CommandText = "update ogrenci set ogrentc=@ogrentc,ad=@ad,soyad=@soyad,telefon=@telefon,email=@email,fakulte=@fakulte,sinif=@sinif,adres=@adres where Kimlik=@Kimlik";
                cmd.Parameters.AddWithValue("@ogrentc", tbtc.Text);
                cmd.Parameters.AddWithValue("@ad", tbad.Text);
                cmd.Parameters.AddWithValue("@soyad", tbsoy.Text);
                cmd.Parameters.AddWithValue("@telefon", tbtelefon.Text);
                cmd.Parameters.AddWithValue("@email", tbemail.Text);
                cmd.Parameters.AddWithValue("@fakulte", tbfakülte.Text);
                cmd.Parameters.AddWithValue("@sinif", tbsınıf.Text);
                cmd.Parameters.AddWithValue("@adres", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@Kimlik", tbıd.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("İşleminiz Tamalanmıştır");
                verileri_cek();
                conn.Close();
                bs.Position = tutindex;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            anamenü f1 = new anamenü();
            this.Hide();
            f1.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            DialogResult c = MessageBox.Show("silmek istediğinize emin misiniz?", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (c == DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from ogrenci where ogrentc=@ogrentc";
                cmd.Parameters.AddWithValue("@tcno", tbtc.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("kaydınız silindi!!!!");
                verileri_cek();
                conn.Close();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (tbıd.DataBindings.Count == 0)
            {
                tbıd.DataBindings.Add("Text", bs, "Kimlik");
                tbtc.DataBindings.Add("Text", bs, "ogrentc");
                tbad.DataBindings.Add("Text", bs, "ad");
                tbsoy.DataBindings.Add("Text", bs, "soyad");
                tbtelefon.DataBindings.Add("Text", bs, "telefon");
                tbemail.DataBindings.Add("Text", bs, "email");
                tbfakülte.DataBindings.Add("Text", bs, "fakulte");
                tbsınıf.DataBindings.Add("Text", bs, "sinif");
                richTextBox1.DataBindings.Add("Text", bs, "adres");
            }
        }

        private void tbtc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void tbsoy_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void tbtelefon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbfakülte_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar);
        }

        private void tbsınıf_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tbarama.Text = "";
            tbarama.Focus();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tbarama.Text = "";
            tbarama.Focus();
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (radioButton2.Checked)
            {
                e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);

            }

            else if (radioButton1.Checked)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void tbarama_TextChanged(object sender, EventArgs e)
        {
            if (tbarama.Text == "")
            {
                verileri_cek();
            }
            else
            {
                if (radioButton1.Checked == false & radioButton2.Checked == false)
                {
                    MessageBox.Show("Arama Yöntemi Secmediniz!!!", "uyari", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    tbarama.Text = "";
                }
                else if (radioButton1.Checked)
                {
                    griddoldurtc();
                }
                else if (radioButton2.Checked)
                {
                    griddoldurad();
                }
            }
        }
    }
}
