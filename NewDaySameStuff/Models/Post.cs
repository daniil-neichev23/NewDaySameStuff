using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDaySameStuff.Models
{
    public class Post
    {
        [Key]
        public int PostId { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        public string ProfilePicture { get; set; }

        public string Path { get; set; }

        public int Like { get; set; }
    }
}
