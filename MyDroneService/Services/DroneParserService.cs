using MyDroneService.Interfaces;
using MyDroneService.Models;

namespace MyDroneService.Services
{
    public class DroneParserService : IDroneParser
    {
        private readonly DataReaderUtil ReaderUtil;

        public DroneParserService()
        {
            this.ReaderUtil = new DataReaderUtil();
        }

        public IDictionary<Drone, int> Parse(string? line)
        {
            Drone? drone = null;
            SortedDictionary<Drone, int> squadDrone = new SortedDictionary<Drone, int>();

            string[] drones = line.Split(",", StringSplitOptions.TrimEntries);

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
                        drone.MaxLoadWeight = this.ReaderUtil.WeightParser(drones[i]);

                        squadDrone.Add(drone, drone.MaxLoadWeight);
                    }
                }
            }

            return squadDrone;
        }
    }
}
