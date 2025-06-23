
    using global::RegistroTecnicoss.DAL;
    using global::RegistroTecnicoss.Models;
    using global::RegistroTecnicoss.Models.RegistroTecnicos.Models;
    using Microsoft.EntityFrameworkCore;
   
    using System.Linq.Expressions;

    namespace RegistroTecnicoss.Services
    {
        public class SistemaService(IDbContextFactory<Contexto> DbFactory)
        {
            private async Task<bool> Existe(int sistemaId)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                return await contexto.Sistemas.AnyAsync(s => s.SistemaId == sistemaId);
            }

            public async Task<bool> ExisteDescripcion(string descripcion, int? idExcluir = null)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                var query = contexto.Sistemas.AsQueryable();

                if (idExcluir.HasValue)
                    query = query.Where(s => s.SistemaId != idExcluir.Value);

                return await query.AnyAsync(s => s.Descripcion.ToLower() == descripcion.ToLower());
            }

            private async Task<bool> Insertar(Sistemas sistema)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                contexto.Sistemas.Add(sistema);
                return await contexto.SaveChangesAsync() > 0;
            }

            private async Task<bool> Modificar(Sistemas sistema)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                contexto.Update(sistema);
                return await contexto.SaveChangesAsync() > 0;
            }

            public async Task<bool> Guardar(Sistemas sistema)
            {
                if (sistema.SistemaId == 0)
                {
                    if (await ExisteDescripcion(sistema.Descripcion))
                        return false;

                    return await Insertar(sistema);
                }
                else
                {
                    if (await ExisteDescripcion(sistema.Descripcion, sistema.SistemaId))
                        return false;

                    return await Modificar(sistema);
                }
            }

            public async Task<Sistemas?> Buscar(int sistemaId)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                return await contexto.Sistemas
                    .AsNoTracking()
                    .FirstOrDefaultAsync(s => s.SistemaId == sistemaId);
            }

            public async Task<bool> Eliminar(int sistemaId)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                try
                {
                    var rowsAffected = await contexto.Sistemas
                        .Where(s => s.SistemaId == sistemaId)
                        .ExecuteDeleteAsync();

                    return rowsAffected > 0;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            public async Task<List<Sistemas>> Listar(Expression<Func<Sistemas, bool>> criterio)
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                return await contexto.Sistemas
                    .Where(criterio)
                    .AsNoTracking()
                    .ToListAsync();
            }

            public async Task<List<Sistemas>> ListarTodo()
            {
                await using var contexto = await DbFactory.CreateDbContextAsync();
                return await contexto.Sistemas
                    .AsNoTracking()
                    .ToListAsync();
            }
        }
    }
