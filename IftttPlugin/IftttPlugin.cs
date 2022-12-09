namespace Loupedeck.IftttPlugin
{
    using System;

    // This class contains the plugin-level logic of the Loupedeck plugin.

    public class IftttPlugin : Plugin
    {
        private String iftttKey;
        // Gets a value indicating whether this is an Universal plugin or an Application plugin.
        public override Boolean UsesApplicationApiOnly => true;

        // Gets a value indicating whether this is an API-only plugin.
        public override Boolean HasNoApplication => true;

        // This method is called when the plugin is loaded during the Loupedeck service start-up.
        public override void Load()
        {
            IftttPluginInstaller.Install();
            this.UpdateSavedKey();
            this.Info.Icon16x16 = EmbeddedResources.ReadImage(EmbeddedResources.FindFile("Icon16x16.png"));
            this.Info.Icon32x32 = EmbeddedResources.ReadImage(EmbeddedResources.FindFile("Icon32x32.png"));
            this.Info.Icon48x48 = EmbeddedResources.ReadImage(EmbeddedResources.FindFile("Icon48x48.png"));
            this.Info.Icon256x256 = EmbeddedResources.ReadImage(EmbeddedResources.FindFile("Icon256x256.png"));

        }

        // This method is called when the plugin is unloaded during the Loupedeck service shutdown.
        public override void Unload()
        {
        }
        public void UpdateSavedKey()
        {
            this.TryGetSetting("iftttKey", out var iftttKey);
            if (Helper.Globals.iftttKey != null && Helper.Globals.iftttKey != iftttKey)
            {
                this.SetPluginSetting("iftttKey", Helper.Globals.iftttKey);
                this.OnPluginStatusChanged(Loupedeck.PluginStatus.Normal, "For Setup instructions refer to the plugin's ", "https://github.com/shells-dw/loupedeck-ifttt#events.json", "GitHub Readme");
            }
            if (Helper.Globals.iftttKey == null || Helper.Globals.iftttKey == "")
            {
                this.OnPluginStatusChanged(Loupedeck.PluginStatus.Warning, "No IFTTT Webhook key found in events.json. Please make sure you've entered it correctly, then restart the Loupdeck Service", "https://github.com/shells-dw/loupedeck-ifttt#events.json", "GitHub Readme");
            }
            return;
        }

        private void FetchSettings() => this.TryGetSetting("iftttKey", out this.iftttKey);

        public Boolean TryGetSetting(String settingName, out String settingValue) =>
            this.TryGetPluginSetting(settingName, out settingValue);

        public void SetSetting(String settingName, String settingValue, Boolean backupOnline = false) =>
            this.SetPluginSetting(settingName, settingValue, backupOnline);

    }
}
