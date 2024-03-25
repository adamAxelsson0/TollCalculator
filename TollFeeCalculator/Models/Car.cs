namespace TollFeeCalculator
{
    public class Car : IVehicle
    {
        public VehicleType VehicleType => VehicleType.Car;
        
        [Obsolete("Fetch VehicleType property instead")]
        public string GetVehicleType()
        {
            return VehicleType.ToString();
        }
    }
}