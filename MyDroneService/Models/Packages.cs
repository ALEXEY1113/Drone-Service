namespace MyDroneService.Models
{
    public class Packages
    {
        public string Name { get; set; }

        public int PackageWeight { get; set; }

        public Packages(string name)
        {
            this.Name = name;
        }
    }
}
