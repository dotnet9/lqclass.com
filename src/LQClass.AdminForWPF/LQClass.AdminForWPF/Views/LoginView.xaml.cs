using LQClass.AdminForWPF.Infrastructure.Configs;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Windows;

namespace LQClass.AdminForWPF.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var configFile = $"{AppDomain.CurrentDomain.BaseDirectory}config.json";
                var configContent = File.ReadAllText(configFile);
                AppConfig appConfig = JsonConvert.DeserializeObject<AppConfig>(configContent);
                string apiAddress = string.Empty;
#if DEBUG
                apiAddress = appConfig.Development.API;
#else
                apiAddress = appConfig.Production.API;
#endif
                var response = Login(apiAddress, tbName.Text, tbPwd.Password);
                if (response.IsSuccessful)
                {
                    MessageBox.Show($"登录成功：{response.Content}");
                }
                else
                {
                    MessageBox.Show($"登录失败：{response.Content}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private IRestResponse Login(string apiAddress, string userName, string pwd)
        {
            var client = new RestClient($"{apiAddress}_login/Login");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("userid", userName);
            request.AddParameter("password", pwd);
            request.AddParameter("cookie", "false");
            IRestResponse response = client.Execute(request);

            return response;
        }
    }
}
