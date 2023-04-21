using MyDroneService.Models;

namespace MyDroneService.Services
{
    public class AssignmentCenterService : IAssignOrderCmd
    {
        public Dictionary<Drone, int> DroneSquad { get; set; }

        public Queue<LocationDelivery?> Locations { get; set; }

        private Dictionary<string, DronePackageOrder> DronesReady = new Dictionary<string, DronePackageOrder>();

        public void Execute()
        {
            while (Locations.Count > 0)
            {
                LocationDelivery? ld = Locations.Dequeue();

                foreach (KeyValuePair<Drone, int> droneEntry in DroneSquad)
                {
                    Drone drone = droneEntry.Key;
                    int droneWeightReal = drone.MaxLoadWeight;
                    int droneWeightTemp = droneEntry.Value;

                    if (ld != null && ld.PackageWeight <= droneWeightTemp)
                    {
                        DronePackageOrder? droneOrder = DronesReady.GetValueOrDefault(drone.Name);
                        if (droneOrder == null)
                        {
                            droneOrder = new DronePackageOrder();
                            droneOrder.DroneName = drone.Name;
                            droneOrder.Deliveries = new List<Trip>();

                            DronesReady.Add(drone.Name, droneOrder);
                        }

                        if (ld.PackageWeight <= droneWeightTemp && ld != null)
                        {
                            if (droneOrder.Deliveries.Count > 0)
                            {
                                AssignPackageToDroneRoute(droneWeightReal, ld, droneOrder.Deliveries);
                            }
                            else
                            {
                                Trip nt = new Trip();
                                nt.TripNo = 1;
                                nt.Locations = new List<LocationDelivery>
                                {
                                    ld
                                };

                                droneOrder.Deliveries.Add(nt);
                            }

                            DroneSquad[drone] = droneEntry.Value - ld.PackageWeight;
                            ld = null;
                            break;
                        }
                    }
                }

                if (ld != null)
                {
                    FindSlotForPackage(ld, DroneSquad);
                }
            }
        }

        public void PrintRoutes()
        {
            foreach (KeyValuePair<string, DronePackageOrder> droneEntry in DronesReady)
            {
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine(droneEntry.Key);
                for (int i = 0; i < droneEntry.Value.Deliveries.Count; i++)
                {
                    Console.WriteLine("Trip #" + droneEntry.Value.Deliveries[i].TripNo);

                    var names = droneEntry.Value.Deliveries[i].Locations.Select(locName => locName.Name);
                    Console.WriteLine(string.Join(",", names));
                    Console.WriteLine(string.Empty);
                }
            }
        }

        private void AssignPackageToDroneRoute(int realWeight, LocationDelivery ld, List<Trip> trips)
        {
            bool foundRoute = false;

            for (int i = 0; i <trips.Count; i++)
            {
                int sum = SumRouteWeight(trips[i].Locations);
                int sumPlusItem = sum + ld.PackageWeight;

                if (sum < realWeight && sumPlusItem <= realWeight)
                {
                    trips[i].Locations.Add(ld);
                    foundRoute = true;
                    break;
                }
            }

            if (!foundRoute)
            {
                Trip nt = new Trip();
                nt.TripNo = trips.Count + 1;
                nt.Locations = new List<LocationDelivery>
                {
                    ld
                };

                trips.Add(nt);
            }
        }

        private void FindSlotForPackage(LocationDelivery ld, Dictionary<Drone, int> droneSquad)
        {
            // Original drones ables to load the weight from the package
            var dronesAblesToLoad = droneSquad.Where(ds => ld.PackageWeight <= ds.Key.MaxLoadWeight).Select(ds => ds.Key.Name).ToList();

            // Look for drones with already routes
            var dronesSelected = dronesAblesToLoad.Where(k => DronesReady.ContainsKey(k)).Select(k => DronesReady[k]).ToList();

            // Select the drone with less routes
            DronePackageOrder droneWithLessRoutes = dronesSelected[0];
            foreach (var droneOrder in dronesSelected.Skip(1))
            {
                if (droneOrder.Deliveries.Count < droneWithLessRoutes.Deliveries.Count)
                    droneWithLessRoutes = droneOrder;
            }

            Trip nt = new Trip();
            nt.TripNo = droneWithLessRoutes.Deliveries.Count + 1;
            nt.Locations = new List<LocationDelivery>
            {
                ld
            };

            droneWithLessRoutes.Deliveries.Add(nt);
        }

        private int SumRouteWeight(List<LocationDelivery> locDeli)
        {
            int sum = 0;
            for (int i = 0; i < locDeli.Count; i++)
            {
                sum = sum + locDeli[i].PackageWeight;
            }

            return sum;
        }
    }
}
