namespace EmacspeakWindowsSpeechServer
{
    using System;
    using System.Globalization;
    using System.Speech.Synthesis;
    using System.Text;

    internal static class SpeechManager
    {
        private static SpeechSynthesizer synth = new SpeechSynthesizer();
        private static PromptBuilder promptBuilder = new PromptBuilder(CultureInfo.CurrentUICulture);
                private static double characterScaleFactor = 1.0;

        public static void Version(string[] args)
        {
            Version version = System.Environment.OSVersion.Version;
            synth.SpeakAsyncCancelAll();
            synth.SpeakAsync(string.Format("{0}.{1}", version.Major, version.Minor));
        }

        public static void Say(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                return;
            }

            synth.SpeakAsyncCancelAll();
            synth.SpeakAsync(args[0]);
        }

        public static void SayCharacter(string[] args)
        {
            if (args == null || args.Length < 1 || args[0].Length == 0)
            {
                return;
            }

            synth.SpeakAsyncCancelAll();
            char c = args[0][0];

            // Special case for punctuation not spoken correctly by SAPI.
            if (Punctuation.Replacements.ContainsKey(c))
            {
                synth.SpeakAsync(Punctuation.Replacements[c]);
                return;
            }
            
            var sb = new StringBuilder(capacity: 256);
            sb.Append("<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"" + CultureInfo.CurrentUICulture.IetfLanguageTag + "\">");
            sb.Append("<prosody rate=\"");
            sb.Append(characterScaleFactor);
            sb.Append(char.IsUpper(c) ? "\" pitch=\"x-high\">" : "\">");
            sb.Append("<say-as interpret-as=\"characters\" format=\"characters\">");
            sb.Append(c);
            sb.Append("</say-as></prosody></speak>");
            synth.SpeakSsmlAsync(sb.ToString());
        }

        public static void Dispatch(string[] args)
        {
            synth.SpeakAsync(promptBuilder);
            promptBuilder.ClearContent();
        }

        public static void Pause(string[] args)
        {
            synth.Pause();
        }

        public static void Resume(string[] args)
        {
            synth.Resume();
        }

        public static void StopSpeaking(string[] args)
        {
            synth.SpeakAsyncCancelAll();
        }

        public static void QueueText(string[] args)
        {
            if (args == null || args.Length < 1 || args[0].Length == 0)
            {
                return;
            }

            promptBuilder.AppendText(args[0]);
        }

        public static void QueueSilence(string[] args)
        {
            int duration;
            if (args == null || args.Length < 1 || !int.TryParse(args[0], out duration))
            {
                return;
            }

            promptBuilder.AppendBreak(TimeSpan.FromMilliseconds(duration));
        }

        public static void Reset(string[] args)
        {
            synth = new SpeechSynthesizer();
            promptBuilder = new PromptBuilder();
        }

        public static void SetRate(string[] args)
        {
            int rate;
            if (args == null || args.Length < 1 || !int.TryParse(args[0], out rate))
            {
                return;
            }

            if (rate < -10)
            {
                rate = -10;
            }
            else if (rate > 10)
            {
                rate = 10;
            }

            synth.Rate = rate;
        }

        public static void SetCharacterScale(string[] args)
        {
            double scaleFactor;
            if (args == null || args.Length < 1 || !double.TryParse(args[0], out scaleFactor))
            {
                return;
            }

            characterScaleFactor = scaleFactor;
        }
    }
}
