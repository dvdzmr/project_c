

using Backend.Events;

namespace Backend
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Eventlistener.Mqtt();
            Console.ReadLine();
        }
    }
}