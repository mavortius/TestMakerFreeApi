using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TestMakerFreeApi.Data;
using TestMakerFreeApi.Data.Models;

namespace TestMakerFreeApi.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiClontroller : Controller
    {
        public BaseApiClontroller(ApplicationDbContext dbContext,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            DbContext = dbContext;
            RoleManager = roleManager;
            UserManager = userManager;
            Configuration = configuration;
        }

        protected ApplicationDbContext DbContext { get; private set; }
        
        protected RoleManager<IdentityRole> RoleManager { get; private set; }
        
        protected UserManager<ApplicationUser> UserManager { get; private set; }
        
        protected IConfiguration Configuration { get; private set; }
    }
}