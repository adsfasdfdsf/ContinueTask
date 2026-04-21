using System;
using System.Threading.Tasks;

namespace ContinueTask;
public class Orchestrator
{
    public async Task<bool> RunAsync()
    {
        try
        {
            var showSplashTask = Tasks.ShowSplash();
            var requestLicenseTask =
                showSplashTask.ContinueWith(_ => Tasks.RequestLicense(),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
            requestLicenseTask.ContinueWith(task => Tasks.ErrorHandler(task),
                TaskContinuationOptions.OnlyOnFaulted);

            var checkForUpdateTask =
                showSplashTask.ContinueWith(_ => Tasks.CheckForUpdate(),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
            checkForUpdateTask.ContinueWith(task => Tasks.ErrorHandler(task),
                TaskContinuationOptions.OnlyOnFaulted);

            var downloadUpdateTask = checkForUpdateTask.ContinueWith(_ => Tasks.DownloadUpdate(),
                TaskContinuationOptions.OnlyOnRanToCompletion);
            downloadUpdateTask.ContinueWith(task => Tasks.ErrorHandler(task),
                TaskContinuationOptions.OnlyOnFaulted);

            var setupMenusTask =
                requestLicenseTask.ContinueWith(_ => Tasks.SetupMenus(),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
            var displayWelcomeScreenTask = Task
                .WhenAll(downloadUpdateTask, setupMenusTask)
                .ContinueWith(_ => Tasks.DisplayWelcomeScreen(),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
            
            var hideSplashTask =
                displayWelcomeScreenTask.ContinueWith(_ => Tasks.HideSplash(),
                    TaskContinuationOptions.OnlyOnRanToCompletion);
            
            await hideSplashTask;
            return hideSplashTask.IsCompletedSuccessfully;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed: {ex.Message}");
            return false;
        }
    }
}