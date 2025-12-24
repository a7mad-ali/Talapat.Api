using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talapat.Infrastructure.Identity
{
    public  class ApplicationIdentityContextSeed
    {
        public static async Task SeeduserAsync(UserManager<ApplicationUser> userManager) 
        {
            if(!userManager.Users.Any())
            {
                var user = new ApplicationUser()
                {
                    DisplayName = "Ahmed Ali",
                    Email = "a7medali725@gmail.com",
                    UserName = "ahmed.ali",
                    PhoneNumber = "01156451789"
                    
                };
                await userManager.CreateAsync(user,"P@ssw0rd");
            }
            

        }
    }
}
