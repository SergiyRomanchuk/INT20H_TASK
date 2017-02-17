using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammCreator.Domain.Services.Contracts;

namespace ProgrammCreator.Domain.Services.ServiceImplementations
{
    public class OvvaService<T> : IOvvaService<T>
    {
        public IProgramSelector<T> Selector { get; set; }

        public OvvaService(IProgramSelector<T> programSelector)
        {
            Selector = programSelector;
        }

        public Task<T> GetProgramm(string language, string channelKey, string day = "")
        {
            Selector.Language = language;
            Selector.ChannelKey = channelKey;
            Selector.Day = day;
            return Selector.GetProgramm();
        }

        public Task<T> GetProgramm()
        {
            return Selector.GetProgramm();
        }
    }
}
