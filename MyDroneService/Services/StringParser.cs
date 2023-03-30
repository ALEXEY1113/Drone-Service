using MyDroneService.Models;
using System.Text;

namespace MyDroneService.Services
{
    public class StringParser
    {
        public static Dictionary<Drone, int> SetupDrones(string dronesInfo)
        {
            Drone? drone = null;
            Dictionary<Drone, int> squadDrone = new Dictionary<Drone, int>();

            string[] drones = dronesInfo.Split(",", StringSplitOptions.TrimEntries);

            for (int i = 0; i < drones.Length; i++)
            {
                if (i%2 == 0)
                {
                    drone = new Drone(drones[i]);
                }
                else
                {
                    if (drone != null)
                    {
                        string weight = drones[i].Replace("[", "").Replace("]", "");
                        drone.MaxLoadWeight = int.Parse(weight);

                        squadDrone.Add(drone, drone.MaxLoadWeight);
                    }
                }
            }

            //write the line to console window
            //Console.WriteLine(dronesInfo);
            return squadDrone;
        }

        public static LocationDelivery? SetupLocation(string locationInfo)
        {
            LocationDelivery? locationDelivery = null;
            string[] location = locationInfo.Split(",", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < location.Length; i++)
            {
                if (i % 2 == 0)
                {
                    locationDelivery = new LocationDelivery(location[i]);
                }
                else
                {
                    if (locationDelivery != null)
                    {
                        string weight = location[i].Replace("[", "").Replace("]", "");
                        locationDelivery.PackageWeight = int.Parse(weight);
                    }
                }
            }

            //write the line to console window
            //Console.WriteLine(locationInfo);
            return locationDelivery;
        }
    }
}
