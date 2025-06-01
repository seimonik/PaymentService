using Microsoft.EntityFrameworkCore;
using Npgsql.NameTranslation;
using Npgsql;
using PaymentService.Dal.Enums;
using PaymentService.Dal.Entities;

namespace PaymentService.Dal;

public class PaymentServiceDbContext : DbContext
{
	private static readonly INpgsqlNameTranslator _nullNameTranslator = new NpgsqlNullNameTranslator();

	public DbSet<Payment> Payments { get; set; } = null!;

	public PaymentServiceDbContext(DbContextOptions options) : base(options) { }

	public PaymentServiceDbContext() { }

	public static NpgsqlDataSource GetDataSource(string connectionString)
	{
		var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);

		dataSourceBuilder.MapEnum<PaymentStatus>(pgName: "lookups.PaymentStatus", nameTranslator: _nullNameTranslator);

		return dataSourceBuilder.Build();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentServiceDbContext).Assembly);

		modelBuilder.HasPostgresEnum<PaymentStatus>(schema: "lookups", nameTranslator: _nullNameTranslator);
	}
}
