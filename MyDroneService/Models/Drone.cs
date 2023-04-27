namespace MyDroneService.Models
{
    public class Drone : IComparable<Drone>
    {
        public string Name { get; set; }
        
        public int MaxLoadWeight { get; set; }

        public Drone(string name)
        {
            this.Name = name;
        }

        public int CompareTo(Drone? other)
        {
            return other!.MaxLoadWeight.CompareTo(this.MaxLoadWeight);
        }
    }
}
