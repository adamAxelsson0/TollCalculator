using TollFeeCalculator.Abstract;

namespace TollFeeCalculator.Services;

public class TollCalculatorV2 : ITollCalculator
{
    private const int MaxFeeInSek = 60;
    
    /// <summary>
    /// Calculate the total toll fee for one day
    /// </summary>
    /// <param name="vehicle">the vehicle</param>
    /// <param name="dates">date and time of all passes on one day</param>
    /// <returns>the total toll fee for that day</returns>
    public int GetTollFeeForOneDay(Vehicle vehicle, IReadOnlyCollection<DateTime> dates)
    {
        return IsTollFreeVehicle(vehicle) 
            ? 0 
            : Math.Min(GetTollFeeForOneDay(dates), MaxFeeInSek);
    }

    private int GetTollFeeForOneDay(IReadOnlyCollection<DateTime> dates)
    {
        var tollFee = 0;

        var firstIteration = true;
        DateTime previousDate = default;
        
        foreach (var date in dates.Order())
        {
            if (firstIteration)
            {
                tollFee += GetTollFee(date);
                firstIteration = false;
                previousDate = date;
                continue;
            }

            if (IsWithinAnHour(previousDate.TimeOfDay, date.TimeOfDay))
            {
                continue;
            }
            
            tollFee += GetTollFee(date);
            previousDate = date;
        }

        return tollFee;
    }

    private static bool IsWithinAnHour(TimeSpan timeSpan, TimeSpan timeSpanToValidate)
    {
        return Math.Abs(timeSpanToValidate.TotalMinutes - timeSpan.TotalMinutes) < 60;
    }

    private static bool IsInRange(double value, double min, double max)
    {
        return value >= min && value <= max;
    }

    public bool IsTollFreeVehicle(IVehicle vehicle)
    {
        if (vehicle == null) return false;
        
        var vehicleType = vehicle.VehicleType;

        return Enum.IsDefined(typeof(TollFreeVehicle), vehicleType.ToString());
    }
    
    private int GetTollFee(DateTime date, IVehicle vehicle)
    {
        if (IsTollFreeDate(date) || IsTollFreeVehicle(vehicle)) return 0;
        
        var totalMinutes = date.TimeOfDay.TotalMinutes;

        if (IsInRange(totalMinutes, 360, 389))  // 06:00–06:29
            return 9;
        if (IsInRange(totalMinutes, 390, 419))  // 06:30–06:59
            return 16;
        if (IsInRange(totalMinutes, 420, 479))  // 07:00–07:59
            return 22;
        if (IsInRange(totalMinutes, 480, 509))  // 08:00–08:29
            return 16;
        if (IsInRange(totalMinutes, 510, 899))  // 08:30–14:59
            return 9;
        if (IsInRange(totalMinutes, 900, 929))  // 15:00–15:29
            return 16;
        if (IsInRange(totalMinutes, 930, 1019))  // 15:30–16:59
            return 22;
        if (IsInRange(totalMinutes, 1020, 1059))  // 17:00–17:59
            return 16;
        if (IsInRange(totalMinutes, 1060, 1089))  // 18:00–18:29
            return 9;
        // 18:30–05:59
        return 0;
    }
    
    private int GetTollFee(DateTime date)
    {
        if (IsTollFreeDate(date)) return 0;
        
        var totalMinutes = date.TimeOfDay.TotalMinutes;

        if (IsInRange(totalMinutes, 360, 389))  // 06:00–06:29
            return 9;
        if (IsInRange(totalMinutes, 390, 419))  // 06:30–06:59
            return 16;
        if (IsInRange(totalMinutes, 420, 479))  // 07:00–07:59
            return 22;
        if (IsInRange(totalMinutes, 480, 509))  // 08:00–08:29
            return 16;
        if (IsInRange(totalMinutes, 510, 899))  // 08:30–14:59
            return 9;
        if (IsInRange(totalMinutes, 900, 929))  // 15:00–15:29
            return 16;
        if (IsInRange(totalMinutes, 930, 1019))  // 15:30–16:59
            return 22;
        if (IsInRange(totalMinutes, 1020, 1059))  // 17:00–17:59
            return 16;
        if (IsInRange(totalMinutes, 1060, 1089))  // 18:00–18:29
            return 9;
        // 18:30–05:59
        return 0;
    }

    /// <summary>
    /// Checks if a given date is a toll-free date.
    /// Toll-free dates include Saturdays, Sundays, public holidays
    /// </summary>
    /// <param name="date">DateTime to be validated</param>
    /// <returns></returns>
    public bool IsTollFreeDate(DateTime date)
    { 
        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) return true;
        
        return date is { Month: 1, Day: 1 or 5 } ||
               date is { Month: 3, Day: 28 or 29 } ||
               date is { Month: 4, Day: 1 or 30 } ||
               date is { Month: 5, Day: 1 or 8 or 9 } ||
               date is { Month: 6, Day: 5 or 6 or 21 } ||
               date.Month == 7 ||
               date is { Month: 11, Day: 1 } ||
               date is { Month: 12, Day: 24 or 25 or 26 or 31 };
    }
    

    private enum TollFreeVehicle
    {
        Motorbike = 0,
        Tractor = 1,
        Emergency = 2,
        Diplomat = 3,
        Foreign = 4,
        Military = 5
    }
}