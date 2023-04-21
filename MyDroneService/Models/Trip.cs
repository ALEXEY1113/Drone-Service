namespace MyDroneService.Models
{
    public class Trip
    {
        public int TripNo { get; set; }

        public List<LocationDelivery> Locations { get; set; }
    }
}
