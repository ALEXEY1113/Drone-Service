using MyDroneService.Interfaces;
using MyDroneService.Models;

namespace MyDroneService.Services
{
    public class AssignmentCenterService : IDeliveryPlanner
    {
        public IDictionary<Drone, int>? DroneSquad { get; set; }

        public IEnumerable<Packages?>? Locations { get; set; }

        public IDictionary<string, DroneTripAssignment> DronesReady { get; set; }

        public AssignmentCenterService()
        {
            this.DroneSquad = null;
            this.Locations = null;
            this.DronesReady = new Dictionary<string, DroneTripAssignment>();
        }

        public IDictionary<string, DroneTripAssignment> GeneratePlan(IDictionary<Drone, int> drones, IEnumerable<Packages> packages)
        {
            this.DroneSquad = drones;
            this.Locations = packages;

            while (this.Locations.Count() > 0)
            {
                Packages? ld = ((Queue<Packages>)this.Locations).Dequeue();

                foreach (KeyValuePair<Drone, int> droneEntry in this.DroneSquad)
                {
                    bool needResetWeight = false;

                    Drone drone = droneEntry.Key;
                    int droneWeightReal = drone.MaxLoadWeight;
                    int droneWeightTemp = droneEntry.Value;

                    if (ld != null && ld.PackageWeight <= droneWeightTemp)
                    {
                        DroneTripAssignment? droneOrder = ((Dictionary<string, DroneTripAssignment>) this.DronesReady).GetValueOrDefault(drone.Name);
                        if (droneOrder == null)
                        {
                            droneOrder = new DroneTripAssignment();
                            droneOrder.DroneName = drone.Name;
                            droneOrder.Deliveries = new List<Trip>();

                            this.DronesReady.Add(drone.Name, droneOrder);
                        }

                        if (ld.PackageWeight <= droneWeightTemp && ld != null)
                        {
                            if (droneOrder.Deliveries.Count > 0)
                            {
                                needResetWeight = AssignPackageToDroneRoute(droneWeightReal, ld, droneOrder.Deliveries);
                            }
                            else
                            {
                                Trip nt = new Trip();
                                nt.TripNo = 1;
                                nt.Locations = new List<Packages>();
                                nt.Locations.Add(ld);

                                droneOrder.Deliveries.Add(nt);
                            }

                            this.DroneSquad[drone] = ((needResetWeight) ? droneEntry.Key.MaxLoadWeight : droneEntry.Value) - ld.PackageWeight;
                            ld = null;
                            break;
                        }
                    }
                }

                if (ld != null)
                {
                    FindSlotForPackage(ld, this.DroneSquad);
                }
            }

            return this.DronesReady;
        }

        private bool AssignPackageToDroneRoute(int realWeight, Packages ld, List<Trip> trips)
        {
            bool foundRoute = false;
            bool needResetTempWeight = false;

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
                nt.Locations = new List<Packages>();
                nt.Locations.Add(ld);

                trips.Add(nt);

                needResetTempWeight = true;
            }

            return needResetTempWeight;
        }

        private void FindSlotForPackage(Packages ld, IDictionary<Drone, int> droneSquad)
        {
            // Original drones ables to load the weight from the package
            var dronesAblesToLoad = droneSquad.Where(ds => ld.PackageWeight <= ds.Key.MaxLoadWeight).Select(ds => ds.Key.Name).ToList();

            // Look for drones with already routes
            var dronesSelected = dronesAblesToLoad.Where(k => DronesReady.ContainsKey(k)).Select(k => DronesReady[k]).ToList();

            // Select the drone with less routes
            DroneTripAssignment droneWithLessRoutes = dronesSelected[0];
            foreach (var droneOrder in dronesSelected.Skip(1))
            {
                if (droneOrder.Deliveries.Count < droneWithLessRoutes.Deliveries.Count)
                    droneWithLessRoutes = droneOrder;
            }

            Trip nt = new Trip();
            nt.TripNo = droneWithLessRoutes.Deliveries.Count + 1;
            nt.Locations = new List<Packages>();
            nt.Locations.Add(ld);

            droneWithLessRoutes.Deliveries.Add(nt);

            // Reset temp weight
            Drone drone = droneSquad.Where(ds => droneWithLessRoutes.DroneName == ds.Key.Name).Select(ds => ds.Key).First();
            this.DroneSquad[drone] = drone.MaxLoadWeight - ld.PackageWeight;
        }

        private int SumRouteWeight(List<Packages> locDeli)
        {
            int sum = 0;
            for (int i = 0; i < locDeli.Count; i++)
            {
                sum += locDeli[i].PackageWeight;
            }

            return sum;
        }
    }
}
