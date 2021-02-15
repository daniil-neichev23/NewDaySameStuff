﻿using Microsoft.AspNetCore.Identity;
using NewDaySameStuff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewDaySameStuff.IdentityPolicy
{
    public class CustomUsernameEmailPolicy : UserValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> userManager,
            AppUser user)
        {
            IdentityResult result = await base.ValidateAsync(userManager, user);
            List<IdentityError> errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();
            if (user.UserName == "google")
            {
                errors.Add(new IdentityError
                {
                    Description = "Google cannot be used as a user name"
                });
            }
            if (user.Email.ToLower().EndsWith("@mail.ru"))
            {
                errors.Add(new IdentityError
                {
                    Description = "Mail.ru cannot be used as email address"
                });
            }
            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
