using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//reference added
using System.Speech.Synthesis;

namespace MP7_ValdezIII
{
    class TextToSpeech
    {
        SpeechSynthesizer synth;

        public TextToSpeech()
        {
            synth = new SpeechSynthesizer();
            //synth.SelectVoice((new SpeechSynthesizer()).GetInstalledVoices()[1].VoiceInfo.Name);

            synth.SetOutputToDefaultAudioDevice();
        }
    
        public void SpeakString(string x)
        {
            if (synth.State == SynthesizerState.Speaking)
            {
                synth.SpeakAsyncCancelAll();
            }

            synth.SpeakAsync(x);
        }

        public void Mute()
        {
            synth.Volume = 0;
        }

        public void test()
        {
            this.SpeakString("Testing Text To speech");
        }
     
    }
}
