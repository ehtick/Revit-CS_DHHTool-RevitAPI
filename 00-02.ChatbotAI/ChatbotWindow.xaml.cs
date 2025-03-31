using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _00_02.ChatbotAI
{
    /// <summary>
    /// Interaction logic for ChatbotWindow.xaml
    /// </summary>
    public partial class ChatbotWindow : Window
    {
        private ExternalCommandData _commandData = null;  // Lưu dữ liệu Revit
        public ChatbotWindow(UIApplication application)
        {
            InitializeComponent();
        }

        private async void SendPrompt(object sender, RoutedEventArgs e)
        {
            string userMessage = UserInput.Text;
            ChatOutput.AppendText("\nUser: " + userMessage);

            string response = await OpenAIHelper.GetRevitCommand(userMessage);
            ChatOutput.AppendText("\nAI: " + response);

            // Xử lý lệnh vẽ nếu AI trả về JSON
            ProcessCommand(response);
        }

        private void ProcessCommand(string response)
        {
            try
            {
                var command = Newtonsoft.Json.Linq.JObject.Parse(response);

                if (command["action"]?.ToString() == "draw_wall")
                {
                    double width = (double)command["parameters"]["width"];
                    double length = (double)command["parameters"]["length"];

                    // Gọi API vẽ tường trong Revit
                    DrawCommands.DrawWall(_commandData, width, length);
                }
            }
            catch (Exception ex)
            {
                ChatOutput.AppendText("\n[Error] " + ex.Message);
            }
        }
    }


}
