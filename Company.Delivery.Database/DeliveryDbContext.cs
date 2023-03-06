﻿using Company.Delivery.Core;
using Company.Delivery.Database.ModelConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Company.Delivery.Database;

public class DeliveryDbContext : DbContext
{
    public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options) : base(options) { }

    public DbSet<CargoItem> CargoItems { get; protected init; } = null!;

    public DbSet<Waybill> Waybills { get; protected init; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new WaybillConfiguration());
        modelBuilder.ApplyConfiguration(new CargoItemConfiguration());
        base.OnModelCreating(modelBuilder);
        // TODO: регистрация всех реализаций IEntityTypeConfiguration в сборке Company.Delivery.Database #Done
        // throw new NotImplementedException();
    }
}