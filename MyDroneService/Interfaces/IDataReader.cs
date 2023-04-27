using MyDroneService.Models;

namespace MyDroneService.Interfaces
{
    public interface IDataReader
    {
        IDictionary<Drone, int> ReadDrones();

        IEnumerable<Packages> ReadPackages();
    }
}
