using MyDroneService.Models;
using MyDroneService.Services;

public class Program
{
    public static void Main(string[] args)
    {
        String? line;
        bool dronesSetup = false;
        Stack<LocationDelivery?> locationStacks = new Stack<LocationDelivery?>();
        Queue<LocationDelivery?> queueLocations = new Queue<LocationDelivery?>();

        try
        {
            //Pass the file path and file name to the StreamReader constructor
            StreamReader sr = new StreamReader("..\\..\\..\\InputFolder\\Input.txt");
            
            //Read the first line of text
            line = sr.ReadLine();
            
            //Continue to read until you reach end of file
            while (line != null)
            {
                if (!dronesSetup)
                {
                    Dictionary<Drone, int> droneSquad = StringParser.SetupDrones(line);
                    dronesSetup = true;

                    //Read the next line
                    line = sr.ReadLine();
                }
                
                if (line != null)
                {
                    if (dronesSetup)
                    {
                        queueLocations.Enqueue(StringParser.SetupLocation(line));

                        //Read the next line
                        line = sr.ReadLine();
                    }
                }
            }
            
            //close the file
            sr.Close();
            //Console.ReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Executing finally block.");
        }
    }
}