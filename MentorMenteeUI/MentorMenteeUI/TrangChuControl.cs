namespace MentorMenteeUI
{
    public partial class TrangChuControl : UserControl
    {
        private readonly HttpClient httpClient = new HttpClient();
        private readonly string apiBase = "https://localhost:5268/api";
        public TrangChuControl()
        {
            InitializeComponent();
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }
    }
}
