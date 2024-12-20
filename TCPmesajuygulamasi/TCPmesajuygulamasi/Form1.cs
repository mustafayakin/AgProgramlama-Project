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
using System.Security.Cryptography;


namespace TCPmesajuygulamasi
{
    public partial class Form1 : Form
    {
        string ID="0";
        SqlConnection connection = new SqlConnection("server=localhost;database=agprogramlama;integrated security=True");
        public Form1()
        {
            InitializeComponent();
        }
        
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private void login_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Kullanici Adi Veya Şifre Boş Bırakılamaz!!", "Boş Değer Hatası", MessageBoxButtons.OK);
                return;
            }
            string sorgu = "select*from users where username=@username AND password=@pass";
            SqlDataReader dr;
            SqlCommand komut = new SqlCommand(sorgu, connection);
            komut.Parameters.AddWithValue("@username", textBox1.Text);
            komut.Parameters.AddWithValue("@pass", HashPassword(textBox2.Text));
            connection.Open();
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                string hosgeldinMesaj = "Hoşgeldin " + dr.GetString(1).ToUpper() + "\nİyi Çalışmalar"; ID = dr.GetByte(0).ToString();
                MessageBox.Show(hosgeldinMesaj, "Hoşgeldin!");
                sorgu = "UPDATE users set isOnline = @isOnline WHERE id ="+dr.GetByte(0).ToString();
                connection.Close();
                komut = new SqlCommand(sorgu, connection);
                komut.Parameters.AddWithValue("@isOnline", 1);
                connection.Open();
                komut.ExecuteNonQuery();
                connection.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı Veya Şifre Yanlış!!", "Yanlışlık Var!!");
            }
            connection.Close();
        }

        private void createAcoount_Click(object sender, EventArgs e)
        {
            regs regform = new regs();
            regform.Show();
            this.Hide();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if(ID != "0")
            //{
            //    sorgu = "UPDATE users set isOnline = @isOnline WHERE id =" + dr.GetByte(0).ToString();
            //    connection.Close();
            //    komut = new SqlCommand(sorgu, connection);
            //    komut.Parameters.AddWithValue("@isOnline", 1);
            //    connection.Open();
            //    komut.ExecuteNonQuery();
            //    connection.Close();
            //}
            Application.Exit();
        }
    }
}
