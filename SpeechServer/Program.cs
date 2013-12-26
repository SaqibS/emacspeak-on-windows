namespace EmacspeakWindowsSpeechServer
{
    using System;
    using System.Linq;
    using System.IO;
    using System.Text;

    internal static class Program
    {
        private static bool debugMode = false;

        internal static void Main(string[] args)
        {
            debugMode = args.Any(x => x.Equals("-debug", StringComparison.OrdinalIgnoreCase));

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
                if (debugMode)
                {
                    LogException(x);
                }
            }
        }

        private static void LogException(Exception x)
        {
            const string Filename = "EmacspeakWindowsSpeechServer.log";
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), Filename);
            using (var sw = new StreamWriter(path, true, Encoding.UTF8))
            {
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
