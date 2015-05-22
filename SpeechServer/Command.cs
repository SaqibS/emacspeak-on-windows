namespace EmacspeakWindowsSpeechServer
{
    using System;
    using System.Collections.Generic;

    internal class Command
    {
        public string Name { get; set; }
        public string[] Arguments { get; private set; }

        public static Command Parse(string line)
        {
            // Each command consists of an initial command word,
            // optionally followed by either {a series of words in braces}
            // or a series of space-separated arguments.

            // Find first word.
            line = line.TrimStart();
            int i = 0;
            while (i < line.Length && line[i] != ' ')
            {
                i++;
            }

            var cmd = new Command();
            cmd.Name = line.Substring(0, i);

            string args = line.Substring(i).Trim();
            if (string.IsNullOrEmpty(args))
            {
                cmd.Arguments = new string[0];
            }
            else if (args.StartsWith("{") && args.EndsWith("}"))
            {
                cmd.Arguments = new string[] { args.Substring(1, args.Length - 2) };
            }
            else
            {
                cmd.Arguments = args.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            }

            return cmd;
        }
    }
}
