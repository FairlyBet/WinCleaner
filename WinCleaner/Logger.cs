using System.Collections.Generic;
using System.IO;
using System.Text;


namespace WinCleaner
{
    public static class Logger
    {
        public const string LogPath = "last_session.txt";
        private static readonly List<Log> s_logs = new List<Log>();
        private static long _total;

        public static long Total => _total;


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
            _total = totalSize;
            File.WriteAllText(LogPath, sb.ToString());
        }
    }

    public class Log
    {
        public string Name { get; set; }
        public long Size { get; set; }
    }
}
