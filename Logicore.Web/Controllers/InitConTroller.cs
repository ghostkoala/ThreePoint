using System.Threading.Tasks;
using Logicore.IRepository;
using Logicore.Web.Attributes;
using Logicore.Web.Extensions;
using Logicore.Web.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Logicore.Web.Controllers
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