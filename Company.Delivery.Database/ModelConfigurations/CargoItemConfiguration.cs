using Company.Delivery.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Delivery.Database.ModelConfigurations;

internal class CargoItemConfiguration : IEntityTypeConfiguration<CargoItem>
{
    public void Configure(EntityTypeBuilder<CargoItem> builder)
    {
        // TODO: все строковые свойства должны иметь ограничение на длину #Done
        // TODO: должно быть ограничение на уникальность свойства CargoItem.Number в пределах одной сущности Waybill #Done
        // TODO: ApplicationDbContextTests должен выполняться без ошибок #Done
        builder.ToTable("CargoItems");
        builder.Property(ci => ci.Number).HasMaxLength(255);
        builder.Property(ci => ci.Name).HasMaxLength(255);
        builder.HasKey(ci => ci.Id);
        builder.HasIndex(ci => new { ci.WaybillId, ci.Number }).IsUnique();
        builder.HasOne(ci => ci.Waybill).WithMany(wb => wb.Items);
    }
}