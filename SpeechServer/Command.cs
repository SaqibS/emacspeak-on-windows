namespace EmacspeakWindowsSpeechServer
{
    using System;
    using System.Collections.Generic;

    internal class Command
    {
        public string Name { get; set; }
        public IList<string> Arguments { get; private set; }

        public static Command Parse(string line)
        {
            // Everything up to the first space is the command name.
            int pos = 0;
            string token = GetNextToken(line, ref pos, ' ');

            var cmd = new Command
            {
                Name = token.ToLower(),
                Arguments = new List<string>()
            };

            // From here on, each word is an argument.
            // Arguments containing a space are {surrounded in braces}.
            while (pos < line.Length)
            {
                SkipWhitespace(line, ref pos);
                if (pos >= line.Length)
                {
                    break;
                }

                char delimiter = ' ';
                if (pos < line.Length - 1 && line[pos] == '{')
                {
                    pos++;
                    delimiter = '}';
                }

                token = GetNextToken(line, ref pos, delimiter);
                if (!char.IsWhiteSpace(delimiter))
                {
                    pos++;
                }

                if (token.Length > 0)
                {
                    cmd.Arguments.Add(token);
                }
            }

            return cmd;
        }

        // Note: Pos will be updated to point to the first character following the token, or lastChar+1.
                private static string GetNextToken(string s, ref int pos, char delimiter)
        {
            int newPos = s.IndexOf(delimiter, pos);
            if (newPos < 0)
            {
                newPos = s.Length;
            }

            string token = s.Substring(pos, newPos - pos);
            pos = newPos;
            return token;
        }

        private static void SkipWhitespace(string s, ref int pos)
        {
            while (pos < s.Length && char.IsWhiteSpace(s[pos]))
            {
                pos++;
            }
        }
    }
}
