using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProgrammCreator.Domain.Enities
{
    public class VkGroup
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class UserGroups
    {
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("items")]
        public List<VkGroup> Groups { get; set; }
    }
}
