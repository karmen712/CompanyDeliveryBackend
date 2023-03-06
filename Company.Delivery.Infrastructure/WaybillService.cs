using Company.Delivery.Database;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Dto;
using Microsoft.EntityFrameworkCore;

using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Company.Delivery.Infrastructure;

public class WaybillService : IWaybillService
{

    public async Task<WaybillDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        //throw new NotImplementedException();
        var dbContextOptions = new DbContextOptionsBuilder<DeliveryDbContext>();
        using (var db = new DeliveryDbContext(dbContextOptions.Options))
        {
            var wbdto = db.Waybills.Where(wb => wb.Id == id)
                .Select(wb => new WaybillDto { Id = wb.Id, Number = wb.Number, Date = wb.Date, Items = (IEnumerable<CargoItemDto>?)wb.Items }).FirstOrDefault<WaybillDto?>();
            if (wbdto == null)
                throw new EntityNotFoundException();
            else
                return await Task.FromResult<WaybillDto>(wbdto);
        }
    }

    public async Task<WaybillDto> CreateAsync(WaybillCreateDto data, CancellationToken cancellationToken)
    {
        var dbContextOptions = new DbContextOptionsBuilder<DeliveryDbContext>();
        using (var db = new DeliveryDbContext(dbContextOptions.Options))
        {
            var result = db.Waybills.Add(new Core.Waybill
            {
                Number = data.Number, Date = data.Date,
                Items = (ICollection<Core.CargoItem>?)data.Items?.Select(p => new Core.CargoItem { Name = p.Name, Number = p.Number })
            });
            WaybillDto wbdto = new WaybillDto { Id = result.Entity.Id, Number = result.Entity.Number, Date = result.Entity.Date, Items = (IEnumerable<CargoItemDto>?)result.Entity.Items };
            return await Task.FromResult(wbdto);
        }
    }

    public async Task<WaybillDto> UpdateByIdAsync(Guid id, WaybillUpdateDto data, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        //                                                                            ^^ ТУТ не сходится с тестом

        var dbContextOptions = new DbContextOptionsBuilder<DeliveryDbContext>();
        using (var db = new DeliveryDbContext(dbContextOptions.Options))
        {
            var result = db.Waybills.SingleOrDefault(b => b.Id == id);
            if (result != null)
            {
                result.Number = data.Number;
                result.Date = data.Date;
                result.Items = (ICollection<Core.CargoItem>?)data.Items?.Select(p => new Core.CargoItem { Name = p.Name, Number = p.Number });
                db.SaveChanges();
                WaybillDto wbdto = new WaybillDto { Id = result.Id, Number = result.Number, Date = result.Date, Items = (IEnumerable<CargoItemDto>?)result.Items };
                return await Task.FromResult(wbdto);
            }
            else
                throw new EntityNotFoundException();
        }
    }

    public Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        var dbContextOptions = new DbContextOptionsBuilder<DeliveryDbContext>();
        using (var db = new DeliveryDbContext(dbContextOptions.Options))
        {
            var result = db.Waybills.SingleOrDefault(b => b.Id == id);
            if (result != null)
            {
                Core.Waybill wb = new Core.Waybill() { Id = id };
                db.Waybills.Attach(wb);
                db.Waybills.Remove(wb);
                db.SaveChanges();
                return Task.CompletedTask;
            }
            else
                throw new EntityNotFoundException();
        }

    }
}