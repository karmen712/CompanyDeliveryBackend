using Company.Delivery.Api.Controllers.Waybills.Request;
using Company.Delivery.Api.Controllers.Waybills.Response;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Company.Delivery.Api.Controllers.Waybills;

/// <summary>
/// Waybills management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WaybillsController : ControllerBase
{
    private readonly IWaybillService _waybillService;

    /// <summary>
    /// Waybills management
    /// </summary>
    public WaybillsController(IWaybillService waybillService) => _waybillService = waybillService;

    /// <summary>
    /// Получение Waybill
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено или кодом 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        WaybillDto? wbdto = null;
        try
        {
            wbdto = await _waybillService.GetByIdAsync(id, cancellationToken);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        if (wbdto == null)
            return NotFound();
        else
        {
            WaybillResponse wbr = new WaybillResponse
            {
                Id = wbdto.Id, Number = wbdto.Number, Date = wbdto.Date,
                Items = wbdto.Items?.Select(p => new CargoItemResponse { Id = p.Id, Name = p.Name, Number = p.Number, WaybillId = p.WaybillId })
            };
            return Ok(wbr);
        }
    }

    /// <summary>
    /// Создание Waybill
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] WaybillCreateRequest request, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если успешно создано
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        var wbdto = await _waybillService.CreateAsync(new WaybillCreateDto { Number = request.Number, Date = request.Date, Items = request.Items?.Select(p => new CargoItemCreateDto { Name = p.Name, Number = p.Number }) }, cancellationToken);
        if (wbdto == null)
            return BadRequest();
        else
        {
            WaybillResponse wbr = new WaybillResponse
            {
                Id = wbdto.Id, Number = wbdto.Number, Date = wbdto.Date,
                Items = wbdto.Items?.Select(p => new CargoItemResponse { Id = p.Id, Name = p.Name, Number = p.Number, WaybillId = p.WaybillId })
            };
            return Ok(wbr);
        }
    }

    /// <summary>
    /// Редактирование Waybill
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, [FromBody] WaybillUpdateRequest request, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено и изменено, или 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        WaybillDto? wbdto = null;
        try
        {
            wbdto = await _waybillService.UpdateByIdAsync(id, new WaybillUpdateDto { Number = request.Number, Date = request.Date, Items = request.Items?.Select(p => new CargoItemUpdateDto { Name = p.Name, Number = p.Number }) }, cancellationToken);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }

        if (wbdto == null)
            return NotFound();
        else
        {
            WaybillResponse wbr = new WaybillResponse
            {
                Id = wbdto.Id, Number = wbdto.Number, Date = wbdto.Date,
                Items = wbdto.Items?.Select(p => new CargoItemResponse { Id = p.Id, Name = p.Name, Number = p.Number, WaybillId = p.WaybillId })
            };
            return Ok(wbr);
        }
    }

    /// <summary>
    /// Удаление Waybill
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено и удалено, или 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try
        {
            await _waybillService.DeleteByIdAsync(id, cancellationToken);
            return Ok();
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
    }
}