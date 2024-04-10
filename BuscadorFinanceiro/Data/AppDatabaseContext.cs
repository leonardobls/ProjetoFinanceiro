using BuscadorFinanceiro.Models;
using Microsoft.EntityFrameworkCore;

namespace BuscadorFinanceiro.Data
{

    public class AppDbContext : DbContext
    {
        public DbSet<ClienteModel> Cliente { get; set; }
        public DbSet<PagamentoModel> Pagamento { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseNpgsql("Server=localhost;Port=5433;Database=FinancialSearch;User Id=postgres;Password=1234;");
    }
}
