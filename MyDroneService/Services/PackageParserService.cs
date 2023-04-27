using MyDroneService.Interfaces;
using MyDroneService.Models;

namespace MyDroneService.Services
{
    public class PackageParserService : IPackageParser
    {
        private readonly DataReaderUtil ReaderUtil;
        private readonly IEnumerable<Packages> PackageQueue;

        public PackageParserService()
        {
            this.ReaderUtil = new DataReaderUtil();
            this.PackageQueue = new Queue<Packages>();
        }

        public IEnumerable<Packages> Parse(IEnumerable<string> lines)
        {
            Packages? locationDelivery = null;
            for (int i = 0; i < lines.Count(); i++)
            {
                string locationInfo = lines.ToList()[i];
                string[] location = locationInfo.Split(",", StringSplitOptions.RemoveEmptyEntries);

                for (int k = 0; k < location.Length; k++)
                {
                    if (k % 2 == 0)
                    {
                        locationDelivery = new Packages(location[k]);
                    }
                    else
                    {
                        if (locationDelivery != null)
                        {
                            locationDelivery.PackageWeight = this.ReaderUtil.WeightParser(location[k]);
                        }
                    }
                }

                ((Queue<Packages>)this.PackageQueue).Enqueue(locationDelivery!);
            }

            return new Queue<Packages>(this.PackageQueue.OrderBy(pck => pck.PackageWeight));
        }
    }
}
