using TollFeeCalculator.Abstract;

namespace TollFeeCalculator.Services;

public interface ITollCalculator
{
    public int GetTollFeeForOneDay(Vehicle vehicle, IReadOnlyCollection<DateTime> dates);
}