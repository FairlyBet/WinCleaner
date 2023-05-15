namespace WinCleaner
{
    public class Program
    {
        private static void Main()
        {
            Logger.CommitBeforeCleaning();
            Cleaner.PerformCleaning();
            Logger.CommitAfterCleaning();
            Logger.Publish();
            // TODO check mozilla cache
        }
    }
}
