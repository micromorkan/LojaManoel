using LojaManoel.Domain;
using LojaManoel.Model;
using LojaManoel.Service;
using LojaManoel.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LojaManoel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PedidosController : ControllerBase
    {
        private readonly IEmpacotarService _empacotarService;

        public PedidosController(IEmpacotarService empacotarService)
        {
            _empacotarService = empacotarService;
        }

        [HttpPost("Empacotar")]
        public IActionResult EmpacotarPedidos([FromBody] Request pedidos)
        {
            var resultado = new List<object>();

            foreach (var pedido in pedidos.Pedidos)
            {
                try
                {
                    var caixasUsadas = _empacotarService.Empacotar(pedido);

                    resultado.Add(new
                    {
                        Pedido_Id = pedido.Pedido_Id,
                        Caixas = caixasUsadas.Select(c => new
                        {
                            Caixa_Id = c.caixa.Caixa_Id,
                            Produtos = c.produtos.Select(p => p.Produto_Id),
                            Observacao = c.caixa.Observacao
                        })
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(new { erro = $"Erro no pedido {pedido.Pedido_Id}: {ex.Message}" });
                }
            }

            return Ok(resultado);
        }
    }
}