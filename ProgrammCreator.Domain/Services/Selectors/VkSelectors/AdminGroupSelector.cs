using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProgrammCreator.Domain.Enities;
using ProgrammCreator.Domain.Services.Contracts;

namespace ProgrammCreator.Domain.Services.Selectors.VkSelectors
{
    public class AdminGroupSelector : IGroupSelector
    {
        private string _accessToken;

        public AdminGroupSelector(string accessToken)
        {
            _accessToken = accessToken;
        }

        public async Task<UserGroups> GetGroupsByUser(string userId)
        {
            using (var httpClient = new HttpClient())
            {
                var uri = String.Format("https://api.vk.com/method/groups.get?userId={0}&filter={1}&extended=1&access_token={2}&v={3} ", userId, "admin", _accessToken, "5.62");
                var responseString = await httpClient.GetStringAsync(uri);
                return JsonConvert.DeserializeObject<VkRootObject<UserGroups>>(responseString).Response;
            }
        }
    }
}
