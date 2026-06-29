using Microsoft.AspNetCore.Mvc;
using SistemaAdm.Service;
using SistemaAdm.Contracts;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using SistemaAdm.ViewModel;

namespace SistemaAdm.Controllers;

public class LoginController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Auth(LoginModel user)
    {
        if(!ModelState.IsValid){
            return View("Index", user);
        }

        LoginResult resultadoLogin = LoginService.Autenticar(user.CPFJ, user.Senha);

        if (!resultadoLogin.Sucesso)
        {
            ModelState.AddModelError("", resultadoLogin.Mensagem);
            return View ("index", user);
        }

        var infos = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.CPFJ)
        };

        var identidade = new ClaimsIdentity(infos, "CookieAuth");
        var principal = new ClaimsPrincipal(identidade);

        await HttpContext.SignInAsync("CookieAuth", principal);

        // autenticação futura
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public async Task<IActionResult> LogOut(){
        await HttpContext.SignOutAsync("CookieAuth");
        return RedirectToAction("Index", "Login");
    }
}