using Microsoft.AspNetCore.Mvc;
using SistemaAdm.Service;
using SistemaAdm.Models;
using SistemaAdm.Contracts;

namespace SistemaAdm.Controllers;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Auth(User user)
    {
        if(!ModelState.IsValid){
            return RedirectToAction("Index");
        }

        LoginResult resultadoLogin = LoginService.Autenticar(user.CPFJ, user.Senha);

        if (!resultadoLogin.Sucesso)
        {
            ModelState.AddModelError("", resultadoLogin.Mensagem);
            return View ("index", "Login");
        }

        // autenticação futura
        return RedirectToAction("Index");
    }
}