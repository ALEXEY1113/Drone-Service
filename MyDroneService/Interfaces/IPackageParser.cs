using MyDroneService.Models;

namespace MyDroneService.Interfaces
{
    public interface IPackageParser
    {
        IEnumerable<Packages> Parse(IEnumerable<string> lines);
    }
}
