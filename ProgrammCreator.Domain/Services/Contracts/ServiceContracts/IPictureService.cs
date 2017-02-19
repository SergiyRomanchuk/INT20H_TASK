using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammCreator.Domain.Enities;

namespace ProgrammCreator.Domain.Services.Contracts
{
    public interface IPictureService
    {
        Bitmap Generate(ICollection<TvShow> programs);
        Bitmap Generate(TvProgram program);
        string GenerateAndSaveFile(ICollection<TvShow> programs, string folderPath);
        string GenerateAndSaveFile(TvProgram program, string folderPath);
    }
}
