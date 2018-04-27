using System.ComponentModel.DataAnnotations;

namespace ReTwitter.Web.Areas.Admin.Models.Users
{
    public class AddUserToRoleViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
