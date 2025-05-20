using RegistroTecnicoss.Models;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicoss.DAL
{
    public class Contexto : DbContext
    
    {
        public Contexto(DbContextOptions<Contexto> options) : base (options) { }
        
        public DbSet<Tecnico> Prestamos { get; set; }

    }
}
