using Microsoft.AspNetCore.Mvc;
using SistemaAdm.Contracts;
using SistemaAdm.Service;
using SistemaAdm.Models;
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

        User user = new User
        {
            RazaoSocial = cadastro.RazaoSocial,
            NomeFantasia = cadastro.NomeFantasia,
            CNPJ = cadastro.CNPJ,
            Email = cadastro.Email,
            Senha = cadastro.Senha,
            DataFundacao = cadastro.DataFundacao ?? DateTime.MinValue,
            Site = cadastro.Site,
            CEP = cadastro.CEP,
            Logradouro = cadastro.Logradouro,
            Numero = cadastro.Numero,
            Complemento = cadastro.Complemento,
            Bairro = cadastro.Bairro,
            Cidade = cadastro.Cidade,
            Estado = cadastro.Estado,
            Telefone = cadastro.Telefone,
            Celular = cadastro.Celular,
            NomeResponsavel = cadastro.NomeResponsavel
        };
    
        CadastroResult resultado = CadService.SalvarUser(user);
    
        if (!resultado.Sucesso)
        {
            ViewBag.Error = resultado.Mensagem;
            return View("index", cadastro);
        }
    
        TempData["Sucesso"] = resultado.Mensagem;

        return RedirectToAction("Index", "Login");
    }
}
