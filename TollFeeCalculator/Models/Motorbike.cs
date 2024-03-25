namespace TollFeeCalculator
{
    public class Motorbike : IVehicle
    {
        public VehicleType VehicleType => VehicleType.Motorbike;

        [Obsolete("Fetch VehicleType property instead")]
        public string GetVehicleType()
        {
            return "Motorbike";
        }
    }
}