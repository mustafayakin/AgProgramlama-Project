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
 
    public partial class regs : Form
    {
        SqlConnection connection = new SqlConnection("server=localhost;database=agprogramlama;integrated security=True");
        public regs()
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
        private void createAcoount_Click(object sender, EventArgs e)
        {
            if(sifre1.Text.ToString() != sifre2.Text.ToString())
            {
                MessageBox.Show("Password doesn't match!","match error x64",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else if(sifre1.Text == ""|| sifre2.Text == "" || textBox1.Text == "")
            {
                MessageBox.Show("Empty Field!", "empty error x64", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                connection.Open();
                string sorgu = "select*from users where username=@username";
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand(sorgu, connection);
                cmd.Parameters.AddWithValue("@username", textBox1.Text);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    MessageBox.Show("Ayni adla bir baska kullanici var", "Benzersizlik hatasi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    connection.Close();
                    string hashPass = HashPassword(sifre1.Text);
                    sorgu = "INSERT INTO users(username,password,isOnline,port) VALUES (@username,@password,@isOnline,@port)";
                    cmd = new SqlCommand(sorgu, connection);
                    cmd.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd.Parameters.AddWithValue("@password", hashPass);
                    cmd.Parameters.AddWithValue("@isOnline", 0);
                    cmd.Parameters.AddWithValue("@port", numericUpDown1.Value.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                    MessageBox.Show("Kaydınız başarıyla alınmıştır. Aramıza Hoşgeldin " + textBox1.Text, "Kayıt Başarılı !!");

                    Form1 giris = new Form1();
                    giris.Show();
                    this.Hide();
                }
            }
            

        }

        private void regs_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 giris = new Form1();
            giris.Show();
            this.Hide();
        }
    }
}
