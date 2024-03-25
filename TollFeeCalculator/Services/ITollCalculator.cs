namespace TollFeeCalculator.Services;

public interface ITollCalculator
{
    public int GetTollFeeForOneDay(IVehicle vehicle, IReadOnlyCollection<DateTime> dates);
}