using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _00_02.ChatbotAI
{
    class OpenAIHelper
    {
        private static readonly string apiKey = "YOUR_OPENAI_API_KEY";
        private static readonly string apiUrl = "https://api.openai.com/v1/chat/completions";

        public static async Task<string> GetRevitCommand(string prompt)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                    new { role = "system", content = "Bạn là trợ lý AI hỗ trợ vẽ trong Revit." },
                    new { role = "user", content = prompt }
                }
                };

                string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                string responseString = await response.Content.ReadAsStringAsync();

                JObject responseObject = JObject.Parse(responseString);
                return responseObject["choices"]?[0]?["message"]?["content"]?.ToString();
            }
        }
    }
}
