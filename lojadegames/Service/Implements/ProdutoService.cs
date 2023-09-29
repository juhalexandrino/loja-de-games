using LojaDeGames.Data;
using LojaDeGames.Model;
using Microsoft.EntityFrameworkCore;

namespace LojaDeGames.Service.Implements
{
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _context;
        public ProdutoService(AppDbContext context) {
            _context = context;
        }
        public async Task<Produto?> Create(Produto produto)
        {
            if(produto.Categoria is not null)
            {
                var buscaProduto = await _context.Categorias.FindAsync(produto.Categoria.Id);

                if (buscaProduto == null)
                    return null;

                produto.Categoria = buscaProduto;
            }

            await _context.Produtos.AddAsync(produto);
            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task Delete(Produto produto)
        {
            _context.Remove(produto);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Produto>> GetAll()
        {
            return await _context.Produtos
                .Include(p => p.Categoria)
                .ToListAsync();
        }

        public async Task<Produto?> GetById(long id)
        {
            try
            {
                var produto = await _context.Produtos
                    .Include(p => p.Categoria)
                    .FirstAsync(i => i.Id == id);

                return produto;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Produto>> GetByNome(string nome)
        {
            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(t => t.Nome.Contains(nome)).ToListAsync();

            return produto;
        }

        public async Task<Produto?> Update(Produto produto)
        {
            var produtoUpdate = await _context.Produtos.FindAsync(produto.Id);

            if (produtoUpdate == null)
                return null;

            if(produto.Categoria is not null)
            {
                var buscaCategoria = await _context.Produtos.FindAsync(produto.Categoria.Id);

                if (buscaCategoria == null)
                    return null;
            }

            await _context.SaveChangesAsync();

            return produto;
        }

        public async Task<IEnumerable<Produto>> GetByNomeOuConsole(string nome, string console)
        {
            return await _context.Produtos
                .Where(produto => produto.Nome == nome || produto.Console == console)
                .Include(t => t.Categoria)
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetByPrecoIntervalo(decimal min, decimal max)
        {
            return await _context.Produtos
                .Where(produto => produto.Preco >= min && produto.Preco <= max)
                .Include(t => t.Categoria)
                .ToListAsync();
        }
    }
}
