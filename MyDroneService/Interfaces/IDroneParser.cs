using MyDroneService.Models;

namespace MyDroneService.Interfaces
{
    public interface IDroneParser
    {
        IDictionary<Drone, int> Parse(string? line);
    }
}
