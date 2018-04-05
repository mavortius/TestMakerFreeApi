using Microsoft.AspNetCore.Mvc;
using TestMakerFreeApi.Data;

namespace TestMakerFreeApi.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiClontroller : Controller
    {
        public BaseApiClontroller(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected ApplicationDbContext DbContext { get; private set; }
    }
}