using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Controllers
{
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
        private readonly ILogger<PlanoContaController> _logger;

        public TransacaoController(ILogger<PlanoContaController> logger)
        {
            _logger = logger;
        }
        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        [Route("Cadastro")]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        [Route("Cadastro")]
        public IActionResult Cadastro(TransacaoModel model)
        {
            return RedirectToAction("Index");
        }
        

    }
}