using Microsoft.EntityFrameworkCore;
using DentalClinic.Models;

namespace DentalClinic.Data
{
    public class DentalClinicContext : DbContext
    {
        public DentalClinicContext(DbContextOptions<DentalClinicContext> options)
            : base(options) { }

        public DbSet<Cliente> Cliente => Set<Cliente>();
        public DbSet<Servicio> Servicio => Set<Servicio>();
        public DbSet<ItemServicio> ItemServicio => Set<ItemServicio>();
        public DbSet<HorarioAtencion> HorarioAtencion => Set<HorarioAtencion>();
        public DbSet<ConfiguracionEmpresa> ConfiguracionEmpresa => Set<ConfiguracionEmpresa>();
        public DbSet<Reserva> Reserva => Set<Reserva>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Cliente -> Reservas
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Reservas)
                .WithOne(r => r.Cliente)
                .HasForeignKey(r => r.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            // Servicio -> Items
            modelBuilder.Entity<Servicio>()
                .HasMany(s => s.Items)
                .WithOne(i => i.Servicio)
                .HasForeignKey(i => i.ServicioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Servicio -> Reservas
            modelBuilder.Entity<Servicio>()
                .HasMany(s => s.Reservas)
                .WithOne(r => r.Servicio)
                .HasForeignKey(r => r.ServicioId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
