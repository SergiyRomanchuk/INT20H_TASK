using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammCreator.Domain.Services.Selectors.ChannelSelectors
{
    public class OnePlusOneProgrammSelector<T> : ProgramSelector<T>
    {
        public sealed override string ChannelKey { get; set; }
        public sealed override string Language { get; set; }

        public OnePlusOneProgrammSelector(string language)
        {
            ChannelKey = "1plus1";
            Language = language;
        }
    }
}
