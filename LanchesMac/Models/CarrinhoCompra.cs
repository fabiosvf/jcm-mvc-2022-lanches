using LanchesMac.Context;

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
}
