//#define DEV
#define DUMMY

using System;


namespace WinCleaner
{
    public class Program
    {
        private static void Main()
        {
            Console.WriteLine($"{1.2294:##0.##}");
#if DEV
            Console.Read();
#endif
#if !DUMMY
            Logger.CommitBeforeCleaning();
            
            Cleaner.LogWriter = Logger.Log;
            Cleaner.PerformCleaning();
            
            Logger.CommitAfterCleaning();
#endif
            // TODO check mozilla cache
        }
    }
}
