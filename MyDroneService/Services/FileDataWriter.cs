using MyDroneService.Interfaces;
using MyDroneService.Models;
using System.Text;

namespace MyDroneService.Services
{
    public class FileDataWriter : IOutputWriter
    {
        private readonly string SEPARATOR_LINE = "--------------------------------------------------";
        private readonly string TRIP_NUMERATOR = "Trip #";

        public void Write(IDictionary<string, DroneTripAssignment> assignments)
        {
            string path = @"..\..\..\OutputFolder\Output.txt";

            if (!File.Exists(path))
            {
                foreach (KeyValuePair<string, DroneTripAssignment> assignment in assignments)
                {
                    File.AppendAllText(path, SEPARATOR_LINE + Environment.NewLine, Encoding.UTF8);
                    File.AppendAllText(path, assignment.Key + Environment.NewLine, Encoding.UTF8);
                    
                    for (int i = 0; i < assignment.Value.Deliveries.Count; i++)
                    {
                        var names = string.Join(",", assignment.Value.Deliveries[i].Locations.Select(locName => locName.Name));
                        
                        File.AppendAllText(path, TRIP_NUMERATOR + assignment.Value.Deliveries[i].TripNo + Environment.NewLine, Encoding.UTF8);
                        File.AppendAllText(path, names + Environment.NewLine, Encoding.UTF8);
                    }
                }
            }
        }
    }
}
