using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TCPmesajuygulamasi
{
    public partial class withTCP : Form
    {
        string kullanici_id;
        TcpListener serverListener;
        public withTCP(string k_id)
        {
            kullanici_id = k_id;
            InitializeComponent();
        }

        public class UserStatus
        {
            public string Username { get; set; }
            public bool IsOnline { get; set; }
        }

        private async Task SendMessageToServerAsync(string ipAddress, int port, string encryptedMessage)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    await client.ConnectAsync(ipAddress, port);
                    using (NetworkStream stream = client.GetStream())
                    {
                        byte[] data = Encoding.UTF8.GetBytes(encryptedMessage);
                        await stream.WriteAsync(data, 0, data.Length);

                        // ACK bekle
                        byte[] ackBuffer = new byte[1024];
                        int bytesRead = await stream.ReadAsync(ackBuffer, 0, ackBuffer.Length);
                        string ackResponse = Encoding.UTF8.GetString(ackBuffer, 0, bytesRead);

                        if (ackResponse == "ACK")
                        {
                            MessageBox.Show("Mesaj başarıyla gönderildi ve ACK alındı!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Mesaj gönderilirken hata oluştu: {ex.Message}");
            }
        }

        private async Task SaveMessageToDatabaseAsync(int senderId, int receiverId, string encryptedMessage, bool isDelivered)
        {
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO messages (sender_id, receiver_id, message, sent_at, isDelivered) VALUES (@senderId, @receiverId, @message, @sentAt, @isDelivered)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@senderId", senderId);
                cmd.Parameters.AddWithValue("@receiverId", receiverId);
                cmd.Parameters.AddWithValue("@message", encryptedMessage);
                cmd.Parameters.AddWithValue("@sentAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@isDelivered", isDelivered);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public static List<UserStatus> GetAllUsers()
        {
            List<UserStatus> users = new List<UserStatus>();
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT username, isOnline FROM users";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new UserStatus
                            {
                                Username = reader["username"].ToString(),
                                IsOnline = Convert.ToBoolean(reader["isOnline"])
                            });
                        }
                    }
                }
            }

            return users;
        }

        private void RefreshUserList()
        {
            listView1.Items.Clear(); // Mevcut listeyi temizle.

            List<UserStatus> users = GetAllUsers();

            string currentUsername = ""; // Giriş yapan kullanıcının adını tutacak.

            // Giriş yapan kullanıcının adını almak için.
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sorgu = "SELECT username FROM users WHERE id = @id";
                using (SqlCommand cmd = new SqlCommand(sorgu, connection))
                {
                    cmd.Parameters.AddWithValue("@id", kullanici_id);
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        currentUsername = dr["username"].ToString();
                        label1.Text = currentUsername; // Kullanıcı adını etikette göster.
                        label1.Visible = true;
                    }
                }
            }

            // Listeyi doldururken giriş yapan kullanıcıyı hariç tut.
            foreach (var user in users)
            {
                if (user.Username == currentUsername)
                {
                    continue; // Giriş yapan kullanıcıyı listeye ekleme.
                }

                ListViewItem item = new ListViewItem(user.Username);

                if (user.IsOnline)
                {
                    // Online kullanıcılar yeşil renkte gösterilir.
                    item.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    // Offline kullanıcılar kırmızı renkte gösterilir.
                    item.ForeColor = System.Drawing.Color.Red;
                }

                listView1.Items.Add(item);
            }
        }

        private string EncryptMessage(string message)
        {
            return AesEncryption.Encrypt(message);
        }

        private void SaveMessageToDatabase(int senderId, int receiverId, string encryptedMessage)
        {
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO messages (sender_id, receiver_id, message, sent_at, isDelivered) VALUES (@senderId, @receiverId, @message, @sentAt, @isDelivered)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@senderId", senderId);
                cmd.Parameters.AddWithValue("@receiverId", receiverId);
                cmd.Parameters.AddWithValue("@message", encryptedMessage);
                cmd.Parameters.AddWithValue("@sentAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@isDelivered", false);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        private bool IsUserOnline(int receiverId, out string ipAddress, out int port)
        {
            ipAddress = null;
            port = 0;
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ipAddress, port FROM users WHERE id = @id AND isOnline = 1";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", receiverId);

                connection.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    ipAddress = dr["ipAddress"].ToString();
                    port = Convert.ToInt32(dr["port"]);
                    return true;
                }
            }
            return false;
        }

        private void ShowReceivedMessage(string message)
        {
            string[] messageParts = message.Split(':');
            if (messageParts.Length < 2) return;

            string senderId = messageParts[0];
            string encryptedMessage = messageParts[1];

            // Eğer gönderici kendi ID'nizse mesajı işlemeyin
            if (senderId == kullanici_id) return;

            string decryptedMessage = DecryptMessage(encryptedMessage);

            Invoke((MethodInvoker)delegate
            {
                richTextBox2.AppendText($"[Gönderen ID {senderId}]: {decryptedMessage}\n");
            });
        }

        private void SaveReceivedMessageToDatabase(string message)
        {
            string decryptedMessage = DecryptMessage(message);

            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO messages (sender_id, receiver_id, message, sent_at, isDelivered) VALUES (@senderId, @receiverId, @message, @sentAt, @isDelivered)";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@senderId", 0); // Gönderen ID (dummy veri, uygun şekilde doldurun)
                cmd.Parameters.AddWithValue("@receiverId", int.Parse(kullanici_id));
                cmd.Parameters.AddWithValue("@message", decryptedMessage);
                cmd.Parameters.AddWithValue("@sentAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@isDelivered", true);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void ShowUndeliveredMessages()
        {
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT sender_id, message FROM messages WHERE receiver_id = @receiverId AND isDelivered = 0";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@receiverId", int.Parse(kullanici_id));

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string senderId = reader["sender_id"].ToString();
                    string encryptedMessage = reader["message"].ToString();

                    // Mesajı çöz ve göster
                    string decryptedMessage = DecryptMessage(encryptedMessage);
                    Invoke((MethodInvoker)(() =>
                    {
                        richTextBox2.AppendText($"[Gönderen ID {senderId}]: {decryptedMessage}\n");
                    }));

                    // Mesajı teslim edildi olarak işaretle
                    MarkMessageAsDelivered(senderId, kullanici_id);
                }
            }
        }

        private void MarkMessageAsDelivered(string senderId, string receiverId)
        {
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE messages SET isDelivered = 1 WHERE sender_id = @senderId AND receiver_id = @receiverId AND isDelivered = 0";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@senderId", senderId);
                cmd.Parameters.AddWithValue("@receiverId", receiverId);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }


        private string DecryptMessage(string encryptedMessage)
        {
            return AesEncryption.Decrypt(encryptedMessage);
        }

        private void withTCP_Load(object sender, EventArgs e)
        {
            ShowUndeliveredMessages();
        }
        private void withTCP_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (serverListener != null)
                {
                    isListening = false; // Sunucunun dinlemesini durdur
                    serverListener.Stop(); // Portu bırak
                    serverListener = null; // Sunucu nesnesini temizle
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Uygulama kapatılırken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<int> GetUserIdsByUsernames(string[] usernames)
        {
            List<int> userIds = new List<int>();
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                foreach (string username in usernames)
                {
                    string query = "SELECT id FROM users WHERE username = @username";
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            userIds.Add(Convert.ToInt32(result));
                        }
                    }
                }
            }

            return userIds;
        }


        private async void sent_Click(object sender, EventArgs e)
        {
            // `whoBox` içindeki kullanıcı adlarını al
            string[] selectedUsernames = whoBox.Text.Split(new[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            if (selectedUsernames.Length == 0)
            {
                MessageBox.Show("Lütfen bir veya birden fazla kullanıcı seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kullanıcıların ID'lerini al
            List<int> selectedUserIds = GetUserIdsByUsernames(selectedUsernames);

            // Mesaj metnini al
            string message = txtMessage.Text.Trim();

            if (!string.IsNullOrEmpty(message) && selectedUserIds.Count > 0)
            {
                foreach (var receiverId in selectedUserIds)
                {
                    string encryptedMessage = EncryptMessage(message);

                    // Mesajı veritabanına kaydet
                    await SaveMessageToDatabaseAsync(int.Parse(kullanici_id), receiverId, encryptedMessage, false);

                    // Kullanıcı online ise mesaj gönder
                    if (IsUserOnline(receiverId, out string ipAddress, out int port))
                    {
                        string formattedMessage = $"{kullanici_id}:{encryptedMessage}";
                        await SendMessageToServerAsync(ipAddress, port, formattedMessage);
                        await MarkMessageAsDeliveredAsync(Int32.Parse(kullanici_id), receiverId);
                    }
                }
                txtMessage.Clear(); // Mesaj gönderildikten sonra text kutusunu temizle
            }
            else
            {
                MessageBox.Show("Lütfen bir mesaj yazın ve kullanıcı(lar) seçin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async Task MarkMessageAsDeliveredAsync(int senderId, int receiverId)
        {
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE messages SET isDelivered = 1 WHERE sender_id = @senderId AND receiver_id = @receiverId AND isDelivered = 0";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@senderId", senderId);
                cmd.Parameters.AddWithValue("@receiverId", receiverId);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }
        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshUserList();
        }
        private void userSec_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                List<string> selectedUsernames = new List<string>();

                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    selectedUsernames.Add(item.Text); // Kullanıcı adını ekle
                }

                whoBox.Text = string.Join(", ", selectedUsernames); // Adları virgülle ayırarak göster
                sent.Visible = true; // Gönder butonunu aktif et
            }
            else
            {
                MessageBox.Show("Lütfen bir veya birden fazla kullanıcı seçin!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private volatile bool isListening = false;
        private void beServer_Click(object sender, EventArgs e)
        {
            try
            {
                string connectionString = "server=localhost;database=agprogramlama;integrated security=True";
                int port = 0;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT port FROM users WHERE id = @id";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@id", kullanici_id);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        port = Convert.ToInt32(reader["port"]);
                    }
                }

                if (port > 0)
                {
                    serverListener = new TcpListener(IPAddress.Any, port);
                    serverListener.Start();
                    isListening = true;

                    Task.Run(() =>
                    {
                        while (isListening)
                        {
                            try
                            {
                                TcpClient client = serverListener.AcceptTcpClient();
                                NetworkStream stream = client.GetStream();

                                byte[] buffer = new byte[1024];
                                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                                string receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                                string[] messageParts = receivedMessage.Split(':');
                                if (messageParts.Length < 2 || messageParts[0] == kullanici_id) continue;

                                string senderId = messageParts[0];
                                string encryptedMessage = messageParts[1];
                                string decryptedMessage = DecryptMessage(encryptedMessage);

                                Invoke((MethodInvoker)(() =>
                                {
                                    richTextBox2.AppendText($"[Gelen Gönderen ID {senderId}]: {decryptedMessage}\n");
                                }));

                                SaveReceivedMessageToDatabase(senderId, kullanici_id, encryptedMessage);

                                byte[] ackData = Encoding.UTF8.GetBytes("ACK");
                                stream.Write(ackData, 0, ackData.Length);
                                client.Close();
                            }
                            catch (SocketException ex)
                            {
                                if (ex.SocketErrorCode != SocketError.Interrupted)
                                {
                                    MessageBox.Show($"Dinleme sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    });

                    MessageBox.Show($"Sunucu {port} portunda başlatıldı.");
                }
                else
                {
                    MessageBox.Show("Sunucu başlatılamadı, geçerli bir port bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sunucu başlatılırken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void SaveReceivedMessageToDatabase(string senderId, string receiverId, string encryptedMessage)
        {
            string connectionString = "server=localhost;database=agprogramlama;integrated security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO messages (sender_id, receiver_id, message, sent_at, isDelivered) VALUES (@senderId, @receiverId, @message, @sentAt, @isDelivered)";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@senderId", senderId);
                cmd.Parameters.AddWithValue("@receiverId", receiverId);
                cmd.Parameters.AddWithValue("@message", encryptedMessage);
                cmd.Parameters.AddWithValue("@sentAt", DateTime.Now);
                cmd.Parameters.AddWithValue("@isDelivered", false);

                await connection.OpenAsync();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private void stopServer_Click(object sender, EventArgs e)
        {
            try
            {
                if (serverListener != null)
                {
                    isListening = false;
                    serverListener.Stop();
                    serverListener = null;
                    MessageBox.Show("Sunucu durduruldu ve port serbest bırakıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sunucu zaten durdurulmuş.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sunucu durdurulurken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}