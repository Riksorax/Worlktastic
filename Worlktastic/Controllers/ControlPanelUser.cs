using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Worlktastic.Controllers
{
    [Authorize(Roles = "PowerUser")]
    [Authorize(Roles = "ControlPanelUser")]
    [Authorize(Roles = "Administrator, PowerUser")]
    public class ControlPanelUser : Controller
    {
        public IActionResult Index() =>
        Content("PowerUser && ControlPanelUser");
        public IActionResult SetTime() =>
        Content("Administrator || PowerUser");

        [Authorize(Roles = "Administrator")]
        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult ShutDown() =>
        Content("Administrator only");
    }
}
