using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammCreator.Domain.Enities;

namespace ProgrammCreator.Domain.Services.Contracts
{
    public interface IPictureGenerator
    {
        void CreateBody(ICollection<TvShow> programs);
        void CreateBody(TvProgram tvProgram);
        Bitmap GetResult();
    }
}
