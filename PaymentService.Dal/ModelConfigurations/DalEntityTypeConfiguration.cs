using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PaymentService.Dal.Interfaces;

namespace PaymentService.Dal.ModelConfigurations;

public abstract class DalEntityTypeConfiguration<T> : EntityTypeConfiguration<T> where T : class, IEntity
{
	public override void Configure(EntityTypeBuilder<T> builder)
	{
		builder.HasKey((T x) => x.Id);
	}
}
