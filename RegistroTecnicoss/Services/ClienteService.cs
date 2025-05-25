using RegistroTecnicoss.DAL;
using RegistroTecnicoss.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroTecnicoss.Services
{
    public class ClienteService(IDbContextFactory<Contexto> DbFactory)
    {
        private async Task<bool> Existe(int clienteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cliente
                .AnyAsync(c => c.ClienteId == clienteId);
        }

        public async Task<bool> ExisteNombreORnc(string nombre, string rnc, int? idExcluir = null)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var query = contexto.Cliente.AsQueryable();

            if (idExcluir.HasValue)
            {
                query = query.Where(c => c.ClienteId != idExcluir.Value);
            }

            return await query.AnyAsync(c => c.Nombres == nombre || c.Rnc == rnc);
        }

        private async Task<bool> Insertar(Cliente cliente)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Cliente.Add(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Cliente cliente)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Cliente cliente)
        {
            if (cliente.ClienteId == 0)
            {
                if (await ExisteNombreORnc(cliente.Nombres, cliente.Rnc))
                    return false;

                return await Insertar(cliente);
            }
            else
            {
                if (await ExisteNombreORnc(cliente.Nombres, cliente.Rnc, cliente.ClienteId))
                    return false;

                return await Modificar(cliente);
            }
        }

        public async Task<Cliente?> Buscar(int clienteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cliente
                .Include(c => c.Tecnico)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == clienteId);
        }

        public async Task<bool> Eliminar(int clienteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            try
            {
                var rowsAffected = await contexto.Cliente
                    .Where(c => c.ClienteId == clienteId)
                    .ExecuteDeleteAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar cliente: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Cliente>> Listar(Expression<Func<Cliente, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cliente
                .Include(c => c.Tecnico)
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Cliente>> ListarTodo()
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cliente
                .Include(c => c.Tecnico)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
