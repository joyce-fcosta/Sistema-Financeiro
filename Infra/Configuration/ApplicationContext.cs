using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Entities.Entidades;

namespace Infra.Configuration
{
	public class ApplicationContext : IdentityDbContext<ApplicationUser>
	{
		private readonly IConfiguration _configuration;

		public ApplicationContext(DbContextOptions options) : base(options)
		{
			
		}

		public DbSet<Despesa> Despesas { get; set; }
		public DbSet<Categoria> Categoria { get; set; }
		public DbSet<UsuarioSistemaFinanceiro> UsuarioSistemaFinanceiro { get; set; }
		public DbSet<SistemaFinanceiro> SistemaFinceiro { get; set; }


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{


			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(ObterStringConexao());
				//optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SqlServer"));
				base.OnConfiguring(optionsBuilder);
			}
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{

			builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);
			base.OnModelCreating(builder);
		}

		public string ObterStringConexao()
		{
			return "Data source=JOYCE\\SQLECOMMERCE; Initial Catalog = SISTEMA_FINANCEIRO; Integrated Security=True; TrustServerCertificate=True";

			
		}
	}
}
