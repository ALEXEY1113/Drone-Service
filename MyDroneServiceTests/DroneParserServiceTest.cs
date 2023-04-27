using MyDroneService.Models;
using MyDroneService.Services;

namespace MyDroneServiceTests
{
    public class DroneParserServiceTest
    {
        [Theory]
        [InlineData("[DroneA], [200]", 1)]
        [InlineData("[DroneA], [200], [DroneB], [250]", 2)]
        [InlineData("[DroneA], [200], [DroneB], [250], [DroneC], [100]", 3)]
        [InlineData("[DroneA], [100], [DroneB], [250], [DroneC], [360], [DroneD], [380]", 4)]
        [InlineData("[DroneA], [100], [DroneB], [250], [DroneC], [360], [DroneD], [380], [DroneE], [200]", 5)]
        public void Parse_WhenCalledWithAnInput_ShouldReturnADictionaryWithAllDrones(string line, int dronesExpected)
        {
            // Arrange
            DroneParserService droneParserService = new DroneParserService();

            // Act
            IDictionary<Drone, int> dronesMap = droneParserService.Parse(line);

            // Assert
            Assert.Equal(dronesExpected, dronesMap.Count);
        }
    }
}
