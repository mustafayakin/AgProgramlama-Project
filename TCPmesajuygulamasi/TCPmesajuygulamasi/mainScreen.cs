using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPmesajuygulamasi
{
    public partial class mainScreen : Form
    {
        string kullanici_id;
        SqlConnection connection = new SqlConnection("server=localhost;database=agprogramlama;integrated security=True");
        public mainScreen(string k_id)
        {
            kullanici_id = k_id;
            InitializeComponent();
        }
        private void mdigetir(Form frm)//yeni mdicgiparent açmak için fonksiyon
        {
            panel1.Controls.Clear();
            frm.MdiParent = this;
            frm.FormBorderStyle = FormBorderStyle.None;
            panel1.Controls.Add(frm);
            frm.Show();
        }
        private void databaseÜzerindenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void tCPİleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            withTCP tcp = new withTCP(kullanici_id);
            mdigetir(tcp);
        }

        private void mainScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            string sorgu = "UPDATE users set isOnline=@isOnline WHERE id ="+kullanici_id;
            SqlCommand cmd = new SqlCommand(sorgu, connection);
            cmd.Parameters.AddWithValue("@isOnline", "0");
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            Application.Exit();
        }

        private void portGüncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            updatePort pr = new updatePort(kullanici_id);
            mdigetir(pr);
        }
    }
}
