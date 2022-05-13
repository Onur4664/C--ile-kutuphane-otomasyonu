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
    public partial class emanetögr : Form
    {
        int tutindex;
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = kutuphaneleryeni.mdb");
        DataSet ds = new DataSet();//oğrenci
        DataSet dd = new DataSet();//kitap
        DataSet aa = new DataSet();//emanet
        BindingSource bs = new BindingSource();//ogrenci
        BindingSource bd = new BindingSource();//kitap
        BindingSource ss = new BindingSource();//emanet
        void verileri_cek()
        {

            ds.Clear();
            string seckomutu = "select*from ogrenci";
            OleDbDataAdapter da = new OleDbDataAdapter(seckomutu, conn);
            da.Fill(ds, "ogrenci");
        }
        void verilerii_cek()
        {
            dd.Clear();
            string seckomutu = "select*from kitap";
            OleDbDataAdapter da = new OleDbDataAdapter(seckomutu, conn);
            da.Fill(dd, "kitap");



        }
        void verilerrii_cek()
        {
            aa.Clear();
            string seckomutu = "select*from emanet";
            OleDbDataAdapter da = new OleDbDataAdapter(seckomutu, conn);
            da.Fill(aa, "emanet");
        }
        public emanetögr()
        {
            InitializeComponent();
        }

        private void emanetögr_Load(object sender, EventArgs e)
        {
            if (conn.State == ConnectionState.Closed) conn.Open();
            verileri_cek(); verilerii_cek(); verilerrii_cek();
            bs.DataSource = ds.Tables["ogrenci"];
            bd.DataSource = dd.Tables["kitap"];
            ss.DataSource = aa.Tables["emanet"];
            dataGridView2.DataSource = bs;
            dataGridView3.DataSource = bd;
            dataGridView1.DataSource = ss;
            TC.DataBindings.Add("Text", bs, "ogrentc");
            seclnögr.DataBindings.Add("Text", bs, "ad");
            seckitap.DataBindings.Add("Text", bd, "kitapad");
            ////emanet
            seçilen_tc.DataBindings.Add("Text", ss, "tc");
            ögradsoy.DataBindings.Add("Text", ss, "ogrenci_adı");
            kitapsec.DataBindings.Add("Text", ss, "kitap_adı");
            cbalıstarih.DataBindings.Add("Text", ss, "iade_tarih");
            tbdurum.DataBindings.Add("Text", ss, "kitap_durum");
            id.DataBindings.Add("Text", ss, "emanet_id");
            cbveris_tarihi.DataBindings.Add("Text", ss, "emanet_tarih");
            label13.DataBindings.Add("Text", ss, "kitap_kullanım");
            label14.DataBindings.Add("Text", ss, "ceza");
        }

        private void TC_TextChanged(object sender, EventArgs e)
        {
            seçilen_tc.Text = TC.Text;
        }

        private void seclnögr_TextChanged(object sender, EventArgs e)
        {
            ögradsoy.Text = seclnögr.Text;
        }

        private void tc_ara_TextChanged(object sender, EventArgs e)
        {
            ds.Clear();
            string seckomutu = "select*from ogrenci where ogrentc like '%" + tc_ara.Text + "%'";
            OleDbDataAdapter da = new OleDbDataAdapter(seckomutu, conn);
            da.Fill(ds, "ogrenci");
        }

        private void tc_ara_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((int)e.KeyChar >= 48 && (int)e.KeyChar <= 57 || (int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;

            }
        }

        private void seckitap_TextChanged(object sender, EventArgs e)
        {
            kitapsec.Text = seckitap.Text;
        }

        private void kitap_ara_TextChanged(object sender, EventArgs e)
        {
            dd.Clear();
            string seckomutu = "select*from kitap where kitapad like '%" + kitap_ara.Text + "%'";
            OleDbDataAdapter da = new OleDbDataAdapter(seckomutu, conn);
            da.Fill(dd, "kitap");
        }

        private void btnkaydet_Click(object sender, EventArgs e)
        {
            if (seckitap.Text != "")
            {


                tbdurum.Text = "emanet verildi";
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into emanet (ogrenci_adı,kitap_adı,emanet_tarih,kitap_durum,tc,kitap_kullanım,ceza) Values(@ogrenci_adı,@kitap_adı,@emanet_tarih,@kitap_durum,@tc,@kitap_kullanım,@ceza)";
                cmd.Parameters.AddWithValue("@ogrenci_adı", ögradsoy.Text);
                cmd.Parameters.AddWithValue("@kitap_adı", kitapsec.Text);
                cmd.Parameters.AddWithValue("@emanet_tarih", cbveris_tarihi.Text);
                cmd.Parameters.AddWithValue("@kitap_durum", tbdurum.Text);
                cmd.Parameters.AddWithValue("@tc", seçilen_tc.Text);
                cmd.Parameters.AddWithValue("@kitap_kullanım", label13.Text);
                cmd.Parameters.AddWithValue("@ceza", label14.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("KAYDINIZ YAPILDI");
                verilerrii_cek();
                tutindex = aa.Tables["emanet"].Rows.Count;
                ss.Position = tutindex;
                btnkaydet.Visible = false;
            }
            else
            {
                MessageBox.Show("kitap seçin");
            }
        }

        private void gelen_kitap_Click(object sender, EventArgs e)
        {
            label14.Text = "0";
            label13.Text = "0";

            TimeSpan fark = cbalıstarih.Value - cbveris_tarihi.Value;
            if (fark.Days > 10)
            {
                int tut = 0; int a = 0;
                tut = fark.Days - 10;
                a = tut * 5;
                label14.Text = a.ToString() + " tl";

            }

            label13.Text = fark.Days.ToString() + " gün";
            tbdurum.Text = "iade alınıdı";
            tutindex = ss.Position;
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            cmd.CommandText = "UPDATE emanet SET iade_tarih=@iade_tarih,kitap_durum=@kitap_durum,kitap_kullanım=@kitap_kullanım,ceza=@ceza WHERE emanet_id=@emanet_id";
            cmd.Parameters.AddWithValue("@iade_tarih", cbalıstarih.Value.ToShortDateString());
            cmd.Parameters.AddWithValue("@kitap_durum", tbdurum.Text);
            cmd.Parameters.AddWithValue("@kitap_kullanım", label13.Text);
            cmd.Parameters.AddWithValue("@ceza", label14.Text);
            cmd.Parameters.AddWithValue("@emanet_id", id.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("güncelleme yapildi");
            verilerrii_cek();
            ss.Position = tutindex;
        }

        private void butonsil_Click(object sender, EventArgs e)
        {
            tutindex = ss.Position;
            DialogResult c = MessageBox.Show("SİLMEK İSTİYORMUSUNUZ", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (c == DialogResult.Yes)
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "delete from emanet where  emanet_id=@emanet_id";
                cmd.Parameters.AddWithValue("@emanet_id", id.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("KAYDINIZ SİLİNDİ");
                verilerrii_cek();
                ss.Position = tutindex;

            }
        }

        private void butonkayıt_yeni_Click(object sender, EventArgs e)
        {
            btnkaydet.Visible = true;
            cbveris_tarihi.Value = DateTime.Now;
            label14.Text = "0";
            label13.Text = "kullanılıyor";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            anamenü f1 = new anamenü();
            this.Hide();
            f1.Show();
        }

        private void geriii_Click(object sender, EventArgs e)
        {
            anamenü f1 = new anamenü();
            this.Hide();
            f1.Show();
        }

        private void raporla_Click(object sender, EventArgs e)
        {
            raporlama f1 = new raporlama();
            f1.Show();
        }
    }
}
