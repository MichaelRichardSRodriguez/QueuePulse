using System.Speech.Synthesis;

namespace QueuePulse.DataAccess.Services
{
    public class TextToSpeechService
    {
        public async Task ConvertTextToSpeech(string text)
        {
            SpeechSynthesizer synthesizer = new SpeechSynthesizer();

            //*** List all available voices ***
            //foreach (var voice in synthesizer.GetInstalledVoices())
            //{
            //    Console.WriteLine("Available voice: " + voice.VoiceInfo.Name);
            //}

            string voiceName = "Microsoft Zira Desktop";

            foreach (var voice in synthesizer.GetInstalledVoices())
            {
                if (voice.VoiceInfo.Name == voiceName)
                {
                    synthesizer.SelectVoice(voiceName);
                }
            }


            // Optionally set voice settings (rate, volume, etc.)
            synthesizer.Rate = 0; // Normal speed
            synthesizer.Volume = 100; // Full volume

            // Speak the text
            await Task.Run(() => synthesizer.SpeakAsync(text));
        }
    }
}
