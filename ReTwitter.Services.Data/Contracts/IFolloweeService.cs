﻿using ReTwitter.DTO;
using System.Collections.Generic;
using ReTwitter.Data.Models;

namespace ReTwitter.Services.Data.Contracts
{
    public interface IFolloweeService
    {
        List<FolloweeDto> GetAllFollowees(string userId);

        FolloweeDto GetFolloweeById(string id);
        Followee Create(FolloweeDto followee);
    }
}
