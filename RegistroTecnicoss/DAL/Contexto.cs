using RegistroTecnicoss.Models;
using Microsoft.EntityFrameworkCore;

namespace RegistroTecnicoss.DAL
{
    public class Contexto : DbContext
    
    {
        public Contexto(DbContextOptions<Contexto> options) : base (options) { }
        
        public DbSet<Tecnico> Tecnico { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Tecnicos> Tecnico { get; set; }
        public DbSet<Clientes> Cliente { get; set; }
        public DbSet<Tickets> Ticket { get; set; }

    }
}
