using System;
using System.Collections.Generic;

namespace CardapioAdmin.Models;

public partial class Produto
{
    public int Id { get; set; }

    public int CategoriaId { get; set; }

    public string Nome { get; set; } = null!;

    public string? Descricao { get; set; }

    public decimal Preco { get; set; }

    public string? ImagemUrl { get; set; }

    public bool Disponivel { get; set; }

    public bool Ativo { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual Categoria Categoria { get; set; } = null!;
}
