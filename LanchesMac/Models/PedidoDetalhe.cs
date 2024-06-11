using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LanchesMac.Models;

[Table("PedidoDetalhes")]
public class PedidoDetalhe
{
    [Key]
    public int PedidoDetalheId { get; set; }
    public int PedidoId { get; set; }
    public int LancheId { get; set; }
    public int Quantidade { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Preco { get; set; }

    public virtual Lanche Lanche { get; set; }
    public virtual Pedido Pedido { get; set; }
}
