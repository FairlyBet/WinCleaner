namespace WinCleaner
{
    public class Program
    {
        private static void Main()
        {
            Logger.CommitBeforeCleaning();
            WinCleaner.Cleaner.PerformCleaning();
            Logger.CommitAfterCleaning();
            Logger.Publish();
            // TODO check mozilla cache
        }
    }
}
