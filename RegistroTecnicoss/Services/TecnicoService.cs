using RegistroTecnicoss.DAL;
using RegistroTecnicoss.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RegistroTecnicoss.Services
{
    public class TecnicoService(IDbContextFactory<Contexto> DbFactory)
    {
        private async Task<bool> Existe(int tecnicoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnico
                .AnyAsync(t => t.TecnicoId == tecnicoId);
        }

        public async Task<bool> ExisteNombre(string nombre, int? idExcluir = null)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var query = contexto.Tecnico.AsQueryable();

            if (idExcluir.HasValue)
            {
                query = query.Where(t => t.TecnicoId != idExcluir.Value);
            }

            return await query.AnyAsync(t => t.Nombre == nombre);
        }

        private async Task<bool> Insertar(Tecnico tecnico)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Tecnico.Add(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Tecnico tecnico)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.Update(tecnico);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Tecnico tecnico)
        {
            if (tecnico.TecnicoId == 0)
            {
                if (await ExisteNombre(tecnico.Nombre))
                    return false;

                return await Insertar(tecnico);
            }
            else
            {
                if (await ExisteNombre(tecnico.Nombre, tecnico.TecnicoId))
                    return false;

                return await Modificar(tecnico);
            }
        }

        public async Task<Tecnico?> Buscar(int tecnicoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnico
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.TecnicoId == tecnicoId);
        }

        public async Task<bool> Eliminar(int tecnicoId)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            try
            {
                var rowsAffected = await contexto.Tecnico
                    .Where(t => t.TecnicoId == tecnicoId)
                    .ExecuteDeleteAsync();

                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar técnico: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Tecnico>> Listar(Expression<Func<Tecnico, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnico
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Tecnico>> ListarTodo()
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.Tecnico
                .AsNoTracking()
                .ToListAsync();
        }
    }
}

