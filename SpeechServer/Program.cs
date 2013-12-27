namespace EmacspeakWindowsSpeechServer
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;

    internal static class Program
    {
        internal static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    string line = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    var command = Command.Parse(line);
                    CommandDispatcher.Dispatch(command);
                }
            }
            catch (Exception x)
            {
                LogException(x);
            }
        }

        private static void LogException(Exception x)
        {
            // Calculate the log filename.
            string executingAssemblyPath = Assembly.GetExecutingAssembly().CodeBase;
            string logPath = Path.Combine(Path.GetDirectoryName(executingAssemblyPath), Path.GetFileNameWithoutExtension(executingAssemblyPath) + ".log");

            using (var sw = new StreamWriter(logPath, true, Encoding.UTF8))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                sw.WriteLine("Exception of type {0} was caught.", x.GetType().FullName);
                sw.WriteLine("Message: {0}", x.Message);

                if (x.InnerException != null)
                {
                    sw.WriteLine("Inner exception: {0}", x.InnerException.GetType().FullName);
                    sw.WriteLine("Inner exception message: {0}", x.InnerException.Message);
                }

                sw.WriteLine("Stack Trace:");
                sw.WriteLine(x.StackTrace);
                sw.WriteLine();
            }
        }
    }
}
