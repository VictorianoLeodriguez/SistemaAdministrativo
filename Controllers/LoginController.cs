using Microsoft.AspNetCore.Mvc;
using SistemaAdm.Models;

namespace SistemaAdm.Controllers;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Auth(string usuario, string senha)
    {
        // autenticação futura
        return RedirectToAction("Index");
    }
}