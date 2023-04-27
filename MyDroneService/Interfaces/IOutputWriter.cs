using MyDroneService.Models;

namespace MyDroneService.Interfaces
{
    public interface IOutputWriter
    {
        void Write(IDictionary<string, DroneTripAssignment> assignments);
    }
}
