namespace Loupedeck.IftttPlugin.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Loupedeck.IftttPlugin.Models;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    internal class Functions
    {
        private static readonly IftttPlugin _plugin;
        public static event EventHandler<IftttEventTrigger> HttpResponse;
        public static Task ReadEventsFile()
        {
            var helperFunction = new IftttPlugin();
            var pluginDataDirectory = helperFunction.GetPluginDataDirectory();
            var json = File.ReadAllText(System.IO.Path.Combine(pluginDataDirectory, System.IO.Path.Combine(pluginDataDirectory, "events.json")));
            var rss = JObject.Parse(json);
            if (rss.ContainsKey("iftttKey"))
            {
                Globals.iftttKey = rss["iftttKey"].ToString();
            }
            Globals.events = rss["events"];
            return Task.CompletedTask;
        }
        public static async Task Send(String eventName, String iftttKey)
        {
            ReadEventsFile().Wait();
            var withJson = "";
            var content = new StringContent($"", Encoding.UTF8, "application/json");
            foreach (JProperty item in Globals.events.Cast<JProperty>())
            {
                if (eventName == item.Name)
                {
                    var jsonPayload = JsonConvert.DeserializeObject<JArray>(item.Value.ToString()).ToObject<List<JObject>>().FirstOrDefault();
                    content = new StringContent(jsonPayload.ToString(), Encoding.UTF8, "application/json");
                    if (!jsonPayload.ContainsKey("value1"))
                    {
                        withJson = "/json";
                    }
                    break;
                }
            }
            var url = new Uri($"https://maker.ifttt.com/trigger/{eventName}{withJson}/with/key/{iftttKey}");

            var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(10);
            var response = await client.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();
            Globals.httpResponses[eventName] = response.StatusCode.ToString();
            var _iftttEventTrigger = new IftttEventTrigger(eventName);
            HttpResponse?.Invoke(_plugin, _iftttEventTrigger);
            await Task.Delay(3000);
            Globals.httpResponses[eventName] = null;
            HttpResponse?.Invoke(_plugin, _iftttEventTrigger);
        }
    }
}
