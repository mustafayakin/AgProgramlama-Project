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
    public partial class updatePort : Form
    {
        string kullanici_id;
        public updatePort(string k_id)
        {
            kullanici_id = k_id;
            InitializeComponent();
        }
        SqlConnection connection = new SqlConnection("server = localhost; database = agprogramlama; integrated security = True");
        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE users set port=@port WHERE id=@id";
            SqlCommand cmd = new SqlCommand(sorgu, connection);
            cmd.Parameters.AddWithValue("@port", numericUpDown1.Value.ToString());
            cmd.Parameters.AddWithValue("@id", kullanici_id);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
            MessageBox.Show("Portunuz Guncellendi Yeni Port=" + numericUpDown1.Value.ToString(),"Basarili Sorgu",MessageBoxButtons.OK);
        }

        private void updatePort_Load(object sender, EventArgs e)
        {
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sorgu = "SELECT port FROM users WHERE id = @id";
                SqlCommand cmd = new SqlCommand(sorgu, connection);

                // Veritabanına bağlan
                connection.Open();

                // Parametreyi ekle
                cmd.Parameters.AddWithValue("@id", kullanici_id);

                // Sorguyu çalıştır
                SqlDataReader dr = cmd.ExecuteReader();

                // Eğer veri varsa
                if (dr.Read())
                {
                    // Port değeri varsa
                    if (dr["port"] != DBNull.Value)
                    {
                        // Portu integer olarak al
                        numericUpDown1.Value = Convert.ToInt32(dr["port"]);
                    }
                    else
                    {
                        // Port değeri yoksa, varsayılan değeri ayarla
                        numericUpDown1.Value = 5100;
                    }
                }

                // Bağlantıyı kapat
                connection.Close();
            }
        }
    }
}
