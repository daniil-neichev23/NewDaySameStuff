using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDaySameStuff.Models
{
    public class PostViewModel
    {

        [Required(ErrorMessage = "Please choose image")]
        public string ProfilePicture { get; set; }

        public IFormFile Photo { get; set; }

        public int Like { get; set; }
    }
}
