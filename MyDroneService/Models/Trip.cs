namespace MyDroneService.Models
{
    public class Trip
    {
        public int TripNo { get; set; }

        public List<Packages> Locations { get; set; }
    }
}
