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
        private System.Windows.Forms.Timer searchDebounceTimer;
        public NhanTinControl(string userId, Form loginForm, string userName)
        {
            InitializeComponent();
            this.userId = userId;
            this.loginForm = loginForm;
            this.userName = userName;

            var suggestionsListBox = this.Controls.Find("lbUserSuggestions", true).FirstOrDefault() as ListBox;
            if (suggestionsListBox == null) // Nếu không tìm thấy trong Designer, tạo mới
            {
                suggestionsListBox = new ListBox
                {
                    Name = "lbUserSuggestions",
                    Visible = false,
                    Location = new Point(tbNguoiNhan.Location.X, tbNguoiNhan.Location.Y + tbNguoiNhan.Height),
                    Width = tbNguoiNhan.Width,
                    Height = 80, // Chiều cao nhỏ hơn
                    IntegralHeight = false
                };
                this.Controls.Add(suggestionsListBox);
                suggestionsListBox.BringToFront();
            }
            suggestionsListBox.DoubleClick += LbUserSuggestions_DoubleClick;


            tbNguoiNhan.TextChanged += TbNguoiNhan_TextChanged;
            // Không cần LostFocus/GotFocus phức tạp cho phiên bản đơn giản

            searchDebounceTimer = new System.Windows.Forms.Timer { Interval = 500 }; // 500ms delay
            searchDebounceTimer.Tick += SearchDebounceTimer_Tick;

            lbDSTroChuyen.SelectedIndexChanged += LstConversations_SelectedIndexChanged;
            ConnectSignalR();
        }
        private void TbNguoiNhan_TextChanged(object sender, EventArgs e)
        {
            searchDebounceTimer.Stop();
            if (!string.IsNullOrWhiteSpace(tbNguoiNhan.Text))
            {
                searchDebounceTimer.Start();
            }
            else
            {
                var suggestionsBox = this.Controls.Find("lbUserSuggestions", true).FirstOrDefault() as ListBox;
                if (suggestionsBox != null)
                {
                    suggestionsBox.Visible = false;
                    suggestionsBox.Items.Clear();
                }
            }
        }

        private async void SearchDebounceTimer_Tick(object sender, EventArgs e)
        {
            searchDebounceTimer.Stop();
            string searchText = tbNguoiNhan.Text.Trim();
            var suggestionsBox = this.Controls.Find("lbUserSuggestions", true).FirstOrDefault() as ListBox;
            if (suggestionsBox == null || string.IsNullOrWhiteSpace(searchText))
            {
                if (suggestionsBox != null) suggestionsBox.Visible = false;
                return;
            }

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetAsync($"https://localhost:5268/api/user/search?query={Uri.EscapeDataString(searchText)}");
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = await response.Content.ReadAsStringAsync();
                        var users = JsonSerializer.Deserialize<List<string>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        suggestionsBox.Items.Clear();
                        if (users != null && users.Any())
                        {
                            foreach (var user in users.Where(u => u != this.userName)) // Không hiển thị chính mình
                            {
                                suggestionsBox.Items.Add(user);
                            }
                        }
                        suggestionsBox.Visible = suggestionsBox.Items.Count > 0;
                    }
                    else { suggestionsBox.Visible = false; }
                }
                catch { suggestionsBox.Visible = false; } // Lỗi thì ẩn đi
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
                // Tùy chọn: tự động chọn trong lbDSTroChuyen hoặc gửi tin nhắn
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
                        rtbKhungTroChuyen.AppendText($"{messageSender} (đến {originalIntendedRecipient}): {messageContent} [{timestamp.ToLocalTime()}]\n");
                        return;
                    }

                    if (!chatHistories.ContainsKey(chatPartner))
                    {
                        chatHistories[chatPartner] = new List<MessageEntry>();
                    }
                    // Sử dụng timestamp từ server
                    chatHistories[chatPartner].Add(new MessageEntry { SenderUsername = messageSender, Content = messageContent, Timestamp = timestamp.ToLocalTime() });
                    // Sắp xếp lại nếu cần
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
                        string displayUser = isMyMessage ? "Bạn" : messageSender;
                        rtbKhungTroChuyen.AppendText($"{displayUser}: {messageContent} \n"); 
                        rtbKhungTroChuyen.ScrollToCaret();
                    }
                    else
                    {
                        // TODO: Chỉ báo tin nhắn mới cho cuộc trò chuyện không active (ví dụ: làm đậm tên, thêm dấu *)
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

                    rtbKhungTroChuyen.Clear();
                    foreach (var msg in chatHistories[partnerName])
                    {
                        string displayUser = (msg.SenderUsername == userName) ? "Bạn" : msg.SenderUsername; 
                        rtbKhungTroChuyen.AppendText($"{displayUser}: {msg.Content} ");
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
                tbNguoiNhan.ReadOnly = false; // Cho phép nhập người nhận mới
                rtbKhungTroChuyen.Clear();
                bGui.Enabled = false; // Vô hiệu hóa nút gửi nếu không có người nhận
                return;
            }

            string newActivePartner = lbDSTroChuyen.SelectedItem.ToString();

            // Xóa dấu (*) chỉ báo tin nhắn mới nếu có
            // if (newActivePartner.EndsWith(" (*)"))
            // {
            //    newActivePartner = newActivePartner.Replace(" (*)", "");
            //    lbDSTroChuyen.Items[lbDSTroChuyen.SelectedIndex] = newActivePartner; // Cập nhật lại item trong listbox
            // }

            activeChatPartner = newActivePartner;
            tbNguoiNhan.Text = activeChatPartner;
            tbNguoiNhan.ReadOnly = true; // Không cho sửa người nhận khi đã chọn từ danh sách
            bGui.Enabled = true; // Cho phép gửi

            rtbKhungTroChuyen.Clear();
            // Không hiển thị từ cache client ngay, luôn yêu cầu server để đảm bảo dữ liệu mới nhất
            // Hoặc có thể hiển thị từ cache trước, sau đó cập nhật từ server

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
    }
}