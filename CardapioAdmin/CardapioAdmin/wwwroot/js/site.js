async function carregarDashboard() {

    try {

        const resposta = await fetch("/Home/Dados");

        if (!resposta.ok) {
            throw new Error("Não foi possível carregar os dados.");
        }

        const dados = await resposta.json();

        document.getElementById("totalProdutos").textContent =
            dados.totalProdutos;

        document.getElementById("totalCategorias").textContent =
            dados.totalCategorias;

        document.getElementById("produtosDisponiveis").textContent =
            dados.produtosDisponiveis;

        document.getElementById("produtosIndisponiveis").textContent =
            dados.produtosIndisponiveis;

        carregarProdutosRecentes(dados.produtosRecentes);

    }
    catch (erro) {

        console.error("Erro ao carregar Dashboard:", erro);

    }

}


// ==========================================
// TABELA DE PRODUTOS RECENTES
// ==========================================

function carregarProdutosRecentes(produtos) {

    const tabela = document.getElementById("tabelaProdutosRecentes");

    tabela.innerHTML = "";

    if (!produtos || produtos.length === 0) {

        tabela.innerHTML = `
            <tr>
                <td colspan="4">Nenhum produto cadastrado.</td>
            </tr>
        `;

        return;
    }

    produtos.forEach(produto => {

        const linha = document.createElement("tr");

        const nome = document.createElement("td");
        nome.textContent = produto.nome;

        const categoria = document.createElement("td");
        categoria.textContent = produto.categoria;

        const preco = document.createElement("td");

        preco.textContent = Number(produto.preco).toLocaleString(
            "pt-BR",
            {
                style: "currency",
                currency: "BRL"
            }
        );

        const disponibilidade = document.createElement("td");

        const status = document.createElement("span");

        status.className = produto.disponivel
            ? "product-status available"
            : "product-status unavailable";

        status.textContent = produto.disponivel
            ? "Disponível"
            : "Indisponível";

        disponibilidade.appendChild(status);

        linha.append(
            nome,
            categoria,
            preco,
            disponibilidade
        );

        tabela.appendChild(linha);

    });

}


/* ==========================================
   PRODUTOS
========================================== */

// carregarProdutos()
// cadastrarProduto()
// editarProduto()
// excluirProduto()


/* ==========================================
   CATEGORIAS
========================================== */

// carregarCategorias()
// cadastrarCategoria()
// editarCategoria()
// excluirCategoria()


/* ==========================================
   INICIALIZAÇÃO
========================================== */

document.addEventListener("DOMContentLoaded", function () {

    if (document.getElementById("totalProdutos")) {

        carregarDashboard();

    }

});
