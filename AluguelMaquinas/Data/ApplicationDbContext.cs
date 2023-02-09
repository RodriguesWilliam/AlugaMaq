using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AluguelMaquinas.Models;

namespace AluguelMaquinas.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<AluguelMaquinas.Models.Equipamento> Equipamento { get; set; }
        public DbSet<AluguelMaquinas.Models.Cliente> Cliente { get; set; }
        public DbSet<AluguelMaquinas.Models.Aluguel> Aluguel { get; set; }
        public DbSet<AluguelMaquinas.Models.AluguelEquipamento> AluguelEquipamento { get; set; }
    }
}