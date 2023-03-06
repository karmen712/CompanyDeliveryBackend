namespace Company.Delivery.Core;

public class Waybill
{
    public Guid Id { get; set; }

    // TODO: уникальное значение #Done
    public string Number { get; set; } = Guid.NewGuid().ToString();

    public DateTime Date { get; set; }

    public ICollection<CargoItem>? Items { get; set; }
}