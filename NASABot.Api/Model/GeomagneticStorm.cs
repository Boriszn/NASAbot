using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NASABot.Api.Model
{
    public partial class GeomagneticStorm
    {
        [JsonProperty("gstID")]
        public string GstId { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("allKpIndex")]
        public List<AllKpIndex> AllKpIndex { get; set; }

        [JsonProperty("linkedEvents")]
        public List<LinkedEvent> LinkedEvents { get; set; }

        [JsonProperty("link")]
        public Uri Link { get; set; }
    }

    public partial class AllKpIndex
    {
        [JsonProperty("observedTime")]
        public string ObservedTime { get; set; }

        [JsonProperty("kpIndex")]
        public long KpIndex { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }
    }

    public partial class LinkedEvent
    {
        [JsonProperty("activityID")]
        public string ActivityId { get; set; }
    }
}
