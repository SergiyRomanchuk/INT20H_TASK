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
    public class VkOwnerPhotoWorker : IVkPhotoWorker
    {
        private string _accessToken;

        public VkOwnerPhotoWorker(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<UrlResponse> GetUploadUrl(int id)
        {
            using (var httpClient = new HttpClient())
            {
                var uri = String.Format("https://api.vk.com/method/photos.getOwnerPhotoUploadServer?owner_id={0}&access_token={1}&v={2} ", id, _accessToken, "5.62");
                var responseString = await httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<VkRootObject<UrlResponse>>(responseString).Response;
            }
        }

        public async Task<UploadPhotoResult> UploadPhoto(string url, string photoFolderPath, byte[] photo)
        {
            HttpClient client = new HttpClient();
            MultipartContent content = new System.Net.Http.MultipartFormDataContent();
            var photoFile = new ByteArrayContent(photo);
            photoFile.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
            {
                Name = "photo",
                FileName = photoFolderPath + "result.jpg",
            };
            content.Add(photoFile);
            HttpResponseMessage response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UploadOwnerResult>(responseBody);
        }

        public async Task<Photo> SavePhoto(UploadPhotoResult savingData, int id)
        {
            using (var httpClient = new HttpClient())
            {
                var uri =
                    String.Format(
                        "https://api.vk.com/method/photos.saveOwnerPhoto?photo={0}&server={1}&hash={2}&owner_id={3}&access_token={4}&v={5}",
                        savingData.Photo, savingData.Server, savingData.Hash, id, _accessToken, "5.62");
                var responseString = await httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<VkRootObject<Photo>>(responseString).Response;
            }
        }

        public async Task<Photo> DoAction(int id, string photoFolderPath, byte[] photo)
        {
            var urlResponse = await this.GetUploadUrl(id);
            var uploadResponse = await this.UploadPhoto(urlResponse.UploadUrl, photoFolderPath, photo);
            return await this.SavePhoto(uploadResponse, Math.Abs(id));
        }
    }
}
