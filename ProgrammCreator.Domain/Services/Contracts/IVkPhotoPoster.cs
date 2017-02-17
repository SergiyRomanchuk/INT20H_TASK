using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammCreator.Domain.Services.Contracts
{
    public interface IVkPhotoPoster : IVkPhotoManager
    {
        IVkWallPoster PhotoPoster { get; set; }
        Task<int> WallPost(int id, byte[] photo);
    }
}
