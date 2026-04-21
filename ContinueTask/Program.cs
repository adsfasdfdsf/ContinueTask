using System;
using System.Threading.Tasks;

namespace ContinueTask;
class Program
{
    public static async Task Main(string[] args)
    {
        var orchestrator = new Orchestrator();
        var success = await orchestrator.RunAsync();
        if (success)
        {
            Console.WriteLine("Finished Successfully");
        }
        else
        {
            Console.WriteLine("Error occured");
        }
    }
}