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
        private string userId; 
        private string userName; 
        private readonly Form loginForm; 

        private string activeChatPartner = null; 
        private Dictionary<string, List<MessageEntry>> chatHistories = new Dictionary<string, List<MessageEntry>>();

        public NhanTinControl(string userId, Form loginForm, string userName)
        {
            InitializeComponent();
            this.userId = userId;
            this.loginForm = loginForm;
            this.userName = userName; 

            lbDSTroChuyen.SelectedIndexChanged += LstConversations_SelectedIndexChanged;

            ConnectSignalR();
        }

        private async void ConnectSignalR()
        {
            if (string.IsNullOrEmpty(this.userName))
            {
                rtbKhungTroChuyen.AppendText("Lỗi: Không có thông tin người dùng để kết nối chat.\n");
                return;
            }

            connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:5268/chathub") 
                .WithAutomaticReconnect()
                .Build();

            connection.On<string, string, string>("ReceiveMessage", (messageSender, messageContent, originalIntendedRecipient) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    string chatPartner;
                    bool isMyMessage = messageSender == userName;

                    if (isMyMessage)
                    {
                        chatPartner = originalIntendedRecipient;
                    }
                    else
                    {
                        chatPartner = messageSender;
                    }

                    if (string.IsNullOrEmpty(chatPartner))
                    {
                        rtbKhungTroChuyen.AppendText($"{messageSender}: {messageContent}\n"); 
                        return;
                    }

                    if (!chatHistories.ContainsKey(chatPartner))
                    {
                        chatHistories[chatPartner] = new List<MessageEntry>();
                    }
                    chatHistories[chatPartner].Add(new MessageEntry { Sender = messageSender, Content = messageContent, Timestamp = DateTime.Now });

                    if (!lbDSTroChuyen.Items.Contains(chatPartner))
                    {
                        lbDSTroChuyen.Items.Insert(0, chatPartner); 
                    }
                    else 
                    {
                        lbDSTroChuyen.Items.Remove(chatPartner);
                        lbDSTroChuyen.Items.Insert(0, chatPartner);
                    }

                    if (activeChatPartner == chatPartner)
                    {
                        string displayUser = isMyMessage ? "Bạn" : messageSender;
                        rtbKhungTroChuyen.AppendText($"{displayUser}: {messageContent}\n");
                    }
                    else
                    {
                        lbDSTroChuyen.Items[lbDSTroChuyen.Items.IndexOf(chatPartner)] = chatPartner;
                    }
                }));
            });

            try
            {
                await connection.StartAsync();
                rtbKhungTroChuyen.AppendText("✅ Đã kết nối tới máy chủ SignalR!\n");
                if (!string.IsNullOrEmpty(userName))
                {
                    await connection.InvokeAsync("RegisterUser", userName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi kết nối SignalR: " + ex.Message);
                rtbKhungTroChuyen.AppendText($"❌ Lỗi kết nối SignalR: {ex.Message}\n");
            }
        }

        private void LstConversations_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbDSTroChuyen.SelectedItem == null)
            {
                activeChatPartner = null;
                tbNguoiNhan.Text = "";
                tbNguoiNhan.ReadOnly = false;
                rtbKhungTroChuyen.Clear();
                return;
            }

            activeChatPartner = lbDSTroChuyen.SelectedItem.ToString();
            tbNguoiNhan.Text = activeChatPartner; 
            tbNguoiNhan.ReadOnly = true; 

            rtbKhungTroChuyen.Clear();
            if (chatHistories.ContainsKey(activeChatPartner))
            {
                foreach (var msgEntry in chatHistories[activeChatPartner])
                {
                    string displayUser = (msgEntry.Sender == userName) ? "Bạn" : msgEntry.Sender;
                    rtbKhungTroChuyen.AppendText($"{displayUser}: {msgEntry.Content}\n");
                }
            }
            // TODO: Xóa chỉ báo tin nhắn mới nếu có
            // if (lstConversations.SelectedItem.ToString().EndsWith(" (*)"))
            // {
            //    lstConversations.Items[lstConversations.SelectedIndex] = activeChatPartner;
            // }
        }

        private async void bGui_Click(object sender, EventArgs e)
        {
            if (connection == null || connection.State != HubConnectionState.Connected)
            {
                MessageBox.Show("SignalR chưa kết nối!");
                return;
            }

            var message = tbTinNhan.Text.Trim();
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Vui lòng nhập nội dung tin nhắn.");
                return;
            }

            string recipientForThisMessage;

            if (!string.IsNullOrEmpty(activeChatPartner))
            {
                recipientForThisMessage = activeChatPartner;
            }
            else
            {
                recipientForThisMessage = tbNguoiNhan.Text.Trim();
                if (string.IsNullOrEmpty(recipientForThisMessage))
                {
                    MessageBox.Show("Vui lòng chọn một cuộc trò chuyện hoặc nhập tên người nhận.");
                    return;
                }
                if (recipientForThisMessage == userName)
                {
                    MessageBox.Show("Bạn không thể tự gửi tin nhắn cho chính mình theo cách này. Nếu muốn ghi chú, hãy gửi cho một người dùng khác (có thể là tài khoản test của bạn).");
                    return;
                }
            }

            try
            {
                await connection.InvokeAsync("SendPrivateMessage", userName, recipientForThisMessage, message);
                
                tbTinNhan.Clear();

                if (string.IsNullOrEmpty(activeChatPartner) || activeChatPartner != recipientForThisMessage)
                {
                    if (!lbDSTroChuyen.Items.Contains(recipientForThisMessage))
                    {
                        lbDSTroChuyen.Items.Insert(0, recipientForThisMessage);
                    }
                    lbDSTroChuyen.SelectedItem = recipientForThisMessage;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gửi tin nhắn: " + ex.Message);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) 
                {
                    components.Dispose();
                }

                if (connection != null)
                {
                    Task.Run(async () => await connection.StopAsync()).Wait();
                    Task.Run(async () => await connection.DisposeAsync()).Wait();
                    connection = null;
                }
            }
            base.Dispose(disposing);
        }

        // ... (rtbKhungTroChuyen_TextChanged, rtbNguoiNhan_TextChanged nếu có logic) ...
    }

    public class MessageEntry
    {
        public string Sender { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } 
    }
}