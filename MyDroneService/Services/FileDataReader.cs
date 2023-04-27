using MyDroneService.Exceptions;
using MyDroneService.Interfaces;
using MyDroneService.Models;

namespace MyDroneService.Services
{
    public class FileDataReader : IDataReader
    {
        private string InputPath { get; set; }
        
        private IEnumerable<string> Lines { get; set; }

        public FileDataReader(string inputPath)
        {
            if (inputPath.Length == 0 || String.IsNullOrEmpty(inputPath))
            {
                throw new InputNotProvidedException("The input was not provided!");
            }

            this.InputPath = inputPath;

            this.Lines = new List<string>();

            ReadFileFromPath();
        }

        public void ReadFileFromPath()
        {
            try
            {
                //Pass the file path and file name to the StreamReader constructor
                StreamReader sr = new StreamReader(this.InputPath);

                //Reading the lines from the file
                string? line = sr.ReadLine();

                while (line != null)
                {
                    ((List<string>)this.Lines).Add(line);

                    //Read the next line
                    line = sr.ReadLine();
                }
            }
            catch (InputNotProvidedException inpe)
            {
                Console.WriteLine($"It happens an exception {nameof(InputNotProvidedException)}", inpe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"It happens an exception {nameof(Exception)}", ex.Message);
            }
        }

        public IEnumerable<Packages> ReadPackages()
        {
            if (this.Lines.Skip(1).Count() == 0)
            {
                throw new PackageException("The Drone list was not provided!");
            }

            IPackageParser packageParser = new PackageParserService();
            return packageParser.Parse(this.Lines.Skip(1));
        }

        public IDictionary<Drone, int> ReadDrones()
        {
            if (this.Lines.FirstOrDefault() == null)
            {
                throw new DroneException("The Drone list was not provided!");
            }

            IDroneParser droneParser = new DroneParserService();
            return droneParser.Parse(this.Lines.FirstOrDefault());
        }
    }
}
