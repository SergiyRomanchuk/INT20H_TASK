using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammCreator.Domain.Services.Contracts
{
    public interface IProgramSelector<T>
    {
        Task<T> GetProgramm();
        string Language { get; set; }
        string ChannelKey { get; set; }
        string Day { get; set; }
    }
}
