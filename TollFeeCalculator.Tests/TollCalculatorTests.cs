using Xunit;

namespace TollFeeCalculator.Tests;

public class TollCalculatorTests
{
    [Theory]
    [InlineData("2024-03-25T07:15:00", "2024-03-25T10:15:00", 31)]
    [InlineData("2024-03-24T07:15:00", "2024-03-24T10:15:00", 0)]
    [InlineData("2024-03-25T07:15:00", "2024-03-25T07:22:00", 22)]
    [InlineData("2024-08-30T11:15:00", "2024-08-30T10:15:00", 18)]
    [InlineData("2024-08-30T11:15:00", "2024-08-30T10:16:00", 9)]
    public void GetTollFeeForOneDay_WithTwoPasses_TotalsFee(string firstPass, 
        string secondPass, int tollFee)
    {
        //Arrange
        var sut = new TollCalculator();
        var vehicle = new Car();
        var passes = new[] { DateTime.Parse(firstPass), DateTime.Parse(secondPass) };
        
        //Act
        var result = sut.GetTollFeeForOneDay(vehicle, passes);

        //Assert
        Assert.Equal(tollFee, result);
    }
    
    [Theory]
    [InlineData("2024-03-25T07:15:00", "2024-03-25T10:15:00", "2024-03-25T18:31:00", 31)]
    [InlineData("2024-08-30T07:15:00", "2024-08-30T10:15:00", "2024-08-30T15:30:00", 53)]
    [InlineData("2024-03-25T07:15:00", "2024-03-25T07:22:00", "2024-03-25T06:15:00", 31)]
    [InlineData("2024-08-30T11:15:00", "2024-08-30T10:15:00", "2024-08-30T12:15:01", 27)]
    [InlineData("2024-08-30T11:15:00", "2024-08-30T10:16:00", "2024-08-30T10:49:13", 9)]
    public void GetTollFeeForOneDay_WithThreePasses_TotalsFee(string firstPass, 
        string secondPass, string thirdPass, int tollFee)
    {
        //Arrange
        var sut = new TollCalculator();
        var vehicle = new Car();
        var passes = new[] { DateTime.Parse(firstPass), DateTime.Parse(secondPass), 
            DateTime.Parse(thirdPass)};
        
        //Act
        var result = sut.GetTollFeeForOneDay(vehicle, passes);

        //Assert
        Assert.Equal(tollFee, result);
    }
    
    [Theory]
    [InlineData("2024-1-1")]
    [InlineData("2024-12-31")]
    [InlineData("2024-06-06")]
    [InlineData("2024-07-06")]
    [InlineData("2024-12-24")]
    public void IsTollFreeDay_Holiday_IsTullFree(DateTime dateTime)
    {
        //Arrange
        var sut = new TollCalculator();
        
        //Act
        var result = sut.IsTollFreeDate(dateTime);

        //Assert
        Assert.True(result);
    }
    
    [Theory]
    [InlineData("2024-1-31")]
    [InlineData("2024-01-31")]
    [InlineData("2024-09-06")]
    [InlineData("2024-12-30")]
    public void IsTollFreeDay_NonHoliday_IsNotTullFree(DateTime dateTime)
    {
        //Arrange
        var sut = new TollCalculator();
        
        //Act
        var result = sut.IsTollFreeDate(dateTime);

        //Assert
        Assert.False(result);
    }
    
    [Fact]
    public void IsTollFreeVehicle_AMotorBike_IsTullFree()
    {
        //Arrange
        var sut = new TollCalculator();
        var vehicle = new Motorbike();
        
        //Act
        var result = sut.IsTollFreeVehicle(vehicle);

        //Assert
        Assert.True(result);
    }
    
    [Fact]
    public void IsTollFreeVehicle_ACar_IsNotTullFree()
    {
        //Arrange
        var sut = new TollCalculator();
        var vehicle = new Car();
        
        //Act
        var result = sut.IsTollFreeVehicle(vehicle);

        //Assert
        Assert.False(result);
    }
}