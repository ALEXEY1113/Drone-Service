namespace MyDroneService.Models
{
    public class DroneTripAssignment
    {
        public string DroneName { get; set; }

        public List<Trip> Deliveries { get; set; }
    }
}
