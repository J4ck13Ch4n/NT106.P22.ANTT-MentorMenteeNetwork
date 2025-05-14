using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Security.Cryptography;
using MentorMentee.Cryptography.Helpers;
using Microsoft.AspNetCore.SignalR.Client;

namespace MentorMenteeUI
{
    public partial class NhanTinControl : UserControl
    {
        private HubConnection connection;
        private string currentUsername = "user1"; // TODO: Lấy username thực tế từ session hoặc biến toàn cục

        public NhanTinControl()
        {
            InitializeComponent();
            ConnectSignalR();
        }

        private async void ConnectSignalR()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5268/chathub")
                .WithAutomaticReconnect()
                .Build();

            // Nhận tin nhắn từ server
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    rtbKhungTroChuyen.AppendText($"{user}: {message}\n");
                }));
            });

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("RegisterUser", currentUsername); // nếu backend hỗ trợ đăng ký user
                rtbKhungTroChuyen.AppendText("✅ Đã kết nối tới máy chủ SignalR!\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi kết nối SignalR: " + ex.Message);
            }
        }

        private string EncryptMessageRSA(string plainText, RSAParameters publicKey)
        {
            using (RSA rsa = RSA.Create())
            {
                rsa.ImportParameters(publicKey);
                byte[] data = Encoding.UTF8.GetBytes(plainText);
                byte[] encrypted = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
                return Convert.ToBase64String(encrypted);
            }
        }

        private RSAParameters GetRecipientPublicKey()
        {
            using (RSA rsa = RSA.Create())
            {
                return rsa.ExportParameters(false); 
            }
        }

        private async void bGui_Click(object sender, EventArgs e)
        {
            if (connection.State == HubConnectionState.Connected)
            {
                var message = tbTinNhan.Text.Trim();
                if (!string.IsNullOrEmpty(message))
                {
                    try
                    {
                        RSAParameters recipientPublicKey = GetRecipientPublicKey(); 
                        string encryptedMessage = EncryptMessageRSA(message, recipientPublicKey);
                        await connection.InvokeAsync("SendMessage", currentUsername, encryptedMessage);
                        rtbKhungTroChuyen.AppendText("Bạn: " + message + "\n");
                        tbTinNhan.Clear(); 
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi gửi tin nhắn: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("SignalR chưa kết nối!");
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (connection != null)
                {
                    connection.StopAsync().Wait();
                    connection.DisposeAsync().AsTask().Wait();
                }
            }
            base.Dispose(disposing);
        }

        private void rtbKhungTroChuyen_TextChanged(object sender, EventArgs e)
        {

        }
    }
}



