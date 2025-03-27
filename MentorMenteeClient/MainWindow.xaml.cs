using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ChatClient.Services;


namespace ChatClient
{
    public partial class MainWindow : Window
    {
        private ChatService _chatService;
        private string _userName;
        private int _userId; // Giả sử ID người dùng là số nguyên
        private List<int> _friendRequests = new List<int>();
        private List<int> _friends = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
            _chatService = new ChatService("http://localhost:5268");
        }

        private async void StartChat_Click(object sender, RoutedEventArgs e)
        {
            _userName = UserNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(_userName))
            {
                MessageBox.Show("Vui lòng nhập tên!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NamePanel.Visibility = Visibility.Collapsed;
            ChatPanel.Visibility = Visibility.Visible;

            _chatService.OnMessageReceived += (user, message) =>
            {
                Dispatcher.Invoke(() =>
                {
                    ChatListBox.Items.Add($"{user}: {message}");
                });
            };

            _chatService.OnFriendRequestReceived += senderId =>
            {
                Dispatcher.Invoke(() =>
                {
                    _friendRequests.Add(senderId);
                    FriendRequestsListBox.Items.Add($"Yêu cầu từ ID: {senderId}");
                });
            };

            _chatService.OnFriendListUpdated += friendList =>
            {
                Dispatcher.Invoke(() =>
                {
                    _friends = friendList;
                    FriendsListBox.Items.Clear();
                    foreach (var friendId in friendList)
                    {
                        FriendsListBox.Items.Add($"Bạn bè ID: {friendId}");
                    }
                });
            };

            await _chatService.ConnectAsync();
        }

        private async void SendMessage_Click(object sender, RoutedEventArgs e)
        {
            string message = MessageTextBox.Text;
            if (!string.IsNullOrEmpty(message))
            {
                await _chatService.SendMessageAsync(_userName, message);
                MessageTextBox.Clear();
            }
        }


        private async void AcceptFriendRequest_Click(object sender, RoutedEventArgs e)
        {
            if (FriendRequestsListBox.SelectedItem is string selectedRequest)
            {
                int senderId = int.Parse(selectedRequest.Split(' ')[3]);
                await _chatService.AcceptFriendRequestAsync(senderId, _userId);
                _friendRequests.Remove(senderId);
                FriendRequestsListBox.Items.Remove(selectedRequest);
                MessageBox.Show("Đã chấp nhận kết bạn.", "Thông báo");
            }
        }
        private void MessageTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            MessagePlaceholder.Visibility = string.IsNullOrWhiteSpace(MessageTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }

        private void UserNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UserNamePlaceholder.Visibility = string.IsNullOrWhiteSpace(UserNameTextBox.Text) ? Visibility.Visible : Visibility.Collapsed;
        }
        private async void SendFriendRequestByName_Click(object sender, RoutedEventArgs e)
        {
            string friendUserName = FriendNameTextBox.Text.Trim();
            if (string.IsNullOrEmpty(friendUserName))
            {
                MessageBox.Show("Vui lòng nhập tên người dùng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool success = await _chatService.SendFriendRequestByNameAsync(_userName, friendUserName);
            if (success)
            {
                MessageBox.Show("Lời mời kết bạn đã được gửi.", "Thông báo");
            }
            else
            {
                MessageBox.Show("Gửi lời mời kết bạn thất bại.", "Lỗi");
            }
        }

    }
}