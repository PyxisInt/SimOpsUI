using Auth0.OidcClient;

namespace SimOpsUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var client = new Auth0Client(new Auth0ClientOptions { 
                Domain = "dev-rvdj99qb.us.auth0.com",
                ClientId = "nN6A30JHTcNWpviapTpj5DIsSbQDnYYb",
                Browser = new WebViewBrowserChromium()
            });
            var loginResult = await client.LoginAsync(new { audience = "https://simopsservice.herokuapp.com/" });
            if (loginResult.IsError)
            {
                MessageBox.Show("Failed to login with Auth0!");
            }
        }
    }
}