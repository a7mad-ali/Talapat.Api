using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.Identity;

namespace Talabat.Core.Servicies.Contract
{
    public  interface IAuthService


    {

        Task<String> CreateTokenAsync(ApplicationUser user,UserManager<ApplicationUser> usermanger);
    }
}
