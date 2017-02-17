using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammCreator.Domain.Services.Images.Helpers
{
    public class ImageHelper
    {
        public static byte[] GetBytes(string filePath)
        {
            byte[] fileBytes = File.ReadAllBytes(filePath);
            return fileBytes;
        }
    }
}
