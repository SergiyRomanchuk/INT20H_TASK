using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ProgrammCreator.Domain.Enities
{
    public class UploadPhotoResult
    {
        [JsonProperty("server")]
        public int Server { get; set; }
        [JsonProperty("photo")]
        public string Photo { get; set; }
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }

    public class UploadOwnerResult : UploadPhotoResult
    {
        [JsonProperty("mid")]
        public int Mid { get; set; }
        [JsonProperty("message_code")]
        public int MessageCode { get; set; }
        [JsonProperty("profile_aid")]
        public int ProfileAid { get; set; }
    }

    public class UrlResponse
    {
        [JsonProperty("upload_url")]
        public string UploadUrl { get; set; }
    }

    public class UrlWallResponse : UrlResponse
    {
        [JsonProperty("album_id")]
        public int AlbumId { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
    }

    public class Photo
    {
        [JsonProperty("id")]
        public int PhotoId { get; set; }
        [JsonProperty("album_id")]
        public int AlbumId { get; set; }
        [JsonProperty("owner_id")]
        public int OwnerId { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("date")]
        public int Date { get; set; }
        [JsonProperty("photo_hash")]
        public string Hash { get; set; }
        [JsonProperty("photo_src_big")]
        public string PhotoUrl { get; set; }
    }

    public class WallPostResponse
    {
        [JsonProperty("post_id")]
        public int PostId { get; set; }
    }

    public class VkRootObject<T>
    {
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}
