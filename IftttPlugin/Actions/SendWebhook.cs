namespace Loupedeck.IftttPlugin.Actions
{
    using System;
    using System.Linq;

    using Loupedeck.IftttPlugin.Helper;
    using Newtonsoft.Json.Linq;

    public class SendWebhook : PluginDynamicCommand
    {

        private IftttPlugin _plugin;
        public SendWebhook()
            : base(displayName: "IFTTT webhook", description: "IFTTT Webhook", groupName: "IFTTT")
        {
            Functions.ReadEventsFile();
            this.MakeProfileAction("list;Select event to send with this button: ");
            foreach (JProperty item in Globals.events.Cast<JProperty>())
            {
                this.AddParameter(item.Name, item.Name, groupName: "IFTTT");
                Globals.httpResponses.Add(item.Name, null);
            }
        }
        protected override Boolean OnLoad()
        {
            this._plugin = base.Plugin as IftttPlugin;
            if (this._plugin is null)
            {
                return false;
            }
            Functions.HttpResponse += (sender, e) => this.ActionImageChanged(e.TriggeredEvent);
            return base.OnLoad();
        }
        protected override void RunCommand(String actionParameter)
        {
            this._plugin.TryGetPluginSetting("iftttKey", out var iftttKey);
            Functions.Send(actionParameter, iftttKey);

        }
        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            var currentState = false;
            if (Globals.httpResponses[actionParameter] == "OK")
            {
                currentState = true;
            }
            if (Globals.httpResponses[actionParameter] != "OK" && Globals.httpResponses[actionParameter] != null)
            {
                using (var bitmapBuilder = new BitmapBuilder(imageSize))
                {
                    bitmapBuilder.DrawRectangle(0, 0, 80, 80, BitmapColor.Black);
                    bitmapBuilder.FillRectangle(0, 0, 80, 80, color: currentState ? new BitmapColor(0, 0, 0, 255) : new BitmapColor(150, 0, 0, 255));
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(EmbeddedResources.FindFile("ifttt80.png")));
                    bitmapBuilder.DrawText(actionParameter, x: 5, y: 40, width: 70, height: 40, fontSize: 15, color: BitmapColor.White);
                //    Globals.httpStatusCode = "";
                    return bitmapBuilder.ToImage();
                }
            }
            if (this.Plugin.PluginStatus.Status.ToString() != "Normal")
            {
                using (var bitmapBuilder = new BitmapBuilder(imageSize))
                {
                    //drawing a black full-size rectangle to overlay the default graphic (TODO: figure out if that's maybe something that is done nicer than this)
                    bitmapBuilder.DrawRectangle(0, 0, 80, 80, new BitmapColor(0, 0, 0, 255));
                    bitmapBuilder.FillRectangle(0, 0, 80, 80, new BitmapColor(0, 0, 0, 255));
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(EmbeddedResources.FindFile("ifttt80.png")));

                    // draw icons for different cases

                    bitmapBuilder.DrawText("Error", x: 5, y: 35, width: 70, height: 40, fontSize: 20, color: new BitmapColor(255, 255, 255, 255));
                    return bitmapBuilder.ToImage();
                }
            }
            using (var bitmapBuilder = new BitmapBuilder(imageSize))
            {
                bitmapBuilder.DrawRectangle(0, 0, 80, 80, BitmapColor.Black);
                bitmapBuilder.FillRectangle(0, 0, 80, 80, color: currentState ? new BitmapColor(0, 100, 0, 255) : new BitmapColor(0, 0, 0, 255));
                bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage(EmbeddedResources.FindFile("ifttt80.png")));
                bitmapBuilder.DrawText(actionParameter, x: 5, y: 40, width: 70, height: 40, fontSize: 15, color: BitmapColor.White);

        //        Globals.httpStatusCode = "";
                return bitmapBuilder.ToImage();
            }
        }
    }
}
