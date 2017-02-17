using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProgrammCreator.API.Models
{
    public class AuthDTO
    {
        [Required]
        public string AuthLink { get; set; }
    }
}