using Auth0.OidcClient;

namespace SimOpsUI
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            if (Program.Config.Authentication.Method.ToLower().Equals("auth0"))
            {
                var client = new Auth0Client(new Auth0ClientOptions
                {
                    Domain = Program.Config.Authentication.Auth0.Domain,
                    ClientId = Program.Config.Authentication.Auth0.ClientId,
                    Browser = new WebViewBrowserChromium()
                });
                var loginResult = await client.LoginAsync(new { audience = Program.Config.SimOpsServer });
                if (loginResult.IsError)
                {
                    Program.ShowError("Failed to login! Unknown username/password");
                    Application.Exit();
                }
                else
                {
                    Program.Username = loginResult.User.Identity.Name;
                    Program.AccessToken = loginResult.AccessToken;
                    Program.RefreshToken = loginResult.RefreshToken;
                    Program.TokenExpiry = loginResult.AccessTokenExpiration.UtcDateTime;
                }
            }
            tsslUser.Text = Program.Username ?? "Not Logged In";
        }
    }
}