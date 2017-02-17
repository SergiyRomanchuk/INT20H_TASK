using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProgrammCreator.Domain.Enities
{
    public class TvProgramRoot<T>
    {
        [JsonProperty("data")]
        public T Program { get; set; }
    }
}
