using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ProgrammCreator.API.Models;
using ProgrammCreator.Domain.Enities;
using ProgrammCreator.Domain.Services.Contracts;
using ProgrammCreator.Domain.Services.Images.Generators;
using ProgrammCreator.Domain.Services.Selectors.ChannelSelectors;
using ProgrammCreator.Domain.Services.ServiceImplementations;

namespace ProgrammCreator.API.Controllers
{
    public class PictureController : ApiController
    {
        private readonly IOvvaService<TvProgramRoot<TvProgram>> OvvaService;
        private IPictureService PictureService;

        public PictureController() 
            : this(new OvvaService<TvProgramRoot<TvProgram>>(new OnePlusOneProgrammSelector<TvProgramRoot<TvProgram>>("ua"))
                  , new PictureService())
        {
            
        }

        public PictureController(IOvvaService<TvProgramRoot<TvProgram>> ovvaService, IPictureService pictureService)
        {
            OvvaService = ovvaService;
            PictureService = pictureService;
        }

        // GET api/values
        public async Task<PictureDTO> Get(string lang = "ua")
        {
            //Configure lang
            OvvaService.Selector.Language = lang;
            try
            {
                var ovvaResponse = await OvvaService.GetProgramm();
                var picturePath = PictureService.GenerateAndSaveFile(ovvaResponse.Program);
                return new PictureDTO() {Path = picturePath};
            }
            catch
            {
                return new PictureDTO();
            }
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
