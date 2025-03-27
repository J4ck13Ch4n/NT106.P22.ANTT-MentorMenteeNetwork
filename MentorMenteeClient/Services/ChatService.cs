using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using static System.Net.WebRequestMethods;

namespace ChatClient.Services
{
    public class ChatService
    {
        private HubConnection _hubConnection;
        private readonly HttpClient _httpClient;
        private readonly string _serverUrl;

        public event Action<string, string> OnMessageReceived;
        public event Action<int> OnFriendRequestReceived;
        public event Action<List<int>> OnFriendListUpdated;

        public ChatService(string serverUrl)
        {
            _serverUrl = "http://localhost:5268";
            _httpClient = new HttpClient { BaseAddress = new Uri(serverUrl) };
        }

        public async Task ConnectAsync()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl($"{_serverUrl}/chatHub") // Địa chỉ server SignalR
                .WithAutomaticReconnect()
                .Build();

            _hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                OnMessageReceived?.Invoke(user, message);
            });

            _hubConnection.On<int>("ReceiveFriendRequest", senderId =>
            {
                OnFriendRequestReceived?.Invoke(senderId);
            });

            _hubConnection.On<List<int>>("UpdateFriendList", friendList =>
            {
                OnFriendListUpdated?.Invoke(friendList);
            });

            try
            {
                await _hubConnection.StartAsync();
                Console.WriteLine("Connected to SignalR server.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to SignalR: {ex.Message}");
            }
        }

        public async Task SendMessageAsync(string user, string message)
        {
            if (_hubConnection != null && _hubConnection.State == HubConnectionState.Connected)
            {
                await _hubConnection.InvokeAsync("SendMessage", user, message);
            }
        }

        // Gửi lời mời kết bạn
        public async Task<bool> SendFriendRequestAsync(int senderId, int receiverId)
        {
            var response = await _httpClient.PostAsJsonAsync("api/friendship/send-request", new
            {
                SenderId = senderId,
                ReceiverId = receiverId
            });

            return response.IsSuccessStatusCode;
        }

        // Chấp nhận lời mời kết bạn
        public async Task<bool> AcceptFriendRequestAsync(int senderId, int receiverId)
        {
            var response = await _httpClient.PostAsJsonAsync("api/friendship/accept-request", new
            {
                SenderId = senderId,
                ReceiverId = receiverId
            });

            return response.IsSuccessStatusCode;
        }

        // Từ chối lời mời kết bạn
        public async Task<bool> RejectFriendRequestAsync(int senderId, int receiverId)
        {
            var response = await _httpClient.PostAsJsonAsync("api/friendship/reject-request", new
            {
                SenderId = senderId,
                ReceiverId = receiverId
            });

            return response.IsSuccessStatusCode;
        }

        // Hủy kết bạn
        public async Task<bool> RemoveFriendAsync(int senderId, int receiverId)
        {
            var response = await _httpClient.PostAsJsonAsync("api/friendship/remove-friend", new
            {
                SenderId = senderId,
                ReceiverId = receiverId
            });

            return response.IsSuccessStatusCode;
        }
        public async Task<bool> SendFriendRequestByNameAsync(string senderUserName, string receiverUserName)
        {
            var response = await _httpClient.PostAsJsonAsync("api/friendship/send-friend-request-by-name", new
            {
                SenderUserName = senderUserName,
                ReceiverUserName = receiverUserName
            });

            return response.IsSuccessStatusCode;
        }
    }
}