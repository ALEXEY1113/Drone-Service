using MyDroneService.Models;

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
                if (i % 2 == 0)
                {
                    drone = new Drone(drones[i]);
                }
                else
                {
                    if (drone != null)
                    {
                        drone.MaxLoadWeight = WeightParser(drones[i]);

                        squadDrone.Add(drone, drone.MaxLoadWeight);
                    }
                }
            }

            //write the line to console window
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
                        locationDelivery.PackageWeight = WeightParser(location[i]);
                    }
                }
            }

            //write the line to console window
            return locationDelivery;
        }

        private static int WeightParser(string str)
        {
            string weight = str.Replace("[", "").Replace("]", "");
            return int.Parse(weight);
        }
    }
}
