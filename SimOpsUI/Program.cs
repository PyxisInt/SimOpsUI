using Auth0.OidcClient;
using Newtonsoft.Json;
using PrabalGhosh.Utilities;
using SimOps.Sdk;

namespace SimOpsUI
{
    public static class Program
    {
        public static SimOpsConfig Config { get; set; }
        public static string LogoFile { get; set; }

        public static Sdk Sdk { get; set; }

        //Login Results
        public static string Username { get; set; }
        public static string AccessToken { get; set; }
        public static string RefreshToken { get; set; }
        public static DateTime TokenExpiry { get; set; }




        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            //Look for configuration file...should be in %DOCUMENTS%/SimOps folder. If not found,
            //Expected to be customized by VA owner
            try
            {
                FindAndLoadConfiguration();
                if (Config == null)
                {
                    MessageBox.Show($"Unable to read configuration. Please get a new one from your VA!",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (Config.SimOpsServer.IsEmpty())
                {
                    ShowError("Configuration does not specify SimOps Server URL! Unable to proceed...");
                    return;
                }
                Sdk = new Sdk(Config.SimOpsServer);
                if (Config.Authentication.Method.ToLower().Equals("internal"))
                {
                    var frmLogin = new LoginForm();
                    DialogResult dr = frmLogin.ShowDialog();
                    if (dr != DialogResult.OK)
                    {
                        ShowError("Login Failed! Unknown username/password");
                        return;
                    }
                }
                //handle login via MainForm
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void ShowMessage(string msg, string title = "INFORMATION")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void ShowError(string msg)
        {
            MessageBox.Show(msg, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }

        private static void FindAndLoadConfiguration()
        {
            try
            {
                var expectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "simops");
                if (Directory.Exists(expectedPath))
                {
                    var files = Directory.GetFiles(expectedPath);
                    foreach(var file in files)
                    {
                        if (file.ToLower().Contains("simops.json"))
                        {
                            var configFile = Path.Combine(expectedPath, "simops.json");
                            using (var sr = new StreamReader(configFile))
                            {
                                var serializer = JsonSerializer.CreateDefault();
                                var reader = new JsonTextReader(sr);
#pragma warning disable CS8601 // Possible null reference assignment.
                                Config = serializer.Deserialize<SimOpsConfig>(reader);
#pragma warning restore CS8601 // Possible null reference assignment.
                            }
                        }
                        if (file.ToLower().Contains("simopslogo.jpg"))
                        {
                            Program.LogoFile = file;
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }        
        }
    }
}