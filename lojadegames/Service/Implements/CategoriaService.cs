using LojaDeGames.Data;
using LojaDeGames.Model;
using Microsoft.EntityFrameworkCore;

namespace LojaDeGames.Service.Implements
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Categoria?> Create(Categoria categoria)
        {
            await _context.Categorias.AddAsync(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }
        public async Task<Categoria?> Delete(Categoria categoria)
        {
            _context.Remove(categoria);
            await _context.SaveChangesAsync();

            return categoria;
        }

        public async Task<IEnumerable<Categoria>> GetAll()
        {
            return await _context.Categorias
                .Include(c => c.Produtos)
                .ToListAsync();
        }

        public async Task<Categoria?> GetById(long id)
        {
            try
            {
                var categoria = await _context.Categorias
                    .Include(c => c.Produtos)
                    .FirstAsync(i => i.Id == id);
                return categoria;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Categoria?> Update(Categoria categoria)
        {
            var categoriaUpdate = await _context.Categorias.FindAsync(categoria.Id);

            if (categoriaUpdate == null)
                return null;

            await _context.SaveChangesAsync();

            return categoria;
        }

        async Task<IEnumerable<Categoria>> ICategoriaService.GetByTipo(string descricao)
        {
            var categoria = await _context.Categorias
                .Include (c => c.Produtos)
                .Where(c => c.Tipo.Contains(descricao))
                .ToListAsync();
            return categoria;
        }
    }
}
