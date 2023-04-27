using MyDroneService.Exceptions;
using MyDroneService.Models;
using MyDroneService.Services;

public class Program
{
    public static void Main(string[] args)
    {
        IDictionary<Drone, int> droneSquad;
        IEnumerable<Packages> queueLocations;

        try
        {
            FileDataReader fileDataReader = new FileDataReader(args[0]);
            droneSquad = fileDataReader.ReadDrones();
            queueLocations = fileDataReader.ReadPackages();

            AssignmentCenterService center = new AssignmentCenterService();
            center.GeneratePlan(droneSquad, queueLocations);

            FileDataWriter fileDataWriter = new FileDataWriter();
            fileDataWriter.Write(center.DronesReady);
        }
        catch (InputNotProvidedException inpe)
        {
            Console.WriteLine("Exception: " + inpe.Message);
        }
        catch (DroneException de)
        {
            Console.WriteLine("Exception: " + de.Message);
        }
        catch (PackageException pe)
        {
            Console.WriteLine("Exception: " + pe.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Assignment Drones was finished!!!");
        }
    }
}