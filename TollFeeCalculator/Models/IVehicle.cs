namespace TollFeeCalculator;

public interface IVehicle
{
    VehicleType VehicleType { get; }
        
    [Obsolete("Fetch VehicleType property instead")]
    string GetVehicleType();
}