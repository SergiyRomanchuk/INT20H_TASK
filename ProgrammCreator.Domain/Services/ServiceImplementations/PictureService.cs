using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ProgrammCreator.Domain.Enities;
using ProgrammCreator.Domain.Services.Contracts;
using ProgrammCreator.Domain.Services.Images.Generators;

namespace ProgrammCreator.Domain.Services.ServiceImplementations
{
    public class PictureService : IPictureService
    {
        private readonly IPictureGenerator PictureBuilder;

        public PictureService():this(new TablePictureGenerator())
        {
            
        }
        public PictureService(IPictureGenerator pictureBuilder)
        {
            PictureBuilder = pictureBuilder;
        }

        public Bitmap Generate(ICollection<TvShow> programs)
        {
            PictureBuilder.CreateBody(programs);
            return PictureBuilder.GetResult();
        }

        public Bitmap Generate(TvProgram program)
        {
            PictureBuilder.CreateBody(program);
            return PictureBuilder.GetResult();
        }

        public string GenerateAndSaveFile(ICollection<TvShow> programs, string folderPath)
        {
            var tvProgramBitmap = this.Generate(programs);
            string fileName = folderPath.EndsWith("/") ? "result.jpg" : "/result.jpg";
            var picturePath = String.Concat(folderPath, fileName);
            tvProgramBitmap.Save(picturePath);
            return picturePath.Replace(HttpContext.Current.Server.MapPath("~/"), String.Empty);
            
        }

        public string GenerateAndSaveFile(TvProgram program)
        {
            return this.GenerateAndSaveFile(program.Programs,
                HttpContext.Current.Server.MapPath("~/Content/ImageResults"));
        }
    }
}
