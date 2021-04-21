using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Forum.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ForumUser class
    public class ForumUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName="varchar(100)")]
        public string FirstName { get; set; }
        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "varchar(100)")]
        [DisplayName("Image Name")]
        public string ProfilePicture { get; set; }

        [PersonalData]
        [DisplayName("Profile picture")]
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
    }
}
