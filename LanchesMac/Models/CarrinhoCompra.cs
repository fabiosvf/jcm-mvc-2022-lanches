﻿using LanchesMac.Context;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Models;

public class CarrinhoCompra
{
	private readonly AppDbContext _context;

	public CarrinhoCompra(AppDbContext context)
	{
		_context = context;
	}

	public string CarrinhoCompraId { get; set; }
	public List<CarrinhoCompraItem> CarrinhoCompraItens { get; set; }

	public static CarrinhoCompra GetCarrinhoCompra(IServiceProvider services)
	{
		//Define uma sessão
		ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

		//Obtém um serviço do tipo do nosso contexto
		var context = services.GetService<AppDbContext>();

		//Obtém ou gera o Id do Carrinho
		string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

		//Atribui o Id do Carrinho na Sessão
		session.SetString("CarrinhoId", carrinhoId);

		//Retorna o carrinho com o contexto e o Id atribuído ou obtido
		return new CarrinhoCompra(context)
		{
			CarrinhoCompraId = carrinhoId
		};
	}

	public void AdicionarAoCarrinho(Lanche lanche)
	{
		var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
			s => s.Lanche.LancheId == lanche.LancheId &&
			s.CarrinhoCompraId == CarrinhoCompraId);

		if (carrinhoCompraItem == null)
		{
			carrinhoCompraItem = new CarrinhoCompraItem
			{
				CarrinhoCompraId = CarrinhoCompraId,
				Lanche = lanche,
				Quantidade = 1
			};
			_context.CarrinhoCompraItens.Add(carrinhoCompraItem);
		}
		else
		{
			carrinhoCompraItem.Quantidade++;
		}
		_context.SaveChanges();
	}

	public int RemoverDoCarrinho(Lanche lanche)
	{
		var carrinhoCompraItem = _context.CarrinhoCompraItens.SingleOrDefault(
			s => s.Lanche.LancheId == lanche.LancheId &&
			s.CarrinhoCompraId == CarrinhoCompraId);

		var quantidadeLocal = 0;

		if (carrinhoCompraItem != null)
		{
			if (carrinhoCompraItem.Quantidade > 1)
			{
				carrinhoCompraItem.Quantidade--;
				quantidadeLocal = carrinhoCompraItem.Quantidade;
			}
			else
			{
				_context.CarrinhoCompraItens.Remove(carrinhoCompraItem);
			}
		}
		_context.SaveChanges();
		return quantidadeLocal;
	}

	public List<CarrinhoCompraItem> GetCarrinhoCompraItens()
	{
		return CarrinhoCompraItens ?? (
				CarrinhoCompraItens = _context.CarrinhoCompraItens
				.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
				.Include(s => s.Lanche)
				.ToList()
			);
	}

	public void LimparCarrinho()
	{
		var carrinhoItens = _context.CarrinhoCompraItens
			.Where(c => c.CarrinhoCompraId == CarrinhoCompraId);

		_context.CarrinhoCompraItens.RemoveRange(carrinhoItens);
		_context.SaveChanges();
	}

	public decimal GetCarrinhoCompraTotal()
	{
		var total = _context.CarrinhoCompraItens
			.Where(c => c.CarrinhoCompraId == CarrinhoCompraId)
			.Select(c => c.Lanche.Preco * c.Quantidade)
			.Sum();

		return total;
	}


}
