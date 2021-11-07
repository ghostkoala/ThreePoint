using System.Threading.Tasks;
using ThreePoint.IRepository;
using ThreePoint.Web.Attributes;
using ThreePoint.Web.Extensions;
using ThreePoint.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ThreePoint.Web.Controllers
{
    public class InitConTroller : Controller
    {
        private readonly IDatabaseInit databaseInit;

        public InitConTroller(IDatabaseInit databaseInit)
        {
            this.databaseInit = databaseInit;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Init()
        {
            var menues = MenuHelper.GetMenues();
            await databaseInit.InitAsync(menues);
            return Ok();
        }
    }
}