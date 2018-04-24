using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReTwitter.DTO;

namespace ReTwitter.Web.Areas.Admin.Models.Users
{
    public class UserListingsViewModel
    {
        public IEnumerable<UserDto> Users { get; set; }

        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}
