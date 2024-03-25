using TollFeeCalculator.Abstract;

namespace TollFeeCalculator;

public class Motorbike() : Vehicle(VehicleType.Motorbike)
{
    [Obsolete("Fetch VehicleType property instead")]
    public override string GetVehicleType()
    {
        return "Motorbike";
    }
}