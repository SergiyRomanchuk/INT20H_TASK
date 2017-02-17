using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ProgrammCreator.Domain.Enities
{
    public class TvShow
    {
        [JsonProperty("image")]
        public ImagePreview Image { get; set; }
        [JsonProperty("realtime_begin")]
        public int BeginTime { get; set; }
        [JsonProperty("realtime_end")]
        public int EndTime { get; set; }
        [JsonProperty("will_broadcast_available")]
        public bool WillOnline { get; set; }
        [JsonProperty("is_on_the_air")]
        public bool OnTheAir { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("subtitle")]
        public string SubTitle { get; set; }
        [JsonIgnore]
        public string StartTime
        {
            get
            {
                int timeZoneOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow).Hours;
                var date = DateTimeOffset.FromUnixTimeSeconds(this.BeginTime).AddHours(timeZoneOffset);
                return date.ToString("t");
            }
        }
    }

    public class ImagePreview
    {
        [JsonProperty("preview")]
        public string Preview { get; set; }
    }
}
