using MyDroneService.Models;

namespace MyDroneService.Interfaces
{
    public interface IDeliveryPlanner
    {
        IDictionary<string, DroneTripAssignment> GeneratePlan(IDictionary<Drone, int> drones, IEnumerable<Packages> packages);
    }
}
