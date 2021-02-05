using Microsoft.AspNetCore.Identity;
using MovieManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Areas.Identity.Validators
{
    public class UserEmailValidator<TUser> : UserValidator<TUser>
        where TUser : ApplicationUser
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            List<IdentityError> errors = new List<IdentityError>();

            var existingUser = await manager.FindByEmailAsync(user.Email);

            if (existingUser != null && existingUser.Id != user.Id)
            {
                errors.Add(new IdentityError() { Code = "DuplicateEmail", Description = $"Email '{user.Email}' is already in use" });
            }

            return errors.Any()
                ? IdentityResult.Failed(errors.ToArray())
                : IdentityResult.Success;

        }
    }
}
