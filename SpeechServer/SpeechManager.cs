namespace EmacspeakWindowsSpeechServer
{
    using System;
    using System.Globalization;
    using System.Speech.Synthesis;

    internal static class SpeechManager
    {
        private static readonly object lockObject = new object();
        private static SpeechSynthesizer synth = new SpeechSynthesizer();
        private static PromptBuilder promptBuilder = new PromptBuilder(CultureInfo.CurrentUICulture);
        private static double characterScaleFactor = 1.0;

        public static void Version(string[] args)
        {
            Version version = System.Environment.OSVersion.Version;
            lock (lockObject)
            {
                synth.SpeakAsyncCancelAll();
                synth.SpeakAsync(string.Format("{0}.{1}", version.Major, version.Minor));
            }
        }

        public static void Say(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                return;
            }

            lock (lockObject)
            {
                synth.SpeakAsyncCancelAll();
                synth.SpeakAsync(args[0]);
            }
        }

        public static void SayCharacter(string[] args)
        {
            if (args == null || args.Length < 1 || args[0].Length == 0)
            {
                return;
            }

            lock (lockObject)
            {
                synth.SpeakAsyncCancelAll();
                int originalRate = synth.Rate;
                synth.Rate = (int)Math.Round(((synth.Rate + 10) * characterScaleFactor) - 10);
                synth.SpeakAsync(args[0][0].ToString());
                synth.Rate = originalRate;
            }
        }

        public static void Dispatch(string[] args)
        {
            lock (lockObject)
            {
                synth.SpeakAsync(promptBuilder);
                promptBuilder = new PromptBuilder();
            }
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

        public static void QueueAudio(string[] args)
        {
            if (args == null || args.Length < 1)
            {
                return;
            }

            promptBuilder.AppendAudio(args[0]);
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
            lock (lockObject)
            {
                synth = new SpeechSynthesizer();
                promptBuilder = new PromptBuilder();
            }
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
