using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.WebSockets;
using System.Security.Cryptography;
using MentorMentee.Cryptography.Helpers;

namespace MentorMenteeUI
{
    public partial class NhanTinControl : UserControl
    {
        private ClientWebSocket ws = new ClientWebSocket();

        public NhanTinControl()
        {
            InitializeComponent();
            ConnectWebSocket();
        }

        private RSAParameters GetRecipientPublicKey()
        {
            using (RSA rsa = RSA.Create())
            {
                return rsa.ExportParameters(false); 
            }
        }

        private async void ConnectWebSocket()
        {
            ws = new ClientWebSocket();
            try
            {
                await ws.ConnectAsync(new Uri("wss://localhost:5268/wss"), CancellationToken.None);
                MessageBox.Show("Kết nối WebSocket thành công!");
                _ = Task.Run(ReceiveMessages);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối WebSocket: " + ex.Message);
            }
        }

        private async Task ReceiveMessages()
        {
            var buffer = new byte[1024];
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    Invoke((MethodInvoker)(() =>
                    {
                        rtbKhungTroChuyen.AppendText("Server: " + message);
                    }));
                }
            }
        }

        private async void bGui_Click(object sender, EventArgs e)
        {
            if (ws.State == WebSocketState.Open)
            {
                var buffer = Encoding.UTF8.GetBytes(tbTinNhan.Text);
                await ws.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

                rtbKhungTroChuyen.AppendText("You: " + tbTinNhan.Text + "\n");
                tbTinNhan.Clear();
            }
            else
            {
                MessageBox.Show("WebSocket chưa kết nối!");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (ws != null && ws.State == WebSocketState.Open)
                {
                    ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "Đóng kết nối", CancellationToken.None).Wait();
                }
            }
            base.Dispose(disposing);
        }

        private void rtbKhungTroChuyen_TextChanged(object sender, EventArgs e)
        {

        }
    }
}



