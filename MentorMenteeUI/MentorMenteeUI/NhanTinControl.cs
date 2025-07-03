using Microsoft.AspNetCore.SignalR.Client;

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

        public NhanTinControl(int userId, Form loginForm, string userName)
        {
            InitializeComponent();
            this.userId = userId;
            this.loginForm = loginForm;
            this.userName = userName;

            lbUserSuggestions.BringToFront();
            lbUserSuggestions.DoubleClick += LbUserSuggestions_DoubleClick;
            tbNguoiNhan.TextChanged += TbNguoiNhan_TextChanged;
            lbDSTroChuyen.SelectedIndexChanged += LbDSTroChuyen_SelectedIndexChanged;
            bGui.Click += bGui_Click;

            ConnectSignalR();
        }

        // ==== MODEL CLASSES ====
        public class PartnerInfo
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public override string ToString() => $"{Username} - {Email}";
        }
        public class ConversationInfo
        {
            public int PartnerId { get; set; }
            public string PartnerUsername { get; set; }
            public string PartnerEmail { get; set; }
            public string LastMessage { get; set; }
            public DateTime LastMessageTime { get; set; }
            public override string ToString() => $"{PartnerUsername} - {LastMessage}";
        }
        public class MessageEntry
        {
            public string SenderUsername { get; set; }
            public string Content { get; set; }
            public DateTime Timestamp { get; set; }
            public bool IsMyMessage { get; set; }
        }
        public class UserSuggestion
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public override string ToString() => $"{Username} - {Email}";
        }

        // ==== SIGNALR CONNECTION & HANDLERS ====
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

            // Nhận danh sách đối tượng gợi ý khi tìm user
            connection.On<List<UserSuggestion>>("ReceiveUserSearchResults", (users) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    lbUserSuggestions.Items.Clear();
                    foreach (var user in users)
                    {
                        if (!user.Username.Equals(this.userName, StringComparison.OrdinalIgnoreCase))
                            lbUserSuggestions.Items.Add(user);
                    }
                    lbUserSuggestions.Visible = lbUserSuggestions.Items.Count > 0;
                    if (lbUserSuggestions.Visible)
                        lbUserSuggestions.BringToFront();
                }));
            });

            // Nhận danh sách cuộc trò chuyện
            connection.On<List<ConversationInfo>>("ReceiveConversationList", (conversations) =>
            {
                if (!lbDSTroChuyen.IsHandleCreated || lbDSTroChuyen.IsDisposed) return;
                lbDSTroChuyen.Invoke((MethodInvoker)(() =>
                {
                    // Lưu lại partner đang chọn
                    var selectedPartner = lbDSTroChuyen.SelectedItem as PartnerInfo;
                    int? selectedId = selectedPartner?.Id;

                    lbDSTroChuyen.Items.Clear();
                    foreach (var convo in conversations.OrderByDescending(c => c.LastMessageTime))
                    {
                        lbDSTroChuyen.Items.Add(new PartnerInfo { Id = convo.PartnerId, Username = convo.PartnerUsername, Email = convo.PartnerEmail });
                    }

                    // Sau khi reload, chọn lại partner cũ nếu còn tồn tại, đồng bộ lại activeChatPartner
                    if (selectedId.HasValue)
                    {
                        foreach (var item in lbDSTroChuyen.Items)
                        {
                            if (item is PartnerInfo p && p.Id == selectedId.Value)
                            {
                                lbDSTroChuyen.SelectedItem = item;
                                activeChatPartner = p;
                                break;
                            }
                        }
                    }
                    else
                    {
                        // Nếu không còn partner cũ, có thể chọn item đầu tiên hoặc giữ nguyên trạng thái
                        lbDSTroChuyen.ClearSelected();
                        activeChatPartner = null;
                    }
                }));
            });

            // Nhận lịch sử chat với 1 user
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

                    foreach (var msg in messages)
                    {
                        msg.IsMyMessage = msg.SenderUsername == userName;
                    }
                    chatHistories[partnerId] = messages.OrderBy(m => m.Timestamp).ToList();

                    ShowChatHistory(partnerId);
                }));
            });

            // Nhận tin nhắn mới
            connection.On<string, string, string, DateTime>("ReceiveMessage", (messageSender, messageContent, originalIntendedRecipient, timestamp) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    PartnerInfo chatPartnerObj = null;
                    bool isMyMessageForDisplay = messageSender == userName;
                    string partnerUsername = isMyMessageForDisplay ? originalIntendedRecipient : messageSender;

                    chatPartnerObj = lbDSTroChuyen.Items
                        .OfType<PartnerInfo>()
                        .FirstOrDefault(p => p.Username == partnerUsername);

                    if (chatPartnerObj == null)
                    {
                        // Thêm bạn mới vào danh sách nếu chưa có
                        chatPartnerObj = new PartnerInfo { Username = partnerUsername, Id = -1, Email = "" };
                        lbDSTroChuyen.Items.Insert(0, chatPartnerObj);
                    }

                    int partnerId = chatPartnerObj.Id;
                    if (!chatHistories.ContainsKey(partnerId))
                        chatHistories[partnerId] = new List<MessageEntry>();

                    // KIỂM TRA TRÙNG LẶP
                    var msgList = chatHistories[partnerId];
                    if (msgList.Any(m =>
                        m.Content == messageContent &&
                        m.SenderUsername == messageSender &&
                        Math.Abs((m.Timestamp - timestamp.ToLocalTime()).TotalSeconds) < 2
                    ))
                    {
                        // Đã có message này, không thêm nữa!
                        return;
                    }

                    msgList.Add(new MessageEntry
                    {
                        SenderUsername = messageSender,
                        Content = messageContent,
                        Timestamp = timestamp.ToLocalTime(),
                        IsMyMessage = isMyMessageForDisplay
                    });
                    chatHistories[partnerId] = msgList.OrderBy(m => m.Timestamp).ToList();

                    // Đưa partner lên đầu danh sách
                    if (lbDSTroChuyen.Items.Contains(chatPartnerObj))
                    {
                        if (lbDSTroChuyen.Items.IndexOf(chatPartnerObj) > 0)
                        {
                            lbDSTroChuyen.Items.Remove(chatPartnerObj);
                            lbDSTroChuyen.Items.Insert(0, chatPartnerObj);
                        }
                    }
                    else
                    {
                        lbDSTroChuyen.Items.Insert(0, chatPartnerObj);
                    }

                    // Nếu đang chat với partner này thì hiển thị ngay
                    if (activeChatPartner != null && activeChatPartner.Username == chatPartnerObj.Username)
                    {
                        AppendMessageToChatBox(messageSender, messageContent, timestamp, isMyMessageForDisplay);
                    }
                }));
            });

            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync("RegisterUser", userName);
                await connection.InvokeAsync("LoadConversations");
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi kết nối SignalR: " + ex.Message);
            }
        }

        // ==== UI EVENTS ====

        // Tìm user realtime qua SignalR
        private async void TbNguoiNhan_TextChanged(object sender, EventArgs e)
        {
            string searchText = tbNguoiNhan.Text.Trim();
            if (!string.IsNullOrWhiteSpace(searchText) && connection != null && connection.State == HubConnectionState.Connected)
            {
                await connection.InvokeAsync("SearchUsers", searchText);
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

        // Chọn user từ kết quả tìm kiếm
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

                // Tải lịch sử khi chọn user từ gợi ý
                _ = LoadChatHistory(selectedUser.Id, selectedUser.Username, selectedUser.Email);
            }
        }

        // Chọn partner từ danh sách trò chuyện
        private void LbDSTroChuyen_SelectedIndexChanged(object sender, EventArgs e)
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
            tbNguoiNhan.Text = $"{activeChatPartner.Username} - {activeChatPartner.Email}";
            bGui.Enabled = true;
            _selectedRecipientId = partnerInfo.Id;
            _selectedRecipientUsername = partnerInfo.Username;

            if (chatHistories.ContainsKey(activeChatPartner.Id) && chatHistories[activeChatPartner.Id]?.Count > 0)
            {
                ShowChatHistory(activeChatPartner.Id);
            }
            else
            {
                rtbKhungTroChuyen.Clear();
                rtbKhungTroChuyen.AppendText($"Đang tải lịch sử với {activeChatPartner.Username}...\n");
                _ = LoadChatHistory(activeChatPartner.Id, activeChatPartner.Username, activeChatPartner.Email);
            }
        }

        private bool isSending = false;

        // Gửi tin nhắn
        private async void bGui_Click(object sender, EventArgs e)
        {
            if (isSending) return; // chống double click
            isSending = true;
            try
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
            finally
            {
                isSending = false;
            }
        }

        // ==== SIGNALR HELPERS ====

        private async Task LoadChatHistory(int partnerId, string partnerUsername, string partnerEmail)
        {
            if (connection == null || connection.State != HubConnectionState.Connected)
            {
                rtbKhungTroChuyen.AppendText("Lỗi: Chưa kết nối tới server để tải lịch sử.\n");
                return;
            }
            try
            {
                await connection.InvokeAsync("LoadChatHistory", partnerId);
                // Nếu chưa có trong danh sách, thêm vào
                if (!lbDSTroChuyen.Items.OfType<PartnerInfo>().Any(p => p.Id == partnerId))
                {
                    var partner = new PartnerInfo { Id = partnerId, Username = partnerUsername, Email = partnerEmail };
                    lbDSTroChuyen.Items.Insert(0, partner);
                    lbDSTroChuyen.SelectedItem = partner;
                }
            }
            catch (Exception ex)
            {
                rtbKhungTroChuyen.AppendText($"Lỗi khi yêu cầu lịch sử chat: {ex.Message}\n");
            }
        }

        private void ShowChatHistory(int partnerId)
        {
            rtbKhungTroChuyen.Clear();
            if (chatHistories.TryGetValue(partnerId, out var messages) && messages != null)
            {
                foreach (var msg in messages)
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
            }
        }

        private void AppendMessageToChatBox(string sender, string content, DateTime timestamp, bool isMyMessage)
        {
            string displayUser = isMyMessage ? "Bạn" : sender;
            rtbKhungTroChuyen.SelectionStart = rtbKhungTroChuyen.TextLength;
            rtbKhungTroChuyen.SelectionLength = 0;
            rtbKhungTroChuyen.SelectionAlignment = isMyMessage ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            rtbKhungTroChuyen.SelectionColor = isMyMessage ? Color.Blue : Color.DarkGreen;
            rtbKhungTroChuyen.AppendText($"[{displayUser} - {timestamp:HH:mm}]\n");
            rtbKhungTroChuyen.AppendText($"{content}\n\n");
            rtbKhungTroChuyen.SelectionColor = rtbKhungTroChuyen.ForeColor;
            rtbKhungTroChuyen.ScrollToCaret();
        }

        public async Task RefreshConversationListAsync()
        {
            if (connection != null && connection.State == Microsoft.AspNetCore.SignalR.Client.HubConnectionState.Connected)
            {
                await connection.InvokeAsync("LoadConversations");
            }
        }

        // ==== CLEANUP ====
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null) components.Dispose();
                if (connection != null)
                {
                    Task.Run(async () => await connection.StopAsync()).Wait();
                    Task.Run(async () => await connection.DisposeAsync()).Wait();
                    connection = null;
                }
            }
            base.Dispose(disposing);
        }
    }
}