using System.Threading.Tasks;
using ProgrammCreator.Domain.Enities;

namespace ProgrammCreator.Domain.Services.Contracts
{
    public interface IVkPhotoWorker
    {
        Task<UrlResponse> GetUploadUrl(int id);
        Task<UploadPhotoResult> UploadPhoto(string url, string folderPhotoPath, byte[] photo);
        Task<Photo> SavePhoto(UploadPhotoResult savingData, int id);
        Task<Photo> DoAction(int id, string folderPhotoPath, byte[] photo);
    }

    public interface IVkWallPhotoPoster : IVkPhotoWorker
    {
        Task<int> WallPost(int groupId, string type, int ownerId, int photoId);
    }

}
