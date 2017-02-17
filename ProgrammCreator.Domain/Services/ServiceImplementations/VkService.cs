using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ProgrammCreator.Domain.Enities;
using ProgrammCreator.Domain.Services.Contracts;
using ProgrammCreator.Domain.Services.Images.Helpers;
using ProgrammCreator.Domain.Services.Selectors.VkSelectors;
using ProgrammCreator.Domain.Services.VkRequests.Implementations;
using ProgrammCreator.Domain.Services.VkRequests.Implementations.Vk;

namespace ProgrammCreator.Domain.Services.ServiceImplementations
{
    public class VkService : IVkService
    {
        private IGroupSelector _vkGroupSelector;
        private IVkPhotoWorker _photoPoster { get; set; }

        public VkService(string accessToken)
            : this(new AdminGroupSelector(accessToken),
                  new VkWallPhotoWorker(accessToken))
        {
        }

        public VkService(IGroupSelector vkGroupSelector, IVkPhotoWorker photoPostManager)
        {
            _vkGroupSelector = vkGroupSelector;
            _photoPoster = photoPostManager;
        }

        public IVkPhotoWorker PhotoWorker
        {
            set { _photoPoster = value; }
        }

        public Task<UserGroups> GetGroups(string userId)
        {
            return _vkGroupSelector.GetGroupsByUser(userId);
        }
        
        public async Task<Photo> PublishPhoto(int groupId)
        {
            return await _photoPoster.DoAction(GroupIdToCorrect(groupId), ImageHelper.GetBytes(HttpContext.Current.Server.MapPath("~/Content/ImageResults/") + "result.jpg"));
        }

        private int GroupIdToCorrect(int groupId)
        {
            if (groupId > 0) return groupId * (-1);
            return groupId;
        }
    }
}
