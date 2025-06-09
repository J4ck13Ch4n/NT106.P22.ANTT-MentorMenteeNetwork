using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MentorMenteeUI.Services;

namespace MentorMenteeUI
{
    public partial class KetBanControl : UserControl
    {
        private readonly UserService _userService = new UserService();
        private readonly FriendService _friendService = new FriendService();
        private List<UserDto> _searchResults = new List<UserDto>();
        private List<Services.PendingRequestDto> _pendingRequests = new List<Services.PendingRequestDto>();
        private int _currentUserId;

        public event EventHandler FriendListChanged;

        public KetBanControl(int currentUserId)
        {
            InitializeComponent();
            _currentUserId = currentUserId;
        }

        protected override async void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            await LoadPendingRequests();
        }

        private async Task LoadPendingRequests()
        {
            _pendingRequests = await _friendService.GetPendingRequests(_currentUserId);
            dgvLoiMoiKetBan.AutoGenerateColumns = true;
            dgvLoiMoiKetBan.DataSource = null;
            dgvLoiMoiKetBan.DataSource = _pendingRequests;
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(keyword)) return;
            _searchResults = await _userService.SearchUsersAsync(keyword);
            HienThiKetQua();
        }

        private async void btnKetBan_Click(object sender, EventArgs e)
        {
            if (dgvKetQua.SelectedRows.Count == 0) return;
            var user = (UserDto)dgvKetQua.SelectedRows[0].DataBoundItem;
            var status = await _friendService.GetRelationshipStatus(_currentUserId, user.Id);
            if (status == "none")
            {
                await _friendService.SendFriendRequest(_currentUserId, user.Id);
                MessageBox.Show("Đã gửi lời mời kết bạn!");
                await LoadPendingRequests();
            }
            else if (status == "pending")
            {
                MessageBox.Show("Đã gửi lời mời, chờ xác nhận!");
            }
            else if (status == "accepted")
            {
                MessageBox.Show("Hai bạn đã là bạn bè!");
            }
            HienThiKetQua();
        }

        private void HienThiKetQua()
        {
            dgvKetQua.DataSource = null;
            dgvKetQua.DataSource = _searchResults;
        }

        private async void btnChapNhan_Click(object sender, EventArgs e)
        {
            if (dgvLoiMoiKetBan.SelectedRows.Count == 0) return;
            var request = (Services.PendingRequestDto)dgvLoiMoiKetBan.SelectedRows[0].DataBoundItem;
            bool ok = await _friendService.AcceptFriendRequest(request.SenderId, _currentUserId);
            if (ok)
            {
                MessageBox.Show("Đã chấp nhận lời mời kết bạn!");
                FriendListChanged?.Invoke(this, EventArgs.Empty); // Trigger event
            }
            else
                MessageBox.Show("Có lỗi khi chấp nhận!");
            await LoadPendingRequests();
        }

        private async void btnTuChoi_Click(object sender, EventArgs e)
        {
            if (dgvLoiMoiKetBan.SelectedRows.Count == 0) return;
            var request = (Services.PendingRequestDto)dgvLoiMoiKetBan.SelectedRows[0].DataBoundItem;
            bool ok = await _friendService.RejectFriendRequest(request.SenderId, _currentUserId);
            if (ok)
                MessageBox.Show("Đã từ chối lời mời kết bạn!");
            else
                MessageBox.Show("Có lỗi khi từ chối!");
            await LoadPendingRequests();
        }
    }
}
