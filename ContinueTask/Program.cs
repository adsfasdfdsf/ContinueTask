using System.Security.Cryptography;

namespace  ContinueTask;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    
    private static readonly Random _random = new Random();
    public static Task Main(string[] args)
    {
        var showSplashTask = ShowSplash();
        var requestLicenseTask =
            showSplashTask.ContinueWith(_ => RequestLicense(), 
                TaskContinuationOptions.OnlyOnRanToCompletion);
        requestLicenseTask.ContinueWith(task => ErrorHandler(task), 
            TaskContinuationOptions.OnlyOnFaulted);
        var checkForUpdateTask = 
            showSplashTask.ContinueWith(_ => CheckForUpdate(), 
                TaskContinuationOptions.OnlyOnRanToCompletion);
        checkForUpdateTask.ContinueWith(task => ErrorHandler(task), 
            TaskContinuationOptions.OnlyOnFaulted);
        var downloadUpdateTask = checkForUpdateTask.ContinueWith(_ => DownloadUpdate(), 
            TaskContinuationOptions.OnlyOnRanToCompletion);
        downloadUpdateTask.ContinueWith(task => ErrorHandler(task), 
            TaskContinuationOptions.OnlyOnFaulted);
        var setupMenusTask =
            requestLicenseTask.ContinueWith(_ => SetupMenus(), 
                TaskContinuationOptions.OnlyOnRanToCompletion);
        var displayWelcomeScreenTask = Task
            .WhenAll(downloadUpdateTask, setupMenusTask)
            .ContinueWith(_ => DisplayWelcomeScreen(), TaskContinuationOptions.OnlyOnRanToCompletion);
        var hideSplashTask =
            displayWelcomeScreenTask.ContinueWith(_ => HideSplash(), 
                TaskContinuationOptions.OnlyOnRanToCompletion);
        hideSplashTask.Wait();
        if (hideSplashTask.IsCompletedSuccessfully)
        {
            Console.WriteLine("Application Loaded Successfully");
        }
        else
        {
            Console.WriteLine("Application Load Failed");
        }
        return Task.CompletedTask;
    }


    public static Task ShowSplash()
    {
        Console.WriteLine("Show splash");
        return Task.CompletedTask;
    }

    public static Task RequestLicense()
    {
        if (_random.Next(0, 100) < 95){
            Console.WriteLine("Request License");
            
        }
        else
        {
            Console.WriteLine("Failed task RequestLicense");
            throw null;
        }
        return Task.CompletedTask;
    }

    public static Task SetupMenus()
    {
        Console.WriteLine("Setup Menus");
        return Task.CompletedTask;
    }

    public static Task CheckForUpdate()
    {
        if (_random.Next(0, 100) < 95){
            Console.WriteLine("Check For Update");
        }
        else
        {
            Console.WriteLine("Failed task CheckForUpdate");
            throw null;
        }
        return Task.CompletedTask;
    }

    public static Task DownloadUpdate()
    {
        if (_random.Next(0, 100) < 95){
            Console.WriteLine("Download Update");
        }
        else
        {
            Console.WriteLine("Failed task DownloadUpdate");
            throw null;
        }
        return Task.CompletedTask;
    }

    public static Task DisplayWelcomeScreen()
    {
        Console.WriteLine("Display Welcome Screen");
        return Task.CompletedTask;
    }

    public static Task HideSplash()
    {
        Console.WriteLine("Hide Splash");
        return Task.CompletedTask;
    }

    public static Task ErrorHandler(Task task)
    {
        if (task.IsFaulted){
            Console.WriteLine($"Error in task: {task.Exception?.InnerException?.Message}");
        }
        return Task.CompletedTask;
        
    }
}
