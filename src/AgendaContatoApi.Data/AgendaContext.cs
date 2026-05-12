using AgendaContatoApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AgendaContatoApi.Data
{
    public class AgendaContext : DbContext
    {
        public DbSet<AgendaModel> TabelaAgenda { get; set; }

        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AgendaModel>().ToTable("Agenda");
        }
    }
}