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
    public partial class Form1 : Form
    {
        OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = kutuphaneleryeni.mdb");
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        Boolean yenikayitmi;
        OleDbDataReader oku;
        public Form1()
        {
            InitializeComponent();
        }
        void verileri_cek()
        {
            ds.Clear();
            string seckomutu = "select * from kullanıcıgiris";
            OleDbDataAdapter da = new OleDbDataAdapter(seckomutu, conn);
            da.Fill(ds, "kullanıcıgiris");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (tbkullanıcı.Text == "" && tbsifre.Text == "")
            {
                MessageBox.Show("Lütfen Kullanıcı Adı Ve Şifre Girin");
            }
            else
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "select * from kullanıcıgiris where kullanıcıadı='" + tbkullanıcı.Text + "'and kullanıcışifre='" + tbsifre.Text + "' ";
                conn.Open();
                oku = cmd.ExecuteReader();
                if (oku.Read())
                {
                    anamenü f1 = new anamenü();
                    this.Hide();
                    f1.Show();
                }
                else
                {
                    MessageBox.Show("Yanlış Kullanıcı Adı Ve Şifre");
                }
                conn.Close();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

