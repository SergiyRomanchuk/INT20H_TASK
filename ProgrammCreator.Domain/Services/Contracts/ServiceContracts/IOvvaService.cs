using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammCreator.Domain.Services.Contracts
{
    public interface IOvvaService<T>
    {
        IProgramSelector<T> Selector { get; set; }
        /// <summary>
        /// return the tv program object from OvvaAPI
        /// </summary>
        /// <param name="language">ua, ru</param>
        /// <param name="channelKey">string value of ovva channelId</param>
        /// <param name="day">date format yyyy-mm-dd</param>
        /// <returns>Custom object of the tv program</returns>
        Task<T> GetProgramm(string language, string channelKey, string day = "");
        /// <summary>
        /// Get program using default state of program selector
        /// </summary>
        /// <returns>Program object</returns>
        Task<T> GetProgramm();
    }
}
