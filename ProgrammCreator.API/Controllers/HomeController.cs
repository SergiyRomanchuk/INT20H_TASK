using System;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using ProgrammCreator.API.Models;
using ProgrammCreator.API.Models.Helpers;
using ProgrammCreator.Domain.Services.Contracts;
using ProgrammCreator.Domain.Services.ServiceImplementations;
using ProgrammCreator.Domain.Services.VkRequests.Implementations.Vk;

namespace ProgrammCreator.API.Controllers
{
    public class HomeController : Controller
    {
        public string VkAccessToken
        {
            get
            {
                var sessionObject = Session["access_token"];
                if (sessionObject == null) return String.Empty;
                return sessionObject.ToString();
            }
        }
        /*
         VKApi: ID: 5870848
         Key: W7k78MIIZkGh3hGwmevP
        */
        private IVkService VkService;

        public HomeController()
        {
            
        }

        public HomeController(IVkService vkService)
        {
            VkService = vkService;
        }

        public async Task<ActionResult> Index()
        {
            if (String.IsNullOrWhiteSpace(VkAccessToken))
                return RedirectToAction("Auth");
            var model = new MainPageViewModel()
            {
                VkUserId = VkAccessToken
            };

            VkService = new VkService(VkAccessToken);
            try
            {
                var groupsResult = await VkService.GetGroups(model.VkUserId);
                model.UserGroups = groupsResult.Groups;
            }
            catch
            {
                model.UserGroups = null;
            }
            return View(model);
        }
        
        public ActionResult Auth()
        {
            return View();
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Auth([FromBody]AuthDTO authModel)
        {
            if (ModelState.IsValid)
            {
                var parameters = AuthHelper.GetSessionParamsFromLink(authModel.AuthLink);
                foreach (var cortege in parameters)
                {
                    Session.Add(cortege.Key, cortege.Value);
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> WallPost(int groupId)
        {
            if(groupId <= 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            VkService = new VkService(VkAccessToken)
            {
                PhotoWorker = new VkWallPhotoWorker(VkAccessToken)
            };
            try
            {
                await VkService.PublishPhoto(groupId);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ActionResult> OwnerPhoto(int groupId)
        {
            if (groupId <= 0) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            VkService = new VkService(VkAccessToken)
            {
                PhotoWorker = new VkOwnerPhotoWorker(VkAccessToken)
            };
            try
            {
                await VkService.PublishPhoto(groupId);
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
