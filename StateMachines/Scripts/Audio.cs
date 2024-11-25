using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using SharpDX.MediaFoundation;
using System.Collections.Generic;
using System.Reflection.Emit;



namespace StateMachines.Scripts
{
    internal class Audio
    {
        private List<SoundEffectInstance> effectInstances;
        private List<Song> song;

        public Audio(ContentManager cm) 
        {
            LoadContent(cm);
        }


        public void LoadContent(ContentManager cm)
        {
           
            List<SoundEffect> sfx = new List<SoundEffect>();
            effectInstances = new List<SoundEffectInstance>();
            sfx.Add(cm.Load<SoundEffect>(@"error3"));
            sfx.Add(cm.Load<SoundEffect>(@"footstep_concrete_003"));
            sfx.Add(cm.Load<SoundEffect>(@"footstep08"));
            sfx.Add(cm.Load<SoundEffect>(@"jump1"));


            for (int sfxnum = 0; sfxnum < sfx.Count; sfxnum++)
            {
                effectInstances.Add(sfx[sfxnum].CreateInstance());
            }

            song = new List<Song>();
            Song gs = cm.Load<Song>("Farm Frolics");
            song.Add(gs);
            gs = cm.Load<Song>("Mission Plausible");
            song.Add(gs);
            gs = cm.Load<Song>("Sad Descent");
            song.Add(gs);
        }

        public void PlaySFX(float vol , int effectNum)
        {
            effectInstances[effectNum].Volume = vol;
            effectInstances[effectNum].Play();
        }

        public void PlaySong(int trackNum, bool loop)
        {
            MediaPlayer.IsRepeating = loop;
            MediaPlayer.Play(song[trackNum]);
        }

    }
}
