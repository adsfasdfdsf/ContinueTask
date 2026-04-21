using System;
using System.Threading.Tasks;

namespace ContinueTask;
public static class Tasks
{
    private static readonly Random _random = new Random();

    public static Task ShowSplash()
    {
        Console.WriteLine("Show splash");
        return Task.CompletedTask;
    }

    public static Task RequestLicense()
    {
        if (_random.Next(0, 100) < 95)
        {
            Console.WriteLine("Request License");
        }
        else
        {
            Console.WriteLine("Failed task RequestLicense");
            throw new InvalidOperationException("License request failed");
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
        if (_random.Next(0, 100) < 95)
        {
            Console.WriteLine("Check For Update");
        }
        else
        {
            Console.WriteLine("Failed task CheckForUpdate");
            throw new InvalidOperationException("Update check failed");
        }
        return Task.CompletedTask;
    }

    public static Task DownloadUpdate()
    {
        if (_random.Next(0, 100) < 95)
        {
            Console.WriteLine("Download Update");
        }
        else
        {
            Console.WriteLine("Failed task DownloadUpdate");
            throw new InvalidOperationException("Download update failed");
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
        if (task.IsFaulted)
        {
            Console.WriteLine($"Error in task: {task.Exception?.InnerException?.Message}");
        }
        return Task.CompletedTask;
    }
}