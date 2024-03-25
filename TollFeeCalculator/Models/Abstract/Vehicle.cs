namespace TollFeeCalculator.Abstract;

public abstract class Vehicle(VehicleType vehicleType) : IVehicle
{
    public VehicleType VehicleType { get; } = vehicleType;

    public abstract string GetVehicleType();
}