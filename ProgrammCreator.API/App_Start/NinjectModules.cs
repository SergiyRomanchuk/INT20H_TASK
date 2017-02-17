using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Modules;
using ProgrammCreator.Domain.Enities;
using ProgrammCreator.Domain.Services.Contracts;
using ProgrammCreator.Domain.Services.ServiceImplementations;
using ProgrammCreator.Domain.Services.VkRequests.Implementations;

namespace ProgrammCreator.API.App_Start
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IOvvaService<TvProgramRoot<TvProgram>>>().To<OvvaService<TvProgramRoot<TvProgram>>>();
        }
        /*
         OwnerPhotoUrlGetter(accessToken), 
                      new PhotoUploader(), new OwnerPhotoSaver(accessToken), 
                      new WallPoster(accessToken)
         */
    }
}