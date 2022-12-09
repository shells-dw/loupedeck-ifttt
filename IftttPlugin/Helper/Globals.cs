namespace Loupedeck.IftttPlugin.Helper
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json.Linq;

    public static class Globals
    {
        public static String iftttKey;
        public static JToken events;
        public static Dictionary<String, String> httpResponses = new Dictionary<String, String>();
    }
}
