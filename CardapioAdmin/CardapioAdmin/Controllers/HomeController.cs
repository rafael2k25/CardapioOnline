using CardapioAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace CardapioAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CardapioBebidasContext _context;

        public HomeController(
            ILogger<HomeController> logger,
            CardapioBebidasContext context)
        {
            _logger = logger;
            _context = context;
        }

        // ABRIR DASHBOARD
        public IActionResult Index()
        {
            return View();
        }

        // BUSCAR DADOS DO DASHBOARD
        [HttpGet]
        public async Task<IActionResult> Dados()
        {
            var totalProdutos = await _context.Produtos.CountAsync();

            var totalCategorias = await _context.Categorias.CountAsync();

            var produtosDisponiveis = await _context.Produtos
                .CountAsync(p => p.Disponivel);

            var produtosIndisponiveis = await _context.Produtos
                .CountAsync(p => !p.Disponivel);

            var produtosRecentes = await _context.Produtos
                .Include(p => p.Categoria)
                .OrderByDescending(p => p.DataCadastro)
                .Take(5)
                .Select(p => new
                {
                    p.Id,
                    p.Nome,
                    Categoria = p.Categoria != null
                        ? p.Categoria.Nome
                        : "Sem categoria",
                    p.Preco,
                    p.Disponivel
                })
                .ToListAsync();

            return Json(new
            {
                totalProdutos,
                totalCategorias,
                produtosDisponiveis,
                produtosIndisponiveis,
                produtosRecentes
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id
                    ?? HttpContext.TraceIdentifier
            });
        }
    }
}
