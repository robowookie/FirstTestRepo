using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using KinveyXamarin;

namespace CloudToDoAppTest
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ToDoClass
    {
        [JsonProperty("_id")]
        public string id { get; set; }
        [JsonProperty]
        public string title { get; set; }
        [JsonProperty]
        public string note { get; set; }
        [JsonProperty]
        public string user { get; set; }
    }
}