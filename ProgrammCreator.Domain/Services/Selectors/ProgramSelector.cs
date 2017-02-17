using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProgrammCreator.Domain.Services.Contracts;

namespace ProgrammCreator.Domain.Services.Selectors
{
    public abstract class ProgramSelector<T>: IProgramSelector<T>
    {
        public virtual async Task<T> GetProgramm()
        {
            T programObject;
            using (var httpClient = new HttpClient())
            {
                var uri = String.Format("https://api.ovva.tv/v2/{0}/tvguide/{1}/{2}", Language, ChannelKey, Day);
                var responseString = await httpClient.GetStringAsync(uri);
                programObject = JsonConvert.DeserializeObject<T>(responseString);
            }
            return programObject;
        }

        public abstract string ChannelKey { get; set; }
        /// <summary>
        /// ua - for Ukrainian and ru - for Russian
        /// </summary>
        public abstract string Language { get; set; }
        public virtual string Day { get; set; }
    }
}
