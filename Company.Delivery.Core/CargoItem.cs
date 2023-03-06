using System.Globalization;

namespace Company.Delivery.Core;

public class CargoItem
{
    public Guid Id { get; set; }

    public Guid WaybillId { get; set; }

    public Waybill? Waybill { get; set; }

    // TODO: Уникальное значение в пределах сущности Waybill #Done
    public string Number
    {
        get
        {
            if (Waybill != null && Waybill.Items != null)
            {
                string WaybillItemID = "";
                int iid = 0;
                foreach (var i in Waybill.Items)
                {
                    iid += 1;
                    if (i == this)
                        return iid.ToString(CultureInfo.CurrentCulture);
                }
                return WaybillItemID;
            }
            else
                return "";
        }
        set { }
    }

    public string Name { get; set; } = null!;
}