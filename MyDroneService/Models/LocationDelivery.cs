using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDroneService.Models
{
    public class LocationDelivery
    {
        public string Name { get; set; }

        public int PackageWeight { get; set; }

        public LocationDelivery(string name)
        {
            this.Name = name;
        }
    }
}
