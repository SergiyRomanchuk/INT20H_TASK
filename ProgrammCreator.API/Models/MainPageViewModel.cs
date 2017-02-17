using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProgrammCreator.Domain.Enities;

namespace ProgrammCreator.API.Models
{
    public class MainPageViewModel
    {
        public string VkUserId { get; set; }
        public ICollection<VkGroup> UserGroups { get; set; }
    }
}