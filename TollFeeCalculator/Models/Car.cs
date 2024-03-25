using TollFeeCalculator.Abstract;

namespace TollFeeCalculator;

public class Car() : Vehicle(VehicleType.Car)
{
    [Obsolete("Fetch VehicleType property instead")]
    public override string GetVehicleType()
    {
        return VehicleType.ToString();
    }
}