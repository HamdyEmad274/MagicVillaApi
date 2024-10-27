namespace MagicVillaApi.Logging
{
    public class Logging : ILogging
    {
        public void Log(string message, string type)
        {
            if (type.ToLower() == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR - " + message);
                Console.ResetColor();
            }
            else {
                Console.WriteLine(message);
            }
        }
    }
}
