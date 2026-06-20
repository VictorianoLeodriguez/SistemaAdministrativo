using Microsoft.AspNetCore.Mvc;
using SistemaAdm.Contracts;
using SistemaAdm.Service;
using SistemaAdm.ViewModel;

namespace SistemaAdm.Controllers;

public class CadastroController : Controller
{
   [HttpGet]
   public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SalvarUser(CadastroModel cadastro)
    {
        if (!ModelState.IsValid)
            return View("index", cadastro);
    
        CadastroResult resultado = CadService.SalvarUser(cadastro.CPFJ, cadastro.Email, cadastro.Nome, cadastro.Senha);
    
        if (!resultado.Sucesso)
        {
            ModelState.AddModelError("", resultado.Mensagem);
            return View("index", cadastro);
        }
    
        TempData["Sucesso"] = resultado.Mensagem;
        return RedirectToAction("Index");
    }
}
