namespace MyDroneService.Exceptions
{
    public class InputNotProvidedException : Exception
    {
        public InputNotProvidedException(string? message) : base(message)
        {
        }
    }
}
