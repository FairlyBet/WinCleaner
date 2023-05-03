using System.Collections.Generic;
using System.IO;
using System.Text;


namespace WinCleaner
{
    internal static class Logger
    {
        private static readonly List<Log> s_logs = new List<Log>();


        public static void Log(Log log)
        {
            s_logs.Add(log);
        }

        public static void Publish()
        {
            long totalSize = 0;
            var sb = new StringBuilder(s_logs.Count);

            foreach (var item in s_logs)
            {
                sb.AppendLine(item.Name + " - " + item.Size + " bytes");
                totalSize += item.Size;
            }
            sb.Append("Total: " + totalSize + " bytes");

            File.WriteAllText("logs.txt", sb.ToString());
        }
    }

    internal class Log
    {
        public string Name { get; set; }
        public long Size { get; set; }
    }
}
