using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;

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
            _ = LoadFriendsToConversationList(); // Tải bạn bè vào danh sách chat khi khởi tạo
        }

        private async void TbNguoiNhan_TextChanged(object sender, EventArgs e)
        {
            string searchText = tbNguoiNhan.Text.Trim();
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                lbUserSuggestions.Visible = false;
                lbUserSuggestions.Items.Clear();

                _selectedRecipientId = null;
                _selectedRecipientUsername = null;

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var response = await httpClient.GetAsync($"https://localhost:5268/api/user/search?query={Uri.EscapeDataString(searchText)}");
                        var jsonResponsed = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"API Response: {jsonResponsed}");

                        if (response.IsSuccessStatusCode)
                        {
                            var usersFound = JsonSerializer.Deserialize<List<UserSuggestion>>(jsonResponsed, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                            lbUserSuggestions.Items.Clear();
                            if (usersFound != null)
                            {
                                foreach (var user in usersFound)
                                {
                                    if (!user.Username.Equals(this.userName, StringComparison.OrdinalIgnoreCase))
                                        lbUserSuggestions.Items.Add(user);
                                }
                                lbUserSuggestions.Visible = lbUserSuggestions.Items.Count > 0;
                                if (lbUserSuggestions.Visible)
                                    lbUserSuggestions.BringToFront();
                            }
                        }
                    }
                    catch
                    {
                        lbUserSuggestions.Visible = false;
                        lbUserSuggestions.Items.Clear();
                    }
                }
            }
            else
            {
                lbUserSuggestions.Visible = false;
                lbUserSuggestions.Items.Clear();
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

            connection.On<List<PartnerInfo>>("ReceiveConversationPartners", (partners) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    lbDSTroChuyen.BeginUpdate();
                    object currentSelection = lbDSTroChuyen.SelectedItem;
                    lbDSTroChuyen.Items.Clear();
                    foreach (var partner in partners.OrderBy(p => p.Username))
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

            connection.On<int, List<MessageEntry>, string>("ReceiveChatHistory", (partnerId, messages, errorMessage) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        rtbKhungTroChuyen.AppendText($"Lỗi tải lịch sử với ID {partnerId}: {errorMessage}\n");
                        return;
                    }

                    if (activeChatPartner == null || activeChatPartner.Id != partnerId) return;

                    chatHistories[partnerId] = messages.OrderBy(m => m.Timestamp).ToList();
                    rtbKhungTroChuyen.Clear();

                    foreach (var msg in chatHistories[partnerId])
                    {
                        string displayUser = msg.IsMyMessage ? "Bạn" : msg.SenderUsername;

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
                        rtbKhungTroChuyen.AppendText($"{msg.Content}\n\n");
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

            if (_selectedRecipientId != null && _selectedRecipientUsername != null)
            {
                recipientId = _selectedRecipientId.Value;
                recipientUsername = _selectedRecipientUsername;
            }
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

        private List<FriendInfo> _friendListCache;
        private async Task LoadFriendsToConversationList()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (!string.IsNullOrEmpty(DangNhap.JwtToken))
                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", DangNhap.JwtToken);
                    var response = await httpClient.GetAsync($"https://localhost:5268/api/friendship/friends/{userId}");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var friends = System.Text.Json.JsonSerializer.Deserialize<List<FriendInfo>>(json, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        _friendListCache = friends;
                        if (friends != null)
                        {
                            lbDSTroChuyen.Items.Clear();
                            foreach (var f in friends)
                            {
                                if (f.FriendName != userName)
                                {
                                    var partner = new PartnerInfo { Id = f.FriendId, Username = f.FriendName };
                                    lbDSTroChuyen.Items.Add(partner);
                                    if (!chatHistories.ContainsKey(f.FriendId))
                                    {
                                        chatHistories[f.FriendId] = new List<MessageEntry>();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private async void LstConversations_SelectedIndexChanged(object sender, EventArgs e)
        {
            var partnerInfo = lbDSTroChuyen.SelectedItem as PartnerInfo;
            if (partnerInfo == null)
            {
                activeChatPartner = null;
                tbNguoiNhan.Text = "";
                tbNguoiNhan.ReadOnly = false;
                rtbKhungTroChuyen.Clear();
                bGui.Enabled = false;
                return;
            }

            activeChatPartner = partnerInfo;
            tbNguoiNhan.ReadOnly = true;
            tbNguoiNhan.Text = activeChatPartner.Username;
            bGui.Enabled = true;
            rtbKhungTroChuyen.Clear();

            if (chatHistories.ContainsKey(activeChatPartner.Id) && chatHistories[activeChatPartner.Id] != null && chatHistories[activeChatPartner.Id].Count > 0)
            {
                foreach (var msg in chatHistories[activeChatPartner.Id])
                {
                    string displayUser = msg.IsMyMessage ? "Bạn" : msg.SenderUsername;
                    rtbKhungTroChuyen.SelectionStart = rtbKhungTroChuyen.TextLength;
                    rtbKhungTroChuyen.SelectionLength = 0;
                    rtbKhungTroChuyen.SelectionAlignment = msg.IsMyMessage ? HorizontalAlignment.Right : HorizontalAlignment.Left;
                    rtbKhungTroChuyen.SelectionColor = msg.IsMyMessage ? Color.Blue : Color.DarkGreen;
                    rtbKhungTroChuyen.AppendText($"[{displayUser} - {msg.Timestamp:HH:mm}]\n");
                    rtbKhungTroChuyen.AppendText($"{msg.Content}\n\n");
                    rtbKhungTroChuyen.SelectionColor = rtbKhungTroChuyen.ForeColor;
                }
                rtbKhungTroChuyen.ScrollToCaret();
                return;
            }

            if (connection != null && connection.State == HubConnectionState.Connected)
            {
                rtbKhungTroChuyen.AppendText($"Đang tải lịch sử với {activeChatPartner.Username}...\n");
                try
                {
                    await connection.InvokeAsync("LoadChatHistory", activeChatPartner.Id);
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

        private class FriendInfo
        {
            public int FriendId { get; set; }
            public string FriendName { get; set; }
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