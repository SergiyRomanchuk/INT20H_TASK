using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using ProgrammCreator.Domain.Enities;
using ProgrammCreator.Domain.Services.Contracts;

namespace ProgrammCreator.Domain.Services.VkRequests.Implementations.Vk
{
    public class VkWallPhotoWorker : IVkPhotoWorker, IVkWallPoster
    {
        private string _accessToken;

        public VkWallPhotoWorker(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<UrlResponse> GetUploadUrl(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var uri = String.Format("https://api.vk.com/method/photos.getWallUploadServer?group_id={0}&access_token={1}&v={2} ", id, _accessToken, "5.62");
                var responseString = await httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<VkRootObject<UrlResponse>>(responseString).Response;
            }
        }

        public async Task<UploadPhotoResult> UploadPhoto(string url, byte[] photo)
        {
            HttpClient client = new HttpClient();
            MultipartContent content = new System.Net.Http.MultipartFormDataContent();
            var photoFile = new ByteArrayContent(photo);
            photoFile.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "photo",
                FileName = HttpContext.Current.Server.MapPath("~/Content/ImageResult/") + "result.jpg",
            };
            content.Add(photoFile);
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UploadPhotoResult>(responseBody);
        }

        public async Task<Photo> SavePhoto(UploadPhotoResult savingData, int groupId)
        {
            using (var httpClient = new HttpClient())
            {
                var uri =
                    String.Format(
                        "https://api.vk.com/method/photos.saveWallPhoto?photo={0}&server={1}&hash={2}&group_id={3}&access_token={4}&v={5}",
                        savingData.Photo, savingData.Server, savingData.Hash, groupId, _accessToken, "5.62");
                var responseString = await httpClient.GetStringAsync(uri);
                return
                    JsonConvert.DeserializeObject<VkRootObject<ICollection<Photo>>>(responseString)
                        .Response.FirstOrDefault();
            }
        }

        public async Task<int> WallPost(int groupId, string type, int ownerId, int photoId)
        {
            using (var httpClient = new HttpClient())
            {
                var uri =
                    String.Format(
                        "https://api.vk.com/method/wall.post?owner_id={0}&attachments={1}&access_token={2}&v={3}",
                        groupId, String.Concat(type, ownerId, '_', photoId), _accessToken, "5.62");
                var responseString = await httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<VkRootObject<WallPostResponse>>(responseString).Response.PostId;
            }
        }

        public async Task<Photo> DoAction(int id, byte[] photo)
        {
            //Next methods must have groupId > 0
            id = Math.Abs(id);
            var urlResponse = await this.GetUploadUrl(id);
            var uploadResponse = await this.UploadPhoto(urlResponse.UploadUrl, photo);
            var photoResult = await this.SavePhoto(uploadResponse, id);
            var wallPostResponse = await this.WallPost(-1*id, "photo", photoResult.OwnerId, photoResult.PhotoId);
            return photoResult;
        }
    }
}
