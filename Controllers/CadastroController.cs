using Microsoft.AspNetCore.Mvc;

namespace SistemaAdm.Controllers;

public class CadastroController : Controller
{
   [HttpGet]
   public IActionResult Index()
    {
        return View();
    }
}
