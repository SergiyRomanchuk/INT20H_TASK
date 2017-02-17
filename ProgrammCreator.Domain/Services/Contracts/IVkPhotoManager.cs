using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammCreator.Domain.Enities;

namespace ProgrammCreator.Domain.Services.Contracts
{
    public interface IVkPhotoManager
    {
        IVkUrlGetter UrlSelector { get; set; }
        IVkPhotoUploader PhotoUploader { get; set; }
        IVkPhotoSaver PhotoSaver { get; set; }
        Task<Photo> SetOwnerPhoto(int id, byte[] photo);
    }
}
