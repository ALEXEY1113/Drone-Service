namespace MyDroneService.Services
{
    public class DataReaderUtil
    {
        public int WeightParser(string str)
        {
            string weight = str.Replace("[", "").Replace("]", "");
            return int.Parse(weight);
        }
    }
}
