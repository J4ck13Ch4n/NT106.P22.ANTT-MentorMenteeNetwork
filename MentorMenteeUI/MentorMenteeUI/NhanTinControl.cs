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
using System.IO; 


namespace MentorMenteeUI
{
    public partial class NhanTinControl : UserControl
    {
        private HubConnection connection;
        private string userId, userName;
        private readonly Form loginForm;
        public NhanTinControl(string userId, Form loginForm, string userName)
        {
            InitializeComponent();
            this.userId = userId;
            this.loginForm = loginForm;
            this.userName = userName;
            ConnectSignalR();
        }

        private async void ConnectSignalR()
        {
            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5268/chathub")
                .WithAutomaticReconnect()
                .Build();

            connection.On<string, string>("ReceiveMessage", (receivedUser, receivedMessage) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    string displayUser = (receivedUser == userName) ? "Bạn" : receivedUser;
                    rtbKhungTroChuyen.AppendText($"{displayUser}: {receivedMessage}\n");
                }));
            });

            try
            {
                await connection.StartAsync();
                if (!string.IsNullOrEmpty(userName))
                {
                    await connection.InvokeAsync("RegisterUser", userName);
                }
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
                var recipientUsername = tbNguoiNhan.Text.Trim();
                if (string.IsNullOrEmpty(recipientUsername))
                {
                    MessageBox.Show("Vui lòng nhập tên người nhận.");
                    return;
                }

                if (string.IsNullOrEmpty(message))
                {
                    MessageBox.Show("Vui lòng nhập nội dung tin nhắn.");
                    return;
                }
                if (!string.IsNullOrEmpty(message))
                {
                    try
                    {
                        //RSAParameters recipientPublicKey = GetRecipientPublicKey(); 
                        //string encryptedMessage = EncryptMessageRSA(message, recipientPublicKey);
                        await connection.InvokeAsync("SendPrivateMessage", userName, recipientUsername, message);
                        //rtbKhungTroChuyen.AppendText("Bạn: " + message + "\n");
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

        private void rtbNguoiNhan_TextChanged(object sender, EventArgs e)
        {

        }
    }
}



