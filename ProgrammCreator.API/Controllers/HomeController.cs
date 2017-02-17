using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProgrammCreator.API.Models;
using ProgrammCreator.API.Models.Helpers;
using ProgrammCreator.Domain.Enities;
using ProgrammCreator.Domain.Services.Contracts;
using ProgrammCreator.Domain.Services.Images.Generators;
using ProgrammCreator.Domain.Services.Selectors.ChannelSelectors;
using ProgrammCreator.Domain.Services.Selectors.VkSelectors;
using ProgrammCreator.Domain.Services.ServiceImplementations;
using ProgrammCreator.Domain.Services.VkRequests.Implementations.Vk;

namespace ProgrammCreator.API.Controllers
{
    public class HomeController : Controller
    {
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
            if (Session["access_token"] == null)
                return RedirectToAction("Auth");
            var model = new MainPageViewModel()
            {
                VkUserId = Session["user_id"].ToString()
            };

            VkService = new VkService(Session["access_token"].ToString());
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
            //string tokenString = Session["access_token"].ToString();
            //var vkService = new VkService(new AdminGroupSelector(tokenString), new VkOwnerPhotoWorker(tokenString));
            //var photo = await vkService.PublishPhoto(-140095872);
            //vkService = new VkService(new AdminGroupSelector(tokenString), new VkWallPhotoWorker(tokenString));
            //var photo2 = await vkService.PublishPhoto(-140095872);
            //var groups = await vkService.GetGroup(Session["user_id"].ToString(), Session["access_token"].ToString());
            
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
            var tokenString = Session["access_token"].ToString();
            if (VkService == null) VkService = new VkService(tokenString);
            VkService.PhotoWorker = new VkWallPhotoWorker(tokenString);
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
            var tokenString = Session["access_token"].ToString();
            if (VkService == null) VkService = new VkService(tokenString);
            VkService.PhotoWorker = new VkOwnerPhotoWorker(tokenString);
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
