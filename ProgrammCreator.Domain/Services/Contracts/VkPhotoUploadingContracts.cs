using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammCreator.Domain.Enities;

namespace ProgrammCreator.Domain.Services.Contracts
{
    // TODO Replace this interfaces
    public interface IVkPhotoUploader
    {
        Task<UploadPhotoResult> UploadPhoto(string url, byte[] photo);
    }

    public interface IVkUrlGetter
    {
        Task<UrlResponse> GetUploadUrl(int id);
    }

    public interface IVkPhotoSaver
    {
        Task<Photo> SavePhoto(UploadPhotoResult savingData, int id);
    }

    public interface IVkWallPoster
    {
        Task<int> WallPost(int groupId, string type, int ownerId, int photoId);
    }

    //
    public interface IVkPhotoWorker
    {
        Task<UrlResponse> GetUploadUrl(int id);
        Task<UploadPhotoResult> UploadPhoto(string url, byte[] photo);
        Task<Photo> SavePhoto(UploadPhotoResult savingData, int id);
        Task<Photo> DoAction(int id, byte[] photo);
    }

    public interface IVkWallPhotoPoster : IVkPhotoWorker
    {
        Task<int> WallPost(int groupId, string type, int ownerId, int photoId);
    }

}
