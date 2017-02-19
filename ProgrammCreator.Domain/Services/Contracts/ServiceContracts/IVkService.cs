using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammCreator.Domain.Enities;

namespace ProgrammCreator.Domain.Services.Contracts
{
    public interface IVkService
    {
        Task<Photo> PublishPhoto(int groupId, string folderPath);
        Task<UserGroups> GetGroups(string userId);
        IVkPhotoWorker PhotoWorker { set; }
    }
}
