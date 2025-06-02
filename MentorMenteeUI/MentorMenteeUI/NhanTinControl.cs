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

                using (var httpClient = new HttpClient())
                {
                    try
                    {
                        var response = await httpClient.GetAsync($"https://localhost:5268/api/user/search?query={Uri.EscapeDataString(searchText)}");
                        var jsonResponsed = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"API Response: {jsonResponsed}");
                        if (response.IsSuccessStatusCode)
                        {
                            var jsonResponse = await response.Content.ReadAsStringAsync();
                            var usersFound = JsonSerializer.Deserialize<List<string>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                            if (usersFound != null && usersFound.Any(user => user != this.userName))
                            {
                                foreach (var user in usersFound)
                                {
                                    if (user != this.userName)
                                    {
                                        this.lbUserSuggestions.Items.Add(user);
                                    }
                                }

                                this.lbUserSuggestions.Visible = this.lbUserSuggestions.Items.Count > 0;
                                if (this.lbUserSuggestions.Visible)
                                {
                                    this.lbUserSuggestions.BringToFront();
                                }
                            }
                            else
                            {
                                this.lbUserSuggestions.Visible = false;
                            }
                        }
                        else
                        {
                            this.lbUserSuggestions.Visible = false;
                            this.lbUserSuggestions.Items.Clear();
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
            }
        }

        private void LbUserSuggestions_DoubleClick(object sender, EventArgs e)
        {
            var suggestionsBox = sender as ListBox;
            if (suggestionsBox != null && suggestionsBox.SelectedItem != null)
            {
                tbNguoiNhan.Text = suggestionsBox.SelectedItem.ToString();
                suggestionsBox.Visible = false;
                tbNguoiNhan.Focus();
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
                    string chatPartner;
                    bool isMyMessageForDisplay = messageSender == userName;

                    if (isMyMessageForDisplay)
                    {
                        chatPartner = originalIntendedRecipient;
                    }
                    else
                    {
                        chatPartner = messageSender;
                    }

                    if (string.IsNullOrEmpty(chatPartner))
                    {
                        rtbKhungTroChuyen.SelectionAlignment = HorizontalAlignment.Left;
                        rtbKhungTroChuyen.SelectionColor = Color.Gray;
                        rtbKhungTroChuyen.AppendText($"{messageSender}: {messageContent} [{timestamp.ToLocalTime():HH:mm}]\n");
                        rtbKhungTroChuyen.SelectionColor = rtbKhungTroChuyen.ForeColor;
                        return;
                    }

                    if (!chatHistories.ContainsKey(chatPartner))
                    {
                        chatHistories[chatPartner] = new List<MessageEntry>();
                    }
                    chatHistories[chatPartner].Add(new MessageEntry { SenderUsername = messageSender, Content = messageContent, Timestamp = timestamp.ToLocalTime(), IsMyMessage = isMyMessageForDisplay });
                    chatHistories[chatPartner] = chatHistories[chatPartner].OrderBy(m => m.Timestamp).ToList();


                    object previouslySelectedItem = lbDSTroChuyen.SelectedItem;
                    bool wasHandlerAttached = true;
                    try
                    {
                        lbDSTroChuyen.SelectedIndexChanged -= LstConversations_SelectedIndexChanged;
                    }
                    catch { wasHandlerAttached = false; }


                    if (!lbDSTroChuyen.Items.Contains(chatPartner))
                    {
                        lbDSTroChuyen.Items.Insert(0, chatPartner);
                    }
                    else if (lbDSTroChuyen.Items.IndexOf(chatPartner) > 0)
                    {
                        lbDSTroChuyen.Items.Remove(chatPartner);
                        lbDSTroChuyen.Items.Insert(0, chatPartner);
                    }

                    if (activeChatPartner == chatPartner && lbDSTroChuyen.Items.Contains(chatPartner)) { lbDSTroChuyen.SelectedItem = chatPartner; }
                    else if (previouslySelectedItem != null && lbDSTroChuyen.Items.Contains(previouslySelectedItem)) { lbDSTroChuyen.SelectedItem = previouslySelectedItem; }
                    else if (lbDSTroChuyen.Items.Contains(chatPartner)) { lbDSTroChuyen.SelectedItem = chatPartner; }


                    if (wasHandlerAttached) lbDSTroChuyen.SelectedIndexChanged += LstConversations_SelectedIndexChanged;


                    if (activeChatPartner == chatPartner)
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
                    else
                    {
                        int itemIndex = lbDSTroChuyen.Items.IndexOf(chatPartner);
                        if (itemIndex != -1)
                        {
                            lbDSTroChuyen.Items[itemIndex] = $"{chatPartner}";
                        }
                    }
                }));
            });

            // Handler để nhận danh sách các cuộc trò chuyện
            connection.On<List<string>>("ReceiveConversationPartners", (partners) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    lbDSTroChuyen.BeginUpdate();
                    object currentSelection = lbDSTroChuyen.SelectedItem;
                    lbDSTroChuyen.Items.Clear();
                    foreach (var partner in partners.OrderBy(p => p))
                    {
                        if (!string.IsNullOrEmpty(partner) && partner != this.userName)
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
            connection.On<string, List<MessageEntry>, string>("ReceiveChatHistory", (partnerName, messages, errorMessage) =>
            {
                Invoke((MethodInvoker)(() =>
                {
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        rtbKhungTroChuyen.AppendText($"Lỗi tải lịch sử với {partnerName}: {errorMessage}\n");
                        return;
                    }

                    if (activeChatPartner != partnerName) return; // Chỉ cập nhật nếu là cuộc trò chuyện đang active

                    // Ghi đè lịch sử từ server vào cache client
                    chatHistories[partnerName] = messages.OrderBy(m => m.Timestamp).ToList();

                    var processedMessages = messages.Select(m => new MessageEntry
                    {
                        SenderUsername = m.SenderUsername,
                        Content = m.Content,
                        Timestamp = m.Timestamp,
                        IsMyMessage = (m.SenderUsername == this.userName)
                    }).OrderBy(m => m.Timestamp).ToList();

                    chatHistories[partnerName] = processedMessages;

                    rtbKhungTroChuyen.Clear();
                    foreach (var msg in chatHistories[partnerName])
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

        private async void LstConversations_SelectedIndexChanged(object sender, EventArgs e) // Thêm async
        {
            if (lbDSTroChuyen.SelectedItem == null)
            {
                activeChatPartner = null;
                tbNguoiNhan.Text = "";
                tbNguoiNhan.ReadOnly = false;
                rtbKhungTroChuyen.Clear();
                bGui.Enabled = false;
                return;
            }

            string newActivePartner = lbDSTroChuyen.SelectedItem.ToString();


            activeChatPartner = newActivePartner;

            tbNguoiNhan.ReadOnly = true;

            tbNguoiNhan.Text = activeChatPartner;

            bGui.Enabled = true;

            rtbKhungTroChuyen.Clear();

            if (connection != null && connection.State == HubConnectionState.Connected)
            {
                rtbKhungTroChuyen.AppendText($"Đang tải lịch sử với {activeChatPartner}...\n");
                try
                {
                    await connection.InvokeAsync("LoadChatHistory", activeChatPartner);
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

    }



    public class MessageEntry
    {
        public string SenderUsername { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } 
        public bool IsMyMessage { get; set; }
    }
}