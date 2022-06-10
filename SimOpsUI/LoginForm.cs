using PrabalGhosh.Utilities;
using SimOps.Models.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimOpsUI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            lblName.Text = Program.Config.VAName;
            pbLogo.ImageLocation = Program.LogoFile;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("You are canceling out of SimOps! Are you sure?",
                "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            if (tbUsername.Text.IsEmpty())
            {
                Program.ShowError("A user name must be specified!");
                tbUsername.Focus();
            }
            else
            {
                //username has been entered. Try to login...
                var request = new LoginRequest { 
                    Username = tbUsername.Text,
                    Password = tbPassword.Text,
                };
                try
                {
                    var loginResult = await Program.Sdk.LoginService.LoginAsync(request);
                    if (loginResult != null)
                    {
                        Program.Username = loginResult.Username;
                        Program.AccessToken = loginResult.AccessToken;
                        Program.RefreshToken = loginResult.RefreshToken;
                        Program.TokenExpiry = loginResult.ExpiresAt;
                        this.DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        Program.ShowError("Login failed! Invalid username/password");
                    }
                }
                catch
                {
                    Program.ShowError("Login failed! Invalid username/password");
                    tbUsername.Focus();
                }
            }
        }
    }
}
