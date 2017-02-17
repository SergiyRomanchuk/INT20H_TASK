using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammCreator.Domain.Enities;

namespace ProgrammCreator.Domain.Services.Contracts
{
    public interface IGroupSelector
    {
        Task<UserGroups> GetGroupsByUser(string userId);
    }
}
