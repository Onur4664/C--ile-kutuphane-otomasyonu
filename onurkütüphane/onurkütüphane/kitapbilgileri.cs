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
    public partial class kitapbilgileri : Form
    {
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = kutuphaneleryeni.mdb");
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        Boolean yenikayitmi;
        OleDbDataReader oku;
        int tutindex;
        public kitapbilgileri()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd2 = new OleDbCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select * from kitap where barkodno='" + tbno.Text + "'";
            if (conn.State == ConnectionState.Closed) conn.Open();
            oku = cmd2.ExecuteReader();
            if (oku.Read())
            {
                DialogResult c = MessageBox.Show("Lütfen Farklı Bilgiler Girin");
                if (c == DialogResult.OK)
                {
                    tbno.Clear();
                    tbad.Clear();
                    tbyazar.Clear();
                    tbsayfa.Clear();
                    tbyayın.Clear();
                }
            }
            else
            {
                {
                    {
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "insert into kitap(barkodno,kitapad,yazar,sayfasayısı,yayınevi) Values(@barkodno,@kitapad,@yazar,@sayfasayısı,@yayınevi)";
                        cmd.Parameters.AddWithValue("barkodno", tbno.Text);
                        cmd.Parameters.AddWithValue("kitapad", tbad.Text);
                        cmd.Parameters.AddWithValue("yazar", tbyazar.Text);
                        cmd.Parameters.AddWithValue("sayfasayısı", tbsayfa.Text);
                        cmd.Parameters.AddWithValue("yayınevi", tbyayın.Text);
                        cmd.ExecuteNonQuery();
                        OleDbCommand onur = new OleDbCommand();
                        //onur.Connection = conn;
                        //onur.CommandText = "insert into kitapadi(kitapismi) values(@kitapismi) ";
                        //onur.Parameters.AddWithValue("kitapismi", tbad.Text);
                        //onur.ExecuteNonQuery();
                        MessageBox.Show("Yeni Kitap Eklendi");
                        verileri_cek();
                    }
                }
            }
        }

        void verileri_cek()
        {
            ds.Clear();
            string seckomutu = "select * from kitap";
            OleDbDataAdapter da = new OleDbDataAdapter(seckomutu, conn);
            da.Fill(ds, "kitap");
        }
        void griddoldurtc()
        {
            conn.Close();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from kitap where barkodno like '%" + tbarama.Text + "%'", conn);
            ds = new DataSet();
            conn.Open();

            da.Fill(ds, "kitap");
            bs.DataSource = ds.Tables["kitap"];
            dataGridView1.DataSource = bs;

            conn.Close();
        }
        void griddoldurad()
        {
            conn.Close();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from kitap where kitapad like '%" + tbarama.Text + "%'", conn);
            ds = new DataSet();
            conn.Open();
            da.Fill(ds, "kitap");
            bs.DataSource = ds.Tables["kitap"];
            dataGridView1.DataSource = bs;

            conn.Close();
        }

        private void kitapbilgileri_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            verileri_cek();
            bs.DataSource = ds.Tables["kitap"];
            dataGridView1.DataSource = bs;
            tbıd.DataBindings.Add("Text", bs, "kitapıd");
            tbno.DataBindings.Add("Text", bs, "barkodno");
            tbad.DataBindings.Add("Text", bs, "kitapad");
            tbyazar.DataBindings.Add("Text", bs, "yazar");
            tbsayfa.DataBindings.Add("Text", bs, "sayfasayısı");
            tbyayın.DataBindings.Add("Text", bs, "yayınevi");
            yenikayitmi = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;

            OleDbCommand cmd2 = new OleDbCommand();
            cmd2.Connection = conn;
            cmd2.CommandText = "select kitapıd from kitap where barkodno='" + tbno.Text + "'";
            if (conn.State == ConnectionState.Closed) conn.Open();
            oku = cmd2.ExecuteReader();
            if (oku.Read())
            {
                string kıd = oku.GetValue(0).ToString();

                cmd.CommandText = "update kitap set barkodno=@barkodno,kitapad=@kitapad,yazar=@yazar,sayfasayısı=@sayfasayısı,yayınevi=@yayınevi where kitapıd=" + int.Parse(kıd) + "";
                cmd.Parameters.AddWithValue("@barkodno", tbno.Text);
                cmd.Parameters.AddWithValue("@kitapad", tbad.Text);
                cmd.Parameters.AddWithValue("@yazar", tbyazar.Text);
                cmd.Parameters.AddWithValue("@sayfasayısı", tbsayfa.Text);
                cmd.Parameters.AddWithValue("@yayınevi", tbyayın.Text);
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
            tutindex = bs.Position;
            DialogResult c = MessageBox.Show("silmek istediğinize emin misiniz?", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (c == DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from kitap where kitapıd=@kitapıd";
                cmd.Parameters.AddWithValue("@kitapıd", int.Parse(tbıd.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("kaydınız silindi!!!!");
                verileri_cek();
                bs.Position = tutindex;
                conn.Close();

            }
        }

        private void tbıd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbno_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbad_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void tbyazar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
        }

        private void tbsayfa_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void tbyayın_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar);
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

        private void button1_Click(object sender, EventArgs e)
        {

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
    }
}
