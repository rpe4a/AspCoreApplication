using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}