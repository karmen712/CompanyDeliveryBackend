using Company.Delivery.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Delivery.Database.ModelConfigurations;

internal class WaybillConfiguration : IEntityTypeConfiguration<Waybill>
{
    public void Configure(EntityTypeBuilder<Waybill> builder)
    {
        // TODO: все строковые свойства должны иметь ограничение на длину #Done
        // TODO: должно быть ограничение на уникальность свойства Waybill.Number #Done
        // TODO: ApplicationDbContextTests должен выполняться без ошибок #Done
        builder.ToTable("Waybills");
        builder.Property(wb => wb.Number).HasMaxLength(255);
        builder.Property(wb => wb.Date).HasColumnType("DateTime");
        builder.HasKey(wb => wb.Id);
        builder.HasIndex(wb => wb.Number).IsUnique();
        builder.HasMany(wb => wb.Items).WithOne(ci => ci.Waybill);
    }
}