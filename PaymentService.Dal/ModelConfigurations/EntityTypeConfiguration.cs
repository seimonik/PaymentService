using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace PaymentService.Dal.ModelConfigurations;

public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : class
{
	public abstract void Configure(EntityTypeBuilder<T> builder);
}
