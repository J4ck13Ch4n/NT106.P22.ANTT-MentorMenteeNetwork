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
using System.Text.Json;
using System.Net.Http;
using System.Collections.Concurrent;


namespace MentorMenteeUI
{
    public partial class NhanTinControl : UserControl
    {
        private HubConnection connection;
        private int userId;
        private string userName;
        private readonly Form loginForm;

        private PartnerInfo activeChatPartner = null;
        private Dictionary<int, List<MessageEntry>> chatHistories = new Dictionary<int, List<MessageEntry>>();


        //lưu id người nhận được chọn
        private int? _selectedRecipientId = null;
        private string _selectedRecipientUsername = null;
        private class UserSuggestion
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Role { get; set; }
            public string Email { get; set; }
            public override string ToString() => $"{Username} - {Email}";
        }

        private class PartnerInfo
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public override string ToString() => Username;
        }

        public NhanTinControl(int userId, Form loginForm, string userName)
        {
            InitializeComponent();
            this.userId = userId;
            this.loginForm = loginForm;
            this.userName = userName;
            this.lbUserSuggestions.BringToFront();
            this.lbUserSuggestions.DoubleClick += LbUserSuggestions_DoubleClick;

            tbNguoiNhan.TextChanged += TbNguoiNhan_TextChanged;

            lbDSTroChuyen.SelectedIndexChanged += LstConversations_SelectedIndexChanged;
            ConnectSignalR();
        }
        private async void TbNguoiNhan_TextChanged(object sender, EventArgs e)
        {

            string searchText = tbNguoiNhan.Text.Trim();
            if (!string.IsNullOrWhiteSpace(searchText)) 
            {
                this.lbUserSuggestions.Visible = false;
                this.lbUserSuggestions.Items.Clear();

                _selectedRecipientId = null;
                _selectedRecipientUsername = null;

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var response = await httpClient.GetAsync($"https://localhost:5268/api/user/search?query={Uri.EscapeDataString(searchText)}");

                        //kiểm tra response có thành công không
                        var jsonResponsed = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"API Response: {jsonResponsed}");

                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();
                            var usersFound = JsonSerializer.Deserialize<List<UserSuggestion>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                            lbUserSuggestions.Items.Clear();
                            if (usersFound != null)
                            {
                                foreach (var user in usersFound)
                                {
                                    // Không hiển thị chính mình
                                    if (!user.Username.Equals(this.userName, StringComparison.OrdinalIgnoreCase))
                                        lbUserSuggestions.Items.Add(user);
                                }
                                lbUserSuggestions.Visible = lbUserSuggestions.Items.Count > 0;
                                if (lbUserSuggestions.Visible)
                                    lbUserSuggestions.BringToFront();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        this.lbUserSuggestions.Visible = false;
                        this.lbUserSuggestions.Items.Clear();
                    }
                }
            }
            else
            {
                this.lbUserSuggestions.Visible = false;
                this.lbUserSuggestions.Items.Clear();
                _selectedRecipientId = null;
                _selectedRecipientUsername = null;
                bGui.Enabled = false;
            }
        }

        private void LbUserSuggestions_DoubleClick(object sender, EventArgs e)
        {
            var suggestionsBox = sender as ListBox;
            if (suggestionsBox?.SelectedItem is UserSuggestion selectedUser)
            {
                tbNguoiNhan.Text = $"{selectedUser.Username} - {selectedUser.Email}";
                _selectedRecipientId = selectedUser.Id;
                _selectedRecipientUsername = selectedUser.Username;
                suggestionsBox.Visible = false;
                tbNguoiNhan.Focus();
                bGui.Enabled = true;
            }
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

            connection.On<string, string, string, DateTime>("ReceiveMessage", (messageSender, messageContent, originalIntendedRecipient, timestamp) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    PartnerInfo chatPartnerObj = null;
                    bool isMyMessageForDisplay = messageSender == userName;

                    if (isMyMessageForDisplay)
                    {
                        chatPartnerObj = lbDSTroChuyen.Items
                            .OfType<PartnerInfo>()
                            .FirstOrDefault(p => p.Username == originalIntendedRecipient);
                    }
                    else
                    {
                        chatPartnerObj = lbDSTroChuyen.Items
                            .OfType<PartnerInfo>()
                            .FirstOrDefault(p => p.Username == messageSender);
                    }

                    if (chatPartnerObj == null)
                    {
                        // Nếu không có, tạm thời hiển thị dạng cũ
                        rtbKhungTroChuyen.SelectionAlignment = HorizontalAlignment.Left;
                        rtbKhungTroChuyen.SelectionColor = Color.Gray;
                        rtbKhungTroChuyen.AppendText($"{messageSender}: {messageContent} [{timestamp.ToLocalTime():HH:mm}]\n");
                        rtbKhungTroChuyen.SelectionColor = rtbKhungTroChuyen.ForeColor;
                        return;
                    }

                    int partnerId = chatPartnerObj.Id;

                    if (!chatHistories.ContainsKey(partnerId))
                    {
                        chatHistories[partnerId] = new List<MessageEntry>();
                    }
                    chatHistories[partnerId].Add(new MessageEntry
                    {
                        SenderUsername = messageSender,
                        Content = messageContent,
                        Timestamp = timestamp.ToLocalTime(),
                        IsMyMessage = isMyMessageForDisplay
                    });
                    chatHistories[partnerId] = chatHistories[partnerId].OrderBy(m => m.Timestamp).ToList();

                    object previouslySelectedItem = lbDSTroChuyen.SelectedItem;
                    bool wasHandlerAttached = true;
                    try
                    {
                        lbDSTroChuyen.SelectedIndexChanged -= LstConversations_SelectedIndexChanged;
                    }
                    catch { wasHandlerAttached = false; }

                    if (!lbDSTroChuyen.Items.Contains(chatPartnerObj))
                    {
                        lbDSTroChuyen.Items.Insert(0, chatPartnerObj);
                    }
                    else if (lbDSTroChuyen.Items.IndexOf(chatPartnerObj) > 0)
                    {
                        lbDSTroChuyen.Items.Remove(chatPartnerObj);
                        lbDSTroChuyen.Items.Insert(0, chatPartnerObj);
                    }

                    if (activeChatPartner != null && activeChatPartner.Id == chatPartnerObj.Id && lbDSTroChuyen.Items.Contains(chatPartnerObj))
                    {
                        lbDSTroChuyen.SelectedItem = chatPartnerObj;
                    }
                    else if (previouslySelectedItem != null && lbDSTroChuyen.Items.Contains(previouslySelectedItem))
                    {
                        lbDSTroChuyen.SelectedItem = previouslySelectedItem;
                    }
                    else if (lbDSTroChuyen.Items.Contains(chatPartnerObj))
                    {
                        lbDSTroChuyen.SelectedItem = chatPartnerObj;
                    }

                    if (wasHandlerAttached) lbDSTroChuyen.SelectedIndexChanged += LstConversations_SelectedIndexChanged;

                    if (activeChatPartner != null && activeChatPartner.Id == chatPartnerObj.Id)
                    {
                        string displayUser = isMyMessageForDisplay ? "Bạn" : messageSender;

                        rtbKhungTroChuyen.SelectionStart = rtbKhungTroChuyen.TextLength;
                        rtbKhungTroChuyen.SelectionLength = 0;

                        if (isMyMessageForDisplay)
                        {
                            rtbKhungTroChuyen.SelectionAlignment = HorizontalAlignment.Right;
                            rtbKhungTroChuyen.SelectionColor = Color.Blue;
                        }
                        else
                        {
                            rtbKhungTroChuyen.SelectionAlignment = HorizontalAlignment.Left;
                        }

                        rtbKhungTroChuyen.AppendText($"[{displayUser} - {timestamp.ToLocalTime():HH:mm}]\n");

                        rtbKhungTroChuyen.AppendText($"{messageContent}\n\n");

                        rtbKhungTroChuyen.SelectionColor = rtbKhungTroChuyen.ForeColor;

                        rtbKhungTroChuyen.ScrollToCaret();
                    }
                }));
            });

            // Handler để nhận danh sách các cuộc trò chuyện
            connection.On<List<PartnerInfo>>("ReceiveConversationPartners", (partners) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    lbDSTroChuyen.BeginUpdate();
                    object currentSelection = lbDSTroChuyen.SelectedItem;
                    lbDSTroChuyen.Items.Clear();
                    foreach (var partner in partners.OrderBy(p => p))
                    {
                        if (!string.IsNullOrEmpty(partner.Username) && partner.Username != this.userName)
                        {
                            lbDSTroChuyen.Items.Add(partner);
                        }
                    }
                    if (currentSelection != null && lbDSTroChuyen.Items.Contains(currentSelection))
                    {
                        lbDSTroChuyen.SelectedItem = currentSelection;
                    }
                    lbDSTroChuyen.EndUpdate();
                }));
            });

            // Handler để nhận lịch sử chat
            connection.On<int, List<MessageEntry>, string>("ReceiveChatHistory", (partnerId, messages, errorMessage) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        rtbKhungTroChuyen.AppendText($"Lỗi tải lịch sử với ID {partnerId}: {errorMessage}\n");
                        return;
                    }

                    if (activeChatPartner == null) return;

                    chatHistories[partnerId] = messages.OrderBy(m => m.Timestamp).ToList();

                    rtbKhungTroChuyen.Clear();

                    var processedMessages = messages.Select(m => new MessageEntry
                    {
                        SenderUsername = m.SenderUsername,
                        Content = m.Content,
                        Timestamp = m.Timestamp,
                        IsMyMessage = (m.SenderUsername == this.userName)
                    }).OrderBy(m => m.Timestamp).ToList();

                    chatHistories[partnerId] = processedMessages;

                    rtbKhungTroChuyen.Clear();
                    foreach (var msg in chatHistories[partnerId])
                    {
                        string displayUser = msg.IsMyMessage ? "Bạn" : msg.SenderUsername;
                        // rtbKhungTroChuyen.AppendText("\n"); // Dòng trống phân cách

                        rtbKhungTroChuyen.SelectionStart = rtbKhungTroChuyen.TextLength;
                        rtbKhungTroChuyen.SelectionLength = 0;

                        if (msg.IsMyMessage)
                        {
                            rtbKhungTroChuyen.SelectionAlignment = HorizontalAlignment.Right;
                            rtbKhungTroChuyen.SelectionColor = Color.Blue;
                        }
                        else
                        {
                            rtbKhungTroChuyen.SelectionAlignment = HorizontalAlignment.Left;
                            rtbKhungTroChuyen.SelectionColor = Color.DarkGreen;
                        }

                        rtbKhungTroChuyen.AppendText($"[{displayUser} - {msg.Timestamp.ToLocalTime():HH:mm}]\n");
                        rtbKhungTroChuyen.AppendText($"{msg.Content}\n\n"); // Thêm dòng trống sau tin nhắn

                        rtbKhungTroChuyen.SelectionColor = rtbKhungTroChuyen.ForeColor;
                    }
                    rtbKhungTroChuyen.ScrollToCaret();
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

            int recipientId;
            string recipientUsername;

            // nếu vừa chọn gợi ý, dùng gợi ý
            if (_selectedRecipientId != null && _selectedRecipientUsername != null)
            {
                recipientId = _selectedRecipientId.Value;
                recipientUsername = _selectedRecipientUsername;
            }
            // Nếu đã chọn từ danh sách trò chuyện (bên trái)
            else if (activeChatPartner != null)
            {
                recipientId = activeChatPartner.Id;
                recipientUsername = activeChatPartner.Username;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người nhận từ danh sách gợi ý hoặc trò chuyện.");
                return;
            }

            try
            {
                await connection.InvokeAsync("SendPrivateMessageById", userId, recipientId, message);
                tbTinNhan.Clear();
                // Reset _selectedRecipientId nếu cần
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

        private async void LstConversations_SelectedIndexChanged(object sender, EventArgs e) // Thêm async
        {
            if (lbDSTroChuyen.SelectedItem is PartnerInfo partnerInfo)
            {
                activeChatPartner = partnerInfo;
                tbNguoiNhan.Text = partnerInfo.Username;
                tbNguoiNhan.ReadOnly = true;
                bGui.Enabled = true;
                rtbKhungTroChuyen.Clear();

                if (connection != null && connection.State == HubConnectionState.Connected)
                {
                    rtbKhungTroChuyen.AppendText($"Đang tải lịch sử với {activeChatPartner}...\n");
                    try
                    {
                        await connection.InvokeAsync("LoadChatHistory", partnerInfo.Id);
                    }
                    catch (Exception ex)
                    {
                        rtbKhungTroChuyen.AppendText($"Lỗi khi yêu cầu lịch sử chat: {ex.Message}\n");
                        MessageBox.Show($"Lỗi khi yêu cầu lịch sử chat: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    rtbKhungTroChuyen.AppendText("Lỗi: Chưa kết nối tới server để tải lịch sử.\n");
                    MessageBox.Show("Chưa kết nối tới server để tải lịch sử.", "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                // reset trạng thái UI
                activeChatPartner = null;
                tbNguoiNhan.Text = "";
                tbNguoiNhan.ReadOnly = false;
                rtbKhungTroChuyen.Clear();
                bGui.Enabled = false;
            }


        }

    }

    public class MessageEntry
    {
        public string SenderUsername { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } 
        public bool IsMyMessage { get; set; }
    }
}