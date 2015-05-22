namespace EmacspeakWindowsSpeechServer
{
    using System;
    using WMPLib;

    internal static class AudioManager
    {
        private static readonly WindowsMediaPlayer player = new WindowsMediaPlayer();

        public static void PlayAudio(string[] args)
        {
            if (args == null || args.Length < 1 || args[0].Length == 0)
            {
                return;
            }

            player.URL = args[0];
            player.controls.play();
        }
    }
}
