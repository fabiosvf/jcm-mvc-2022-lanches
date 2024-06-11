using LanchesMac.Context;
using LanchesMac.Models;
using LanchesMac.Repositories.Interfaces;

namespace LanchesMac.Repositories;

public class PedidoRepository : IPedidoRepository
{
	private readonly AppDbContext _context;
	private readonly CarrinhoCompra _carrinhoCompra;

	public PedidoRepository(AppDbContext context, CarrinhoCompra carrinhoCompra)
	{
		_context = context;
		_carrinhoCompra = carrinhoCompra;
	}

	public void CriarPedido(Pedido pedido)
	{
		pedido.PedidoEnviado = DateTime.Now;
		_context.Pedidos.Add(pedido);
		_context.SaveChanges();

		var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItens;
		_context.PedidoDetalhes.AddRange(carrinhoCompraItens.Select(c => new PedidoDetalhe
		{
			Quantidade = c.Quantidade,
			LancheId = c.Lanche.LancheId,
			PedidoId = pedido.PedidoId,
			Preco = c.Lanche.Preco
		}));
		_context.SaveChanges();
	}
}
