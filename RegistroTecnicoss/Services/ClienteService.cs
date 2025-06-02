using RegistroTecnicoss.DAL;
using RegistroTecnicoss.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroTecnicoss.Services
{
    public class ClienteService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<bool> Guardar(Clientes cliente)
        {
            if (!await Existe(cliente.ClienteId))
            {
                return await Insertar(cliente);
            }
            else
            {
                return await Modificar(cliente);
            }
        }

        public async Task<bool> Existe(int ClienteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cliente.AnyAsync(c => c.ClienteId == ClienteId);
        }

        public async Task<bool> ExisteNombre(string Nombres)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cliente.AnyAsync(c => c.Nombres.ToLower() == Nombres.ToLower());
        }

        public async Task<bool> ExisteRNC(string Rnc)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cliente.AnyAsync(c => c.Rnc.ToLower() == Rnc.ToLower());
        }

        private async Task<bool> Insertar(Clientes cliente)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Cliente.Add(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Clientes cliente)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(cliente);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<Clientes?> Buscar(int ClienteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cliente.FirstOrDefaultAsync(c => c.ClienteId == ClienteId);
        }

        public async Task<bool> Eliminar(int ClienteId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cliente.AsNoTracking().Where(c => c.ClienteId == ClienteId).ExecuteDeleteAsync() > 0;
        }

        public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Cliente.Where(criterio).Include(c => c.Tecnico).AsNoTracking().ToListAsync();
        }

    }

}
