namespace MyDroneService.Models
{
    public class DronePackageOrder
    {
        public string DroneName { get; set; }

        public List<Trip> Deliveries { get; set; }
    }
}
