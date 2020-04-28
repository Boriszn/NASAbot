using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NASABot.Api.Model
{
    //public partial class CoronalMassEjectionRoot
    //{
    //    public List<CoronalMassEjection> CoronalMassEjection { get; set; }
    //}

    public partial class CoronalMassEjection
    {
        [JsonProperty("activityID")]
        public string ActivityId { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("sourceLocation")]
        public string SourceLocation { get; set; }

        [JsonProperty("activeRegionNum")]
        public object ActiveRegionNum { get; set; }

        [JsonProperty("instruments")]
        public List<Instrument> Instruments { get; set; }

        [JsonProperty("cmeAnalyses")]
        public List<CmeAnalysis> CmeAnalyses { get; set; }

        [JsonProperty("linkedEvents")]
        public object LinkedEvents { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("catalog")]
        public string Catalog { get; set; }
    }

    public partial class CmeAnalysis
    {
        [JsonProperty("time21_5")]
        public string Time215 { get; set; }

        [JsonProperty("latitude")]
        public long? Latitude { get; set; }

        [JsonProperty("longitude")]
        public long? Longitude { get; set; }

        [JsonProperty("halfAngle")]
        public long? HalfAngle { get; set; }

        [JsonProperty("speed")]
        public long? Speed { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("isMostAccurate")]
        public bool IsMostAccurate { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("levelOfData")]
        public long LevelOfData { get; set; }

        [JsonProperty("enlilList")]
        public List<EnlilList> EnlilList { get; set; }
    }

    public partial class EnlilList
    {
        [JsonProperty("modelCompletionTime")]
        public string ModelCompletionTime { get; set; }

        [JsonProperty("au")]
        public long Au { get; set; }

        [JsonProperty("estimatedShockArrivalTime")]
        public string EstimatedShockArrivalTime { get; set; }

        [JsonProperty("estimatedDuration")]
        public object EstimatedDuration { get; set; }

        [JsonProperty("rmin_re")]
        public object RminRe { get; set; }

        [JsonProperty("kp_18")]
        public object Kp18 { get; set; }

        [JsonProperty("kp_90")]
        public long? Kp90 { get; set; }

        [JsonProperty("kp_135")]
        public long? Kp135 { get; set; }

        [JsonProperty("kp_180")]
        public long? Kp180 { get; set; }

        [JsonProperty("isEarthGB")]
        public bool IsEarthGb { get; set; }

        [JsonProperty("impactList")]
        public List<ImpactList> ImpactList { get; set; }

        [JsonProperty("cmeIDs")]
        public List<string> CmeIDs { get; set; }
    }

    public partial class ImpactList
    {
        [JsonProperty("isGlancingBlow")]
        public bool IsGlancingBlow { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("arrivalTime")]
        public string ArrivalTime { get; set; }
    }

    public partial class Instrument
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }
}
