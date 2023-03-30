namespace MyDroneService.Models
{
    public class Drone
    {
        public string Name { get; set; }
        
        public int MaxLoadWeight { get; set; }

        public Drone(string name)
        {
            this.Name = name;
        }
    }
}
